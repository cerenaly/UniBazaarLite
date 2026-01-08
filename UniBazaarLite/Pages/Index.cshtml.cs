using Microsoft.AspNetCore.Mvc.RazorPages; // Razor Sayfaları için temel kütüphaneyi içe aktarır.
using Microsoft.Extensions.Logging; // Loglama işlevselliği için gerekli kütüphaneyi içe aktarır.

 
namespace UniBazaarLite.Pages
{
    // Ana sayfa (Index) için arka plan kod modelini tanımlar.
    public class IndexModel : PageModel
    {
        // Loglama için ILogger servisini tutar
        private readonly ILogger<IndexModel> _logger;

        // IndexModel oluşturulurken ILogger servisini Dependency Injection ile alır.
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // Sayfaya GET isteği yapıldığında (sayfa yüklendiğinde) çalışan metot.
        // Bu sayfada özel bir veri işlemi yapılmadığı için şu an boş bırakılmıştır.
        public void OnGet()
        {

        }
    }
}
