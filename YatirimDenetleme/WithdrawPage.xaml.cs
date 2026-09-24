namespace YatirimDenetleme;

public partial class WithdrawPage : ContentPage
{
    private DatabaseService _dbService;
    private List<Asset> _varlikListesi;

    public WithdrawPage()
    {
        InitializeComponent();
        _dbService = new DatabaseService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VarliklariYukle();
    }

    private async Task VarliklariYukle()
    {
        _varlikListesi = await _dbService.GetAssetsAsync();

        if (_varlikListesi == null || _varlikListesi.Count == 0)
        {
            LblBosUyari.IsVisible = true;
            CvVarliklar.IsVisible = false;
        }
        else
        {
            LblBosUyari.IsVisible = false;
            CvVarliklar.IsVisible = true;
            CvVarliklar.ItemsSource = _varlikListesi;
        }
    }

    private async void OnVarlikTapped(object sender, EventArgs e)
    {
        var border = sender as Border;
        var varlik = border?.BindingContext as Asset;

        if (varlik == null) return;

        string secim = await DisplayActionSheet(
            $"{varlik.AltTurOrHisse} - İşlem Seçin",
            "İptal", null,
            "Kârdan Para Çek", "Genel Para Çek");

        if (secim == "Kârdan Para Çek")
        {
            await KarCekIslemi(varlik);
        }
        else if (secim == "Genel Para Çek")
        {
            await GenelCekIslemi(varlik);
        }
    }

    private async Task KarCekIslemi(Asset varlik)
    {
        double karTutari = varlik.GuncelBakiye - varlik.YatirilanAnaPara;

        if (karTutari <= 0)
        {
            await DisplayAlert("Çekim Yapılamaz", "Bu varlıkta henüz kâr bulunmuyor veya zarardasınız.", "Tamam");
            return;
        }

        if (varlik.AnaKategori == "Fiziksel" && varlik.AltTurOrHisse == "Altın")
        {
            double mevcutAdet = ExtractAdet(varlik.EkBilgi);
            double birimBakiye = varlik.GuncelBakiye / (mevcutAdet > 0 ? mevcutAdet : 1);
            double birimGiris = varlik.YatirilanAnaPara / (mevcutAdet > 0 ? mevcutAdet : 1);
            double birimKar = birimBakiye - birimGiris;

            if (karTutari < birimBakiye)
            {
                await DisplayAlert("Yetersiz Kâr", $"1 adet altın bozdurabilmek için kârın en az 1 adet değerine (₺{birimBakiye:N0}) eşit olması gerekir. Mevcut Kâr: ₺{karTutari:N0}", "Tamam");
                return;
            }

            string adetInput = await DisplayPromptAsync("Kârdan Çekim (Adet)",
                $"Çekilebilecek Maksimum Kâr: ₺{karTutari:N2}\nMevcut Adet: {mevcutAdet}",
                "Çek", "İptal", keyboard: Keyboard.Numeric);

            if (double.TryParse(adetInput, out double cekilecekAdet) && cekilecekAdet > 0)
            {
                double toplamTutarCekilecek = cekilecekAdet * birimBakiye;
                if (toplamTutarCekilecek > karTutari)
                {
                    await DisplayAlert("Hata", "Çekilmek istenen adet kâr tutarını aşıyor!", "Tamam");
                    return;
                }

                double oran = (mevcutAdet - cekilecekAdet) / mevcutAdet;
                varlik.YatirilanAnaPara *= oran;
                varlik.GuncelBakiye -= toplamTutarCekilecek;
                varlik.EkBilgi = $"{mevcutAdet - cekilecekAdet} Adet";

                await GuncelleVeKapat(varlik);
            }
        }
        else
        {
            string input = await DisplayPromptAsync("Kârdan Çekim",
                $"Çekilebilecek Maksimum Kâr: ₺{karTutari:N2}",
                "Çek", "İptal", keyboard: Keyboard.Numeric);

            if (double.TryParse(input, out double cekilen) && cekilen > 0)
            {
                if (cekilen > karTutari)
                {
                    await DisplayAlert("Hata", $"Maksimum ₺{karTutari:N2} çekebilirsiniz!", "Tamam");
                    return;
                }

                varlik.GuncelBakiye -= cekilen;
                await GuncelleVeKapat(varlik);
            }
        }
    }

    private async Task GenelCekIslemi(Asset varlik)
    {
        if (varlik.AnaKategori == "Fiziksel" && varlik.AltTurOrHisse == "Altın")
        {
            double mevcutAdet = ExtractAdet(varlik.EkBilgi);

            string adetInput = await DisplayPromptAsync("Genel Para Çek (Adet)",
                $"Maksimum Çekilebilir Adet: {mevcutAdet}",
                "Çek", "İptal", keyboard: Keyboard.Numeric);

            if (double.TryParse(adetInput, out double cekilecekAdet) && cekilecekAdet > 0)
            {
                if (cekilecekAdet > mevcutAdet)
                {
                    await DisplayAlert("Hata", "Mevcut adetten fazla çekemezsiniz!", "Tamam");
                    return;
                }

                double oran = (mevcutAdet - cekilecekAdet) / mevcutAdet;
                varlik.YatirilanAnaPara *= oran;
                varlik.GuncelBakiye *= oran;
                varlik.EkBilgi = $"{mevcutAdet - cekilecekAdet} Adet";

                await GuncelleVeKapat(varlik);
            }
        }
        else
        {
            string input = await DisplayPromptAsync("Genel Para Çek",
                $"Çekilebilecek Maksimum Tutar: ₺{varlik.GuncelBakiye:N2}",
                "Çek", "İptal", keyboard: Keyboard.Numeric);

            if (double.TryParse(input, out double cekilen) && cekilen > 0)
            {
                if (cekilen > varlik.GuncelBakiye)
                {
                    await DisplayAlert("Hata", "Mevcut bakiyeden fazla para çekemezsiniz!", "Tamam");
                    return;
                }

                double oran = (varlik.GuncelBakiye - cekilen) / varlik.GuncelBakiye;
                varlik.YatirilanAnaPara *= oran;
                varlik.GuncelBakiye -= cekilen;

                await GuncelleVeKapat(varlik);
            }
        }
    }

    private async Task GuncelleVeKapat(Asset varlik)
    {
        if (varlik.GuncelBakiye <= 0)
        {
            await _dbService.DeleteAssetAsync(varlik);
        }
        else
        {
            await _dbService.UpdateAssetAsync(varlik);
        }

        await DisplayAlert("Başarılı", "Çekim işlemi yapıldı, varlık güncellendi.", "Tamam");
        await VarliklariYukle();
    }

    private async void OnKapatClicked(object sender, EventArgs e)
    {
        if (Navigation != null)
        {
            await Navigation.PopModalAsync();
        }
    }
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
}