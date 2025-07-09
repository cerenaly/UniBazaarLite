using Microsoft.AspNetCore.Mvc.RazorPages; // Razor Sayfalar� i�in temel k�t�phaneyi i�e aktar�r.
using Microsoft.Extensions.Logging; // Loglama i�levselli�i i�in gerekli k�t�phaneyi i�e aktar�r.

// BURADAK� KOD BLOKLARI Mustafa K�sem(05200000032) TARAFINDAN YAZILMI�TIR

namespace UniBazaarLite.Pages
{
    // Ana sayfa (Index) i�in arka plan kod modelini tan�mlar.
    public class IndexModel : PageModel
    {
        // Loglama i�in ILogger servisini tutar
        private readonly ILogger<IndexModel> _logger;

        // IndexModel olu�turulurken ILogger servisini Dependency Injection ile al�r.
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // Sayfaya GET iste�i yap�ld���nda (sayfa y�klendi�inde) �al��an metot.
        // Bu sayfada �zel bir veri i�lemi yap�lmad��� i�in �u an bo� b�rak�lm��t�r.
        public void OnGet()
        {

        }
    }
}