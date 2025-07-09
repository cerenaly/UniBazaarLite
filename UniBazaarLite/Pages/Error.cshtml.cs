using Microsoft.AspNetCore.Mvc; // MVC'ye özgü nitelikleri kullanmak için.
using Microsoft.AspNetCore.Mvc.RazorPages; // Razor Sayfalarý için temel kütüphaneyi içe aktarýr.
using System.Diagnostics;

// BURADAKÝ KOD BLOKLARI Mustafa Kösem(05200000032) TARAFINDAN YAZILMIÞTIR

namespace UniBazaarLite.Pages
{
    // Yanýtýn önbelleðe alýnmamasýný saðlar, her zaman yeni bir yanýt gönderilir.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        // Ýstek için benzersiz kimlik.
        public string? RequestId { get; set; }
        // RequestId boþ deðilse true döner, sayfa üzerinde gösterilip gösterilmeyeceðini kontrol eder.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Hata durum kodu
        public string? ErrorStatusCode { get; set; }
        // Hata mesajý.
        public string? ErrorMessage { get; set; }

        // Sayfaya GET isteði yapýldýðýnda çalýþan metot.
        // Ýsteðe baðlý olarak bir 'code' parametresi (HTTP durum kodu) alabilir.
        public void OnGet(string? code)
        {
            // Mevcut isteðin kimliðini alýr.
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ErrorStatusCode = code;

            // Gelen 'code' parametresine göre kullanýcýya gösterilecek hata mesajýný belirler.
            switch (code)
            {
                case "404": // Sayfa bulunamadý hatasý.
                    ErrorMessage = "Ulaþmaya çalýþtýðýnýz sayfa bulunamadý. Lütfen adresi kontrol edip tekrar deneyin.";
                    break;
                default: // Tanýmlanmamýþ veya genel hata durumu.
                    ErrorMessage = "Ýsteðiniz iþlenirken beklenmedik bir hata oluþtu.";
                    break;
            }
        }
    }
}