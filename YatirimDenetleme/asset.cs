using SQLite;

namespace YatirimDenetleme
{
    [Table("Assets")]
    public class Asset
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
            
        // Ana Kategori: "Midas", "Zborsa", "Fiziksel"
        public string AnaKategori { get; set; } = string.Empty;

        // Detay İsmi: "AAPL", "Kısa Vade", "Altın", "Halka Arz" vb.
        public string AltTurOrHisse { get; set; } = string.Empty;

        // TL Cinsinden Yatırılan Tutarlar
        public double YatirilanAnaPara { get; set; }
        public double GuncelBakiye { get; set; }

        // Ek Bilgi: "$65", "2 Adet (@₺2500)", "Zborsa Portföyü" vb.
        public string EkBilgi { get; set; } = string.Empty;

        public string Platform { get; set; } // "Fiziksel", "Midas", "ZBorsa"
    }
}