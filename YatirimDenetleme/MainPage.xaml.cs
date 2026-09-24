namespace YatirimDenetleme;

public partial class MainPage : ContentPage
{
    private DatabaseService _dbService;

    public MainPage()
    {
        InitializeComponent();
        _dbService = new DatabaseService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VerileriYukle();
    }

    private async Task VerileriYukle()
    {
        var varliklar = await _dbService.GetAssetsAsync();

        double toplamAnaPara = 0;
        double toplamBakiye = 0;

        if (varliklar != null && varliklar.Count > 0)
        {
            foreach (var item in varliklar)
            {
                toplamAnaPara += item.YatirilanAnaPara;
                toplamBakiye += item.GuncelBakiye;
            }

            // 1. Kategorilere Göre Grupla (Fiziksel, Hisse, Katılım Bankası)
            var kategoriGruplari = varliklar
                .GroupBy(v => string.IsNullOrEmpty(v.AnaKategori) ? "Diğer" : v.AnaKategori)
                .Select(g => new KategoriOzet
                {
                    KategoriAdi = g.Key,
                    ElemanSayisi = g.Count(),
                    ToplamBakiye = g.Sum(x => x.GuncelBakiye),
                    ToplamAnaPara = g.Sum(x => x.YatirilanAnaPara)
                }).ToList();

            CollectionViewKategoriler.ItemsSource = kategoriGruplari;

            // 2. Pasta Grafiği Çizdir
            PieGraphicsView.Drawable = new PieChartDrawable(kategoriGruplari, toplamBakiye);
            PieGraphicsView.Invalidate();

            // 3. Renk Açıklama Kutularını (Legend) Oluştur
            LegendContainer.Children.Clear();
            var renkler = PieChartDrawable.RenkPaleti;
            int colorIndex = 0;

            foreach (var kat in kategoriGruplari)
            {
                var renk = renkler[colorIndex % renkler.Length];

                var itemStack = new HorizontalStackLayout { Spacing = 5, Margin = new Thickness(5) };
                itemStack.Children.Add(new BoxView { Color = renk, WidthRequest = 12, HeightRequest = 12, VerticalOptions = LayoutOptions.Center });
                itemStack.Children.Add(new Label { Text = $"{kat.KategoriAdi} (%{(toplamBakiye > 0 ? (kat.ToplamBakiye / toplamBakiye * 100) : 0):N0})", TextColor = Colors.White, FontSize = 12 });

                LegendContainer.Children.Add(itemStack);
                colorIndex++;
            }
        }
        else
        {
            CollectionViewKategoriler.ItemsSource = null;
            PieGraphicsView.Drawable = null;
            LegendContainer.Children.Clear();
        }

        // Üst Özet Bilgileri
        LblAnaPara.Text = $"₺{toplamAnaPara:N2}";
        LblToplamBakiye.Text = $"₺{toplamBakiye:N2}";

        double karZarar = toplamBakiye - toplamAnaPara;
        double oran = toplamAnaPara > 0 ? (karZarar / toplamAnaPara) * 100 : 0;

        LblNetKarZarar.Text = $"{(karZarar >= 0 ? "+" : "")}₺{karZarar:N2} (%{oran:N1})";
        LblNetKarZarar.TextColor = karZarar >= 0 ? Color.FromArgb("#27AE60") : Color.FromArgb("#E74C3C");
    }

    private async void OnParaEkleClicked(object sender, EventArgs e)
    {
        if (Navigation != null)
        {
            await Navigation.PushModalAsync(new AddAssetPage());
        }
    }

    private async void OnParaCekClicked(object sender, EventArgs e)
    {
        if (Navigation != null)
        {
            await Navigation.PushModalAsync(new WithdrawPage());
        }
    }

    private async void OnKurGuncelleClicked(object sender, EventArgs e)
    {
        BtnKurGuncelle.IsEnabled = false;
        BtnKurGuncelle.Text = "⏳ Canlı Kurlar Çekiliyor...";

        GoldService kurServisi = new GoldService();
        KurModel guncelKurlar = await kurServisi.TumKurlariGetirAsync();

        if (guncelKurlar.Dolar > 0 || guncelKurlar.GramAltin > 0)
        {
            var tumVarliklar = await _dbService.GetAssetsAsync();

            // Yalnızca Fiziksel Varlıkları Filtrele (Altın, Dolar, Euro)
            var fizikselVarliklar = tumVarliklar
                .Where(v => v.AnaKategori == "Fiziksel")
                .ToList();

            int guncellenenSayisi = 0;

            foreach (var varlik in fizikselVarliklar)
            {
                double adetOrMiktar = ExtractAdet(varlik.EkBilgi);

                if (varlik.AltTurOrHisse == "Altın" && guncelKurlar.GramAltin > 0)
                {
                    varlik.GuncelBakiye = adetOrMiktar * guncelKurlar.GramAltin;
                    await _dbService.UpdateAssetAsync(varlik);
                    guncellenenSayisi++;
                }
                else if (varlik.AltTurOrHisse == "Dolar" && guncelKurlar.Dolar > 0)
                {
                    varlik.GuncelBakiye = adetOrMiktar * guncelKurlar.Dolar;
                    await _dbService.UpdateAssetAsync(varlik);
                    guncellenenSayisi++;
                }
                else if (varlik.AltTurOrHisse == "Euro" && guncelKurlar.Euro > 0)
                {
                    varlik.GuncelBakiye = adetOrMiktar * guncelKurlar.Euro;
                    await _dbService.UpdateAssetAsync(varlik);
                    guncellenenSayisi++;
                }
            }

            await DisplayAlert("Kurlar Güncellendi 🚀",
                $"Anlık Kurlar:\n" +
                $"• Dolar: ₺{guncelKurlar.Dolar:N2}\n" +
                $"• Euro: ₺{guncelKurlar.Euro:N2}\n" +
                $"• Gram Altın: ₺{guncelKurlar.GramAltin:N2}\n\n" +
                $"{guncellenenSayisi} adet fiziksel varlığınız güncellendi!",
                "Tamam");

            await VerileriYukle();
        }
        else
        {
            await DisplayAlert("Hata", "Canlı kurlar çekilemedi. Bağlantınızı kontrol edin.", "Tamam");
        }

        BtnKurGuncelle.IsEnabled = true;
        BtnKurGuncelle.Text = "🔄 Canlı Kurları Güncelle (Altın / Dolar / Euro)";
    }

    // EkBilgi metninden Adet sayısını ayıklayan yardımcı metod
    private double ExtractAdet(string ekBilgi)
    {
        if (string.IsNullOrEmpty(ekBilgi)) return 1;
        var parts = ekBilgi.Split(' ');
        if (parts.Length > 0 && double.TryParse(parts[0], out double adet))
        {
            return adet;
        }
        return 1;
    }

    private async void OnKategoriSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is KategoriOzet secilen)
        {
            CollectionViewKategoriler.SelectedItem = null; // Seçimi temizle

            if (Navigation != null)
            {
                // Seçilen kategoriyi (örneğin "Hisse" veya "Fiziksel") alt sayfaya parametre olarak gönderiyoruz
                await Navigation.PushModalAsync(new SubCategoryDetailPage(secilen.KategoriAdi));
            }
        }
    }
}

// --- YARDIMCI MODELLER VE MAUI UYUMLU GRAFİK ÇİZİCİ ---

public class KategoriOzet
{
    public string KategoriAdi { get; set; } = string.Empty;
    public int ElemanSayisi { get; set; }
    public double ToplamBakiye { get; set; }
    public double ToplamAnaPara { get; set; }
}

public class PieChartDrawable : IDrawable
{
    private readonly List<KategoriOzet> _veri;
    private readonly double _toplam;

    public static readonly Color[] RenkPaleti = new Color[]
    {
        Color.FromArgb("#27AE60"), // Yeşil (Fiziksel)
        Color.FromArgb("#3498DB"), // Mavi (Hisse)
        Color.FromArgb("#F39C12"), // Turuncu (Katılım Bankası)
        Color.FromArgb("#9B59B6"), // Mor
        Color.FromArgb("#E74C3C")  // Kırmızı
    };

    public PieChartDrawable(List<KategoriOzet> veri, double toplam)
    {
        _veri = veri;
        _toplam = toplam;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (_veri == null || _veri.Count == 0 || _toplam <= 0)
            return;

        float cx = dirtyRect.Width / 2;
        float cy = dirtyRect.Height / 2;
        float radius = Math.Min(cx, cy) - 15;

        float startAngle = -90; // Saat 12 yönünden başla
        int colorIndex = 0;

        foreach (var item in _veri)
        {
            float sweepAngle = (float)((item.ToplamBakiye / _toplam) * 360);

            canvas.FillColor = RenkPaleti[colorIndex % RenkPaleti.Length];

            // MAUI UYUMLU PASTA DİLİMİ ÇİZİMİ (PathF Kullanarak)
            var path = new PathF();
            path.MoveTo(cx, cy);
            path.AddArc(cx - radius, cy - radius, cx + radius, cy + radius, startAngle, startAngle + sweepAngle, true);
            path.Close();

            canvas.FillPath(path);

            startAngle += sweepAngle;
            colorIndex++;
        }
    }
}