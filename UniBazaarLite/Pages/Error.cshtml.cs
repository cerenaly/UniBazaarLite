using Microsoft.AspNetCore.Mvc; // MVC'ye �zg� nitelikleri kullanmak i�in.
using Microsoft.AspNetCore.Mvc.RazorPages; // Razor Sayfalar� i�in temel k�t�phaneyi i�e aktar�r.
using System.Diagnostics;

// BURADAK� KOD BLOKLARI Mustafa K�sem(05200000032) TARAFINDAN YAZILMI�TIR

namespace UniBazaarLite.Pages
{
    // Yan�t�n �nbelle�e al�nmamas�n� sa�lar, her zaman yeni bir yan�t g�nderilir.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        // �stek i�in benzersiz kimlik.
        public string? RequestId { get; set; }
        // RequestId bo� de�ilse true d�ner, sayfa �zerinde g�sterilip g�sterilmeyece�ini kontrol eder.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Hata durum kodu
        public string? ErrorStatusCode { get; set; }
        // Hata mesaj�.
        public string? ErrorMessage { get; set; }

        // Sayfaya GET iste�i yap�ld���nda �al��an metot.
        // �ste�e ba�l� olarak bir 'code' parametresi (HTTP durum kodu) alabilir.
        public void OnGet(string? code)
        {
            // Mevcut iste�in kimli�ini al�r.
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ErrorStatusCode = code;

            // Gelen 'code' parametresine g�re kullan�c�ya g�sterilecek hata mesaj�n� belirler.
            switch (code)
            {
                case "404": // Sayfa bulunamad� hatas�.
                    ErrorMessage = "Ula�maya �al��t���n�z sayfa bulunamad�. L�tfen adresi kontrol edip tekrar deneyin.";
                    break;
                default: // Tan�mlanmam�� veya genel hata durumu.
                    ErrorMessage = "�ste�iniz i�lenirken beklenmedik bir hata olu�tu.";
                    break;
            }
        }
    }
}