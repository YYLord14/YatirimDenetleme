namespace YatirimDenetleme;

public partial class AssetDetailPage : ContentPage
{
    private Asset _asset;
    private DatabaseService _dbService;
    private const double DOLAR_KURU = 47.0;

    public AssetDetailPage(Asset asset)
    {
        InitializeComponent();
        _asset = asset;
        _dbService = new DatabaseService();

        EkraniDoldur();
    }

    private void EkraniDoldur()
    {
        Title = $"{_asset.AltTurOrHisse} Düzenle";
        LblBaslik.Text = $"{_asset.AnaKategori} - {_asset.AltTurOrHisse}";
        LblAltBilgi.Text = _asset.EkBilgi ?? "";

        LblAnaPara.Text = $"₺{_asset.YatirilanAnaPara:N2}";
        LblGuncelBakiye.Text = $"₺{_asset.GuncelBakiye:N2}";

        // Kâr / Zarar Hesaplama
        double fark = _asset.GuncelBakiye - _asset.YatirilanAnaPara;
        double oran = _asset.YatirilanAnaPara > 0 ? (fark / _asset.YatirilanAnaPara) * 100 : 0;

        if (fark >= 0)
        {
            LblKarZarar.Text = $"+₺{fark:N2} (%{oran:N1})";
            LblKarZarar.TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#27AE60");
        }
        else
        {
            LblKarZarar.Text = $"-₺{Math.Abs(fark):N2} (%{Math.Abs(oran):N1})";
            LblKarZarar.TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#E74C3C");
        }

        // Midas veya TL Ayrımı
        if (_asset.AnaKategori == "Midas")
        {
            LblGirisAciklama.Text = $"Hissenin Güncel Toplam Dolar ($) Değerini Girin (Kur: ₺{DOLAR_KURU}):";
            double mevcutDolar = _asset.GuncelBakiye / DOLAR_KURU;
            EntryGuncelDeger.Text = mevcutDolar > 0 ? mevcutDolar.ToString("F2") : "";
        }
        else
        {
            LblGirisAciklama.Text = "Hisse / Varlığın Güncel Toplam TL (₺) Değerini Girin:";
            EntryGuncelDeger.Text = _asset.GuncelBakiye > 0 ? _asset.GuncelBakiye.ToString("F2") : "";
        }
    }

    private async void OnGuncelleClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryGuncelDeger.Text))
        {
            await DisplayAlert("Hata", "Lütfen güncel bir değer girin.", "Tamam");
            return;
        }

        if (double.TryParse(EntryGuncelDeger.Text.Replace('.', ','), out double girilenDeger))
        {
            if (_asset.AnaKategori == "Midas")
            {
                // Midas için Dolar cinsinden girilen değeri TL'ye çevirip bakiyeye işliyoruz
                _asset.GuncelBakiye = girilenDeger * DOLAR_KURU;
                _asset.EkBilgi = $"${girilenDeger:N2} (Kur: ₺{DOLAR_KURU})";
            }
            else
            {
                // Zborsa ve diğerleri için doğrudan TL olarak güncelliyoruz
                _asset.GuncelBakiye = girilenDeger;
            }

            await _dbService.UpdateAssetAsync(_asset);
            await DisplayAlert("Başarılı", "Güncel bakiye kaydedildi!", "Tamam");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Hata", "Lütfen geçerli bir sayısal değer girin.", "Tamam");
        }
    }

    private async void OnSilClicked(object sender, EventArgs e)
    {
        bool cevap = await DisplayAlert("Onay", $"{_asset.AltTurOrHisse} kaydını silmek istediğinize emin misiniz?", "Evet", "Hayır");
        if (cevap)
        {
            await _dbService.DeleteAssetAsync(_asset);
            await Navigation.PopAsync();
        }
    }
}