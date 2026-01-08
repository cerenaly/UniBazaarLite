using Microsoft.AspNetCore.Mvc; // MVC'ye özgü nitelikleri kullanmak için.
using Microsoft.AspNetCore.Mvc.RazorPages; // Razor Sayfaları için temel kütüphaneyi içe aktarır.
using System.Diagnostics;

namespace UniBazaarLite.Pages
{
    // Yanıtın önbelleğe alınmamasını sağlar, her zaman yeni bir yanıt gönderilir.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        // İstek için benzersiz kimlik.
        public string? RequestId { get; set; }
        // RequestId boş değilse true döner, sayfa üzerinde gösterilip gösterilmeyeceğini kontrol eder.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Hata durum kodu
        public string? ErrorStatusCode { get; set; }
        // Hata mesajı.
        public string? ErrorMessage { get; set; }

        // Sayfaya GET isteği yapıldığında çalışan metot.
        // İsteğe bağlı olarak bir 'code' parametresi (HTTP durum kodu) alabilir.
        public void OnGet(string? code)
        {
            // Mevcut isteğin kimliğini alır.
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ErrorStatusCode = code;

            // Gelen 'code' parametresine göre kullanıcıya gösterilecek hata mesajını belirler.
            switch (code)
            {
                case "404": // Sayfa bulunamadı hatası.
                    ErrorMessage = "Ulaşmaya çalıştığınız sayfa bulunamadı. Lütfen adresi kontrol edip tekrar deneyin.";
                    break;
                default: // Tanımlanmamış veya genel hata durumu.
                    ErrorMessage = "İsteğiniz işlenirken beklenmedik bir hata oluştu.";
                    break;
            }
        }
    }
}
