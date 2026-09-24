namespace YatirimDenetleme;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Sayfayı NavigationPage içine alıyoruz ki Modal/Push işlemleri çökmesin
        MainPage = new NavigationPage(new MainPage());
    }
}