using Microsoft.AspNetCore.Mvc.RazorPages; // Razor Sayfalarý için temel kütüphaneyi içe aktarýr.
using Microsoft.Extensions.Logging; // Loglama iþlevselliði için gerekli kütüphaneyi içe aktarýr.

// BURADAKÝ KOD BLOKLARI Mustafa Kösem(05200000032) TARAFINDAN YAZILMIÞTIR

namespace UniBazaarLite.Pages
{
    // Ana sayfa (Index) için arka plan kod modelini tanýmlar.
    public class IndexModel : PageModel
    {
        // Loglama için ILogger servisini tutar
        private readonly ILogger<IndexModel> _logger;

        // IndexModel oluþturulurken ILogger servisini Dependency Injection ile alýr.
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // Sayfaya GET isteði yapýldýðýnda (sayfa yüklendiðinde) çalýþan metot.
        // Bu sayfada özel bir veri iþlemi yapýlmadýðý için þu an boþ býrakýlmýþtýr.
        public void OnGet()
        {

        }
    }
}