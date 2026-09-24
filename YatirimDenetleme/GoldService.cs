using System.Text.Json;
using HtmlAgilityPack;

namespace YatirimDenetleme;

public class KurModel
{
    public double Dolar { get; set; }
    public double Euro { get; set; }
    public double GramAltin { get; set; }
}

public class GoldService
{
    private static readonly HttpClient _httpClient = new HttpClient();

    // Hem Döviz Kurlarını (API) Hem Altın Fiyatını Çeken Ana Metod
    public async Task<KurModel> TumKurlariGetirAsync()
    {
        var sonuc = new KurModel();

        // 1. Dolar ve Euro Çekme (ExchangeRate-API)
        try
        {
            string dovizUrl = "https://open.er-api.com/v6/latest/USD";
            string response = await _httpClient.GetStringAsync(dovizUrl);

            using var doc = JsonDocument.Parse(response);
            var rates = doc.RootElement.GetProperty("rates");

            // 1 USD kaç TRY?
            if (rates.TryGetProperty("TRY", out var tryRate))
            {
                sonuc.Dolar = tryRate.GetDouble();
            }

            // EUR / TRY Hesabı (Cross Rate)
            if (rates.TryGetProperty("EUR", out var eurRate) && sonuc.Dolar > 0)
            {
                sonuc.Euro = sonuc.Dolar / eurRate.GetDouble();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Döviz Çekme Hatası: {ex.Message}");
        }

        // 2. Gram Altın Çekme (Malatya Kuyumcular Odası)
        sonuc.GramAltin = await GetGramAltinFiyatiAsync();

        return sonuc;
    }

    private async Task<double> GetGramAltinFiyatiAsync()
    {
        try
        {
            string url = "https://malatyakuyumcular.net/";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            string html = await _httpClient.GetStringAsync(url);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var nodes = htmlDoc.DocumentNode.SelectNodes("//td | //div | //span");

            if (nodes != null)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    string text = nodes[i].InnerText.Trim();
                    if (text.Contains("24 Ayar") || text.Contains("Gram Altın") || text.Contains("HAS ALTIN"))
                    {
                        for (int j = 1; j <= 3; j++)
                        {
                            if (i + j < nodes.Count)
                            {
                                string potansiyelFiyat = nodes[i + j].InnerText.Trim();
                                potansiyelFiyat = potansiyelFiyat.Replace(".", "").Replace("₺", "").Replace("TL", "").Replace(",", ".").Trim();

                                if (double.TryParse(potansiyelFiyat, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double fiyat) && fiyat > 1000)
                                {
                                    return fiyat;
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Altın Çekme Hatası: {ex.Message}");
        }

        return 0;
    }
}