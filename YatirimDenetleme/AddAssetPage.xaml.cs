namespace YatirimDenetleme;

public partial class AddAssetPage : ContentPage
{
    private DatabaseService _dbService;

    public AddAssetPage()
    {
        InitializeComponent();
        _dbService = new DatabaseService();
    }

    private void OnAnaKategoriChanged(object sender, EventArgs e)
    {
        int index = PickerAnaKategori.SelectedIndex;
        LayoutMidas.IsVisible = (index == 0);
        LayoutZborsa.IsVisible = (index == 1);
        LayoutFiziksel.IsVisible = (index == 2);
    }

    private void OnMidasDolarChanged(object sender, TextChangedEventArgs e)
    {
        double.TryParse(EntryMidasDolar.Text, out double dolar);
        double.TryParse(EntryDolarKuru.Text, out double kur);
        double tl = dolar * kur;
        LblMidasTLKarsiligi.Text = $"₺{tl:N2}";
    }

    private void OnFizikselTurChanged(object sender, EventArgs e)
    {
        string secilen = PickerFizikselTur.SelectedItem as string;
        if (secilen == "Altın" || secilen == "Gümüş")
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

    private void OnMiktarOrKurChanged(object sender, TextChangedEventArgs e)
    {
        double.TryParse(EntryAdet.Text, out double adet);
        double.TryParse(EntryAlisKuru.Text, out double kur);
        double toplam = adet * kur;
        LblOtomatikToplam.Text = $"₺{toplam:N2}";
    }

    private async void OnKaydetClicked(object sender, EventArgs e)
    {
        int catIndex = PickerAnaKategori.SelectedIndex;
        if (catIndex == -1)
        {
            await DisplayAlert("Uyarı", "Lütfen bir kategori seçin.", "Tamam");
            return;
        }

        Asset yeniVarlik = new Asset();

        if (catIndex == 0) // Midas
        {
            if (string.IsNullOrWhiteSpace(EntryMidasHisse.Text))
            {
                await DisplayAlert("Uyarı", "Lütfen hisse adını girin.", "Tamam");
                return;
            }
            double.TryParse(EntryMidasDolar.Text, out double dolar);
            double.TryParse(EntryDolarKuru.Text, out double kur);
            double tlTutar = dolar * kur;

            yeniVarlik.AnaKategori = "Midas";
            yeniVarlik.AltTurOrHisse = EntryMidasHisse.Text.Trim();
            yeniVarlik.YatirilanAnaPara = tlTutar;
            yeniVarlik.GuncelBakiye = tlTutar;
            yeniVarlik.EkBilgi = $"${dolar:N0}";
        }
        else if (catIndex == 1) // Zborsa
        {
            if (PickerZborsaStrateji.SelectedItem == null)
            {
                await DisplayAlert("Uyarı", "Lütfen Zborsa stratejisini seçin.", "Tamam");
                return;
            }
            double.TryParse(EntryZborsaTutar.Text, out double tutar);

            yeniVarlik.AnaKategori = "Zborsa";
            yeniVarlik.AltTurOrHisse = PickerZborsaStrateji.SelectedItem.ToString();
            yeniVarlik.YatirilanAnaPara = tutar;
            yeniVarlik.GuncelBakiye = tutar;
            yeniVarlik.EkBilgi = "Zborsa Portföyü";
        }
        else if (catIndex == 2) // Fiziksel
        {
            if (PickerFizikselTur.SelectedItem == null)
            {
                await DisplayAlert("Uyarı", "Fiziksel varlık türünü seçin.", "Tamam");
                return;
            }

            string tur = PickerFizikselTur.SelectedItem.ToString();
            double tlTutar = 0;
            string ekBilgi = "";

            if (tur == "Altın" || tur == "Gümüş")
            {
                double.TryParse(EntryAdet.Text, out double adet);
                double.TryParse(EntryAlisKuru.Text, out double kur);
                tlTutar = adet * kur;
                ekBilgi = $"{adet} Adet (@₺{kur:N0})";
            }
            else
            {
                double.TryParse(EntryFizikselTutar.Text, out tlTutar);
                ekBilgi = "Nakit Birikim";
            }

            yeniVarlik.AnaKategori = "Fiziksel";
            yeniVarlik.AltTurOrHisse = tur;
            yeniVarlik.YatirilanAnaPara = tlTutar;
            yeniVarlik.GuncelBakiye = tlTutar;
            yeniVarlik.EkBilgi = ekBilgi;
        }

        await _dbService.AddAssetAsync(yeniVarlik);
        await Navigation.PopModalAsync();
    }

    private async void OnIptalClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}