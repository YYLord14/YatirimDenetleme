namespace YatirimDenetleme;

public class CategoryGroup
{
    public string KategoriAdi { get; set; }
    public double ToplamAnaPara { get; set; }
    public double ToplamBakiye { get; set; }
    public int ElemanSayisi { get; set; }
}

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

        // 3 Ana Gruba Topluyoruz
        var gruplar = varliklar.GroupBy(v => v.AnaKategori ?? "Diğer")
            .Select(g => new CategoryGroup
            {
                KategoriAdi = g.Key,
                ToplamAnaPara = g.Sum(x => x.YatirilanAnaPara),
                ToplamBakiye = g.Sum(x => x.GuncelBakiye),
                ElemanSayisi = g.Count()
            }).ToList();

        CollectionViewKategoriler.ItemsSource = gruplar;

        double genelAnaPara = varliklar.Sum(v => v.YatirilanAnaPara);
        double genelBakiye = varliklar.Sum(v => v.GuncelBakiye);
        double netKarZarar = genelBakiye - genelAnaPara;
        double karOrani = genelAnaPara > 0 ? (netKarZarar / genelAnaPara) * 100 : 0;

        LblAnaPara.Text = $"₺{genelAnaPara:N2}";
        LblToplamBakiye.Text = $"₺{genelBakiye:N2}";

        if (netKarZarar >= 0)
        {
            LblNetKarZarar.Text = $"+₺{netKarZarar:N2} (%{karOrani:N1})";
            LblNetKarZarar.TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#27AE60");
        }
        else
        {
            LblNetKarZarar.Text = $"-₺{Math.Abs(netKarZarar):N2} (%{Math.Abs(karOrani):N1})";
            LblNetKarZarar.TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#E74C3C");
        }

        // Grafiği ve Legend Açıklamasını Oluştur
        PieGraphicsView.Drawable = new MacroPieDrawable(gruplar, genelAnaPara);
        PieGraphicsView.Invalidate();
        LegendOlustur(gruplar, genelAnaPara);
    }

    private void LegendOlustur(List<CategoryGroup> gruplar, double genelAnaPara)
    {
        LegendContainer.Children.Clear();
        var renkler = new Dictionary<string, string>
        {
            { "Midas", "#3498DB" },   // Mavi
            { "Zborsa", "#2ECC71" },  // Yeşil
            { "Fiziksel", "#9B59B6" } // Mor
        };

        foreach (var g in gruplar)
        {
            double yuzde = genelAnaPara > 0 ? (g.ToplamAnaPara / genelAnaPara) * 100 : 0;
            string colorHex = renkler.ContainsKey(g.KategoriAdi) ? renkler[g.KategoriAdi] : "#F1C40F";

            var itemStack = new HorizontalStackLayout { Spacing = 6, Margin = new Thickness(5) };
            var box = new BoxView { Color = Microsoft.Maui.Graphics.Color.FromArgb(colorHex), WidthRequest = 14, HeightRequest = 14, CornerRadius = 3, VerticalOptions = LayoutOptions.Center };
            var label = new Label { Text = $"{g.KategoriAdi} (%{yuzde:N1})", TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#CCCCCC"), FontSize = 13, VerticalOptions = LayoutOptions.Center };

            itemStack.Children.Add(box);
            itemStack.Children.Add(label);
            LegendContainer.Children.Add(itemStack);
        }
    }

    private async void OnParaEkleClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddAssetPage());
    }

    private async void OnKategoriSelected(object sender, SelectionChangedEventArgs e)
    {
        var secilen = e.CurrentSelection.FirstOrDefault() as CategoryGroup;
        if (secilen == null) return;

        ((CollectionView)sender).SelectedItem = null;

        // Kategoriye özel detay grafiği sayfasına geçiş
        await Navigation.PushAsync(new SubCategoryDetailPage(secilen.KategoriAdi));
    }
}

// Ana Grafik Çizicisi (Max 3 Renk)
public class MacroPieDrawable : IDrawable
{
    private readonly List<CategoryGroup> _gruplar;
    private readonly double _genelAnaPara;
    private readonly Dictionary<string, string> _renkler = new Dictionary<string, string>
    {
        { "Midas", "#3498DB" },
        { "Zborsa", "#2ECC71" },
        { "Fiziksel", "#9B59B6" }
    };

    public MacroPieDrawable(List<CategoryGroup> gruplar, double genelAnaPara)
    {
        _gruplar = gruplar;
        _genelAnaPara = genelAnaPara;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (_genelAnaPara <= 0 || _gruplar == null || _gruplar.Count == 0) return;

        float centerX = dirtyRect.Width / 2;
        float centerY = dirtyRect.Height / 2;
        float radius = Math.Min(centerX, centerY) - 15;

        float startAngle = -90;

        foreach (var g in _gruplar)
        {
            float sweepAngle = (float)((g.ToplamAnaPara / _genelAnaPara) * 360);
            string hex = _renkler.ContainsKey(g.KategoriAdi) ? _renkler[g.KategoriAdi] : "#F1C40F";

            canvas.FillColor = Microsoft.Maui.Graphics.Color.FromArgb(hex);

            PathF path = new PathF();
            path.MoveTo(centerX, centerY);
            path.AddArc(centerX - radius, centerY - radius, centerX + radius, centerY + radius, startAngle, startAngle + sweepAngle, false);
            path.Close();

            canvas.FillPath(path);
            startAngle += sweepAngle;
        }
    }
}