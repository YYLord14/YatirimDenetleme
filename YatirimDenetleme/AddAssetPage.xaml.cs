namespace YatirimDenetleme;

public partial class AddAssetPage : ContentPage
{
    private DatabaseService _dbService;

    public AddAssetPage()
    {
        InitializeComponent();
        _dbService = new DatabaseService();
    }

    // 1. Ana Kategori değiştiğinde Alt Seçenek listesini doldurur
    private void OnAnaKategoriChanged(object sender, EventArgs e)
    {
        var secilenKategori = PickerAnaKategori.SelectedItem as string;

        if (string.IsNullOrEmpty(secilenKategori))
            return;

        // Alt seçeneği temizle, aktif et ve layout panellerini gizle
        PickerAltSecenek.IsEnabled = true;
        PickerAltSecenek.SelectedItem = null;
        PickerAltSecenek.ItemsSource = null;

        LayoutMidas.IsVisible = false;
        LayoutZborsa.IsVisible = false;
        LayoutFiziksel.IsVisible = false;

        switch (secilenKategori)
        {
            case "Fiziksel":
                PickerAltSecenek.ItemsSource = new List<string> { "Altın", "Dolar", "Euro" };
                PickerAltSecenek.Title = "Varlık Seçin";
                break;

            case "Hisse":
                PickerAltSecenek.ItemsSource = new List<string> { "Midas", "Ziraat", "Yapı Kredi", "Kuveyt Türk" };
                PickerAltSecenek.Title = "Borsa / Aracı Kurum Seçin";
                break;

            case "Katılım Bankası":
                PickerAltSecenek.ItemsSource = new List<string> { "Ziraat Katılım", "Vakıf Katılım", "Kuveyt Katılım" };
                PickerAltSecenek.Title = "Katılım Bankası Seçin";
                break;
        }
    }

    // 2. Alt Seçenek (Kurum / Varlık) seçildiğinde ilgili giriş panelini gösterir
    private void OnAltSecenekChanged(object sender, EventArgs e)
    {
        var secilenKurum = PickerAltSecenek.SelectedItem as string;

        if (string.IsNullOrEmpty(secilenKurum))
            return;

        LayoutMidas.IsVisible = false;
        LayoutZborsa.IsVisible = false;
        LayoutFiziksel.IsVisible = false;

        if (secilenKurum == "Midas")
        {
            LayoutMidas.IsVisible = true;
        }
        else if (secilenKurum == "Altın" || secilenKurum == "Dolar" || secilenKurum == "Euro")
        {
            LayoutFiziksel.IsVisible = true;

            // Eğer Altın için adet/kur hesabı yapılacaksa paneli göster
            if (secilenKurum == "Altın")
            {
                LayoutAdetKurHesaplama.IsVisible = true;
                LayoutFizikselTutar.IsVisible = false;
            }
            else
            {
                LayoutAdetKurHesaplama.IsVisible = false;
                LayoutFizikselTutar.IsVisible = true;
            }
        }
        else
        {
            // Ziraat, Yapı Kredi, Kuveyt Türk veya Katılım Bankaları seçildiğinde strateji giriş panelini açar
            LayoutZborsa.IsVisible = true;
        }
    }

    private void OnMidasDolarChanged(object sender, TextChangedEventArgs e)
    {
        double.TryParse(EntryMidasDolar.Text, out double dolar);
        double.TryParse(EntryDolarKuru.Text, out double kur);
        double tl = dolar * kur;
        LblMidasTLKarsiligi.Text = $"₺{tl:N2}";
    }

    private void OnMiktarOrKurChanged(object sender, TextChangedEventArgs e)
    {
        double.TryParse(EntryAdet.Text, out double adet);
        double.TryParse(EntryAlisKuru.Text, out double kur);
        double toplam = adet * kur;
        LblOtomatikToplam.Text = $"₺{toplam:N2}";
    }

    // 3. Veritabanına kaydetme mantığı
    private async void OnKaydetClicked(object sender, EventArgs e)
    {
        if (PickerAnaKategori.SelectedItem == null)
        {
            await DisplayAlert("Uyarı", "Lütfen bir ana kategori seçin.", "Tamam");
            return;
        }

        if (PickerAltSecenek.SelectedItem == null)
        {
            await DisplayAlert("Uyarı", "Lütfen bir kurum veya varlık seçin.", "Tamam");
            return;
        }

        string anaKategori = PickerAnaKategori.SelectedItem.ToString();
        string altSecenek = PickerAltSecenek.SelectedItem.ToString();

        Asset yeniVarlik = new Asset
        {
            AnaKategori = anaKategori,
            Platform = altSecenek
        };

        if (altSecenek == "Midas")
        {
            if (string.IsNullOrWhiteSpace(EntryMidasHisse.Text))
            {
                await DisplayAlert("Uyarı", "Lütfen hisse adını girin.", "Tamam");
                return;
            }
            double.TryParse(EntryMidasDolar.Text, out double dolar);
            double.TryParse(EntryDolarKuru.Text, out double kur);
            double tlTutar = dolar * kur;

            yeniVarlik.AltTurOrHisse = EntryMidasHisse.Text.Trim();
            yeniVarlik.YatirilanAnaPara = tlTutar;
            yeniVarlik.GuncelBakiye = tlTutar;
            yeniVarlik.EkBilgi = $"${dolar:N0}";
        }
        else if (anaKategori == "Fiziksel")
        {
            double tlTutar = 0;
            string ekBilgi = "";

            if (altSecenek == "Altın")
            {
                double.TryParse(EntryAdet.Text, out double adet);
                double.TryParse(EntryAlisKuru.Text, out double kur);
                tlTutar = adet * kur;
                ekBilgi = $"{adet} Adet (@₺{kur:N0})";
            }
            else
            {
                double.TryParse(EntryFizikselTutar.Text, out tlTutar);
                ekBilgi = $"{altSecenek} Birikim";
            }

            yeniVarlik.AltTurOrHisse = altSecenek;
            yeniVarlik.YatirilanAnaPara = tlTutar;
            yeniVarlik.GuncelBakiye = tlTutar;
            yeniVarlik.EkBilgi = ekBilgi;
        }
        else // Hisse (Ziraat, Yapı Kredi vb.) veya Katılım Bankaları
        {
            if (PickerZborsaStrateji.SelectedItem == null)
            {
                await DisplayAlert("Uyarı", "Lütfen yatırım stratejisini seçin.", "Tamam");
                return;
            }
            double.TryParse(EntryZborsaTutar.Text, out double tutar);

            yeniVarlik.AltTurOrHisse = PickerZborsaStrateji.SelectedItem.ToString();
            yeniVarlik.YatirilanAnaPara = tutar;
            yeniVarlik.GuncelBakiye = tutar;
            yeniVarlik.EkBilgi = $"{altSecenek} Portföyü";
        }

        await _dbService.AddAssetAsync(yeniVarlik);
        await Navigation.PopModalAsync();
    }

    private async void OnIptalClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}