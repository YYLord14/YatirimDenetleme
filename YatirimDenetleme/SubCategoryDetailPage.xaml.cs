namespace YatirimDenetleme;

public partial class SubCategoryDetailPage : ContentPage
{
    private DatabaseService _dbService;
    private string _kategoriAdi;
    private List<Asset> _kategoriVarliklari;

    public SubCategoryDetailPage(string kategoriAdi)
    {
        InitializeComponent();
        _dbService = new DatabaseService();
        _kategoriAdi = kategoriAdi;
        LblBaslik.Text = $"{_kategoriAdi} Portföyü";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VerileriYukle();
    }

    private async Task VerileriYukle()
    {
        var tumVarliklar = await _dbService.GetAssetsAsync();

        // Yalnızca seçilen ana kategoriye ait olan varlıkları süz
        _kategoriVarliklari = tumVarliklar
            .Where(v => v.AnaKategori == _kategoriAdi)
            .ToList();

        CvAltVarliklar.ItemsSource = _kategoriVarliklari;

        double toplamBakiye = _kategoriVarliklari.Sum(x => x.GuncelBakiye);

        if (_kategoriVarliklari.Count > 0 && toplamBakiye > 0)
        {
            // Alt Grafiği Çizdir
            var altGruplar = _kategoriVarliklari
                .GroupBy(v => string.IsNullOrEmpty(v.AltTurOrHisse) ? v.Platform : v.AltTurOrHisse)
                .Select(g => new KategoriOzet
                {
                    KategoriAdi = g.Key,
                    ToplamBakiye = g.Sum(x => x.GuncelBakiye)
                }).ToList();

            PieGraphicsViewSub.Drawable = new PieChartDrawable(altGruplar, toplamBakiye);
            PieGraphicsViewSub.Invalidate();

            // Legend Doldur
            LegendContainerSub.Children.Clear();
            var renkler = PieChartDrawable.RenkPaleti;
            int colorIndex = 0;

            foreach (var item in altGruplar)
            {
                var renk = renkler[colorIndex % renkler.Length];
                var itemStack = new HorizontalStackLayout { Spacing = 5, Margin = new Thickness(5) };
                itemStack.Children.Add(new BoxView { Color = renk, WidthRequest = 10, HeightRequest = 10, VerticalOptions = LayoutOptions.Center });
                itemStack.Children.Add(new Label { Text = $"{item.KategoriAdi} (%{(toplamBakiye > 0 ? (item.ToplamBakiye / toplamBakiye * 100) : 0):N0})", TextColor = Colors.White, FontSize = 11 });

                LegendContainerSub.Children.Add(itemStack);
                colorIndex++;
            }
        }
        else
        {
            PieGraphicsViewSub.Drawable = null;
            LegendContainerSub.Children.Clear();
        }
    }

    // Varlığa tıklanınca Güncelleme veya Silme Seçeneği Çıkar
    private async void OnVarlikGuncelleTapped(object sender, EventArgs e)
    {
        var border = sender as Border;
        var varlik = border?.BindingContext as Asset;

        if (varlik == null) return;

        // Kullanıcıya Ne Yapmak İstediğini Soruyoruz
        string eylem = await DisplayActionSheet(
            $"{varlik.AltTurOrHisse} - İşlem Seçin",
            "İptal",
            "🗑️ Varlığı Tamamen Sil",
            "✏️ Güncel Fiyat / Bakiye Düzenle");

        if (eylem == "🗑️ Varlığı Tamamen Sil")
        {
            bool onay = await DisplayAlert("Varlık Silinsin mi?",
                $"{varlik.AltTurOrHisse} ({varlik.Platform}) varlığını portföyünüzden tamamen silmek istediğinize emin misiniz?",
                "Evet, Sil", "Hayır");

            if (onay)
            {
                await _dbService.DeleteAssetAsync(varlik);
                await DisplayAlert("Silindi", "Varlık başarıyla portföyünüzden kaldırıldı.", "Tamam");
                await VerileriYukle();
            }
        }
        else if (eylem == "✏️ Güncel Fiyat / Bakiye Düzenle")
        {
            double karZarar = varlik.GuncelBakiye - varlik.YatirilanAnaPara;
            string durumMesaji = karZarar >= 0
                ? $"Mevcut Kâr: +₺{karZarar:N2}"
                : $"Mevcut Zarar: -₺{Math.Abs(karZarar):N2}";

            string yeniBakiyeStr = await DisplayPromptAsync(
                $"{varlik.AltTurOrHisse} Güncelle",
                $"Giriş Parası: ₺{varlik.YatirilanAnaPara:N2}\n{durumMesaji}\n\nYeni Güncel Toplam Değerini Girin (₺):",
                "Güncelle", "İptal",
                initialValue: varlik.GuncelBakiye.ToString(),
                keyboard: Keyboard.Numeric);

            if (double.TryParse(yeniBakiyeStr, out double yeniBakiye) && yeniBakiye >= 0)
            {
                if (yeniBakiye == 0)
                {
                    bool silOnay = await DisplayAlert("Bakiye Sıfırlandı", "Bakiyeyi 0 yaptınız. Bu varlık portföyden silinsin mi?", "Evet, Sil", "Hayır, 0 Olarak Tut");
                    if (silOnay)
                    {
                        await _dbService.DeleteAssetAsync(varlik);
                        await VerileriYukle();
                        return;
                    }
                }

                varlik.GuncelBakiye = yeniBakiye;
                await _dbService.UpdateAssetAsync(varlik);

                double yeniKarZarar = varlik.GuncelBakiye - varlik.YatirilanAnaPara;
                string sonDurum = yeniKarZarar >= 0
                    ? $"+₺{yeniKarZarar:N2} kârdasınız."
                    : $"-₺{Math.Abs(yeniKarZarar):N2} zarardasınız.";

                await DisplayAlert("Başarılı", $"Varlık bakiyesi güncellendi!\n{sonDurum}", "Tamam");
                await VerileriYukle();
            }
        }
    }

    private async void OnKapatClicked(object sender, EventArgs e)
    {
        if (Navigation != null)
        {
            await Navigation.PopModalAsync();
        }
    }
}