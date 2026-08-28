namespace YatirimDenetleme;

public partial class SubCategoryDetailPage : ContentPage
{
    private string _kategoriAdi;
    private DatabaseService _dbService;

    public SubCategoryDetailPage(string kategoriAdi)
    {
        InitializeComponent();
        _kategoriAdi = kategoriAdi;
        _dbService = new DatabaseService();
        Title = $"{_kategoriAdi} Detayı";
        LblBaslik.Text = $"{_kategoriAdi} Portföy Detayı";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var tumVarliklar = await _dbService.GetAssetsAsync();
        var altVarliklar = tumVarliklar.Where(v => v.AnaKategori == _kategoriAdi).ToList();

        CollectionViewAltVarliklar.ItemsSource = altVarliklar;

        double katAnaPara = altVarliklar.Sum(v => v.YatirilanAnaPara);
        double katBakiye = altVarliklar.Sum(v => v.GuncelBakiye);

        LblKatAnaPara.Text = $"₺{katAnaPara:N2}";
        LblKatToplamBakiye.Text = $"₺{katBakiye:N2}";

        // Detay Grafik Çizimi
        SubPieGraphicsView.Drawable = new SubPieDrawable(altVarliklar, katAnaPara);
        SubPieGraphicsView.Invalidate();
        LegendOlustur(altVarliklar, katAnaPara);
    }

    private void LegendOlustur(List<Asset> altVarliklar, double katAnaPara)
    {
        SubLegendContainer.Children.Clear();
        string[] renkler = { "#E74C3C", "#3498DB", "#2ECC71", "#F1C40F", "#9B59B6", "#E67E22" };

        int index = 0;
        foreach (var v in altVarliklar)
        {
            double yuzde = katAnaPara > 0 ? (v.YatirilanAnaPara / katAnaPara) * 100 : 0;
            string colorHex = renkler[index % renkler.Length];

            var itemStack = new HorizontalStackLayout { Spacing = 6, Margin = new Thickness(5) };
            var box = new BoxView { Color = Microsoft.Maui.Graphics.Color.FromArgb(colorHex), WidthRequest = 14, HeightRequest = 14, CornerRadius = 3, VerticalOptions = LayoutOptions.Center };
            var label = new Label { Text = $"{v.AltTurOrHisse} (%{yuzde:N1})", TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#CCCCCC"), FontSize = 13, VerticalOptions = LayoutOptions.Center };

            itemStack.Children.Add(box);
            itemStack.Children.Add(label);
            SubLegendContainer.Children.Add(itemStack);
            index++;
        }
    }

    // LISTEDEN BIR VARLIĞA/HISSEYE TIKLANDIĞINDA ÇALIŞACAK METOT
    private async void OnVarlikSelected(object sender, SelectionChangedEventArgs e)
    {
        var secilenVarlik = e.CurrentSelection.FirstOrDefault() as Asset;
        if (secilenVarlik == null) return;

        // Tıklama efektinden sonra seçili durumu temizle
        ((CollectionView)sender).SelectedItem = null;

        // Detay / Güncelleme / Silme sayfasına git
        await Navigation.PushAsync(new AssetDetailPage(secilenVarlik));
    }
}

public class SubPieDrawable : IDrawable
{
    private readonly List<Asset> _altVarliklar;
    private readonly double _katAnaPara;
    private readonly string[] _renkler = { "#E74C3C", "#3498DB", "#2ECC71", "#F1C40F", "#9B59B6", "#E67E22" };

    public SubPieDrawable(List<Asset> altVarliklar, double katAnaPara)
    {
        _altVarliklar = altVarliklar;
        _katAnaPara = katAnaPara;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (_katAnaPara <= 0 || _altVarliklar == null || _altVarliklar.Count == 0) return;

        float centerX = dirtyRect.Width / 2;
        float centerY = dirtyRect.Height / 2;
        float radius = Math.Min(centerX, centerY) - 15;

        float startAngle = -90;

        int index = 0;
        foreach (var v in _altVarliklar)
        {
            float sweepAngle = (float)((v.YatirilanAnaPara / _katAnaPara) * 360);
            string hex = _renkler[index % _renkler.Length];

            canvas.FillColor = Microsoft.Maui.Graphics.Color.FromArgb(hex);

            PathF path = new PathF();
            path.MoveTo(centerX, centerY);
            path.AddArc(centerX - radius, centerY - radius, centerX + radius, centerY + radius, startAngle, startAngle + sweepAngle, false);
            path.Close();

            canvas.FillPath(path);
            startAngle += sweepAngle;
            index++;
        }
    }
}