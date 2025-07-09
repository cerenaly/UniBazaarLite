using Microsoft.AspNetCore.Mvc.Filters; // Filtre arayüzleri için gerekli.
using System.Diagnostics; // Performans ölçümü için Stopwatch kullanır.
using Microsoft.Extensions.Logging; // Loglama işlevselliği için gerekli.

// BURADAKİ KOD BLOKLARI Mustafa Kösem(05200000032) TARAFINDAN YAZILMIŞTIR

namespace UniBazaarLite.Filters
{
    // Hem MVC Action'larını hem de Razor Page'lerini izleyen bir filtre sınıfı.
    public class LogActivityFilter : IActionFilter, IPageFilter
    {
        private readonly ILogger<LogActivityFilter> _logger; // Loglama servisi.
        private Stopwatch _stopwatch; // İşlem süresini ölçmek için kronometre.

        // Constructorımız ILogger servisini enjekte eder ve Stopwatch'ı başlatır.
        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }

        // Bir Action metodu çalışmadan hemen önce çalışır. (MVC Controller'lar için)
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Start(); // Kronometreyi başlatır.
            _logger.LogInformation($"[LogActivityFilter] Action başlatılıyor: {context.ActionDescriptor.DisplayName} (Path: {context.HttpContext.Request.Path}, Method: {context.HttpContext.Request.Method})"); // Loglama mesajını yazar.
        }

        // Bir Action metodu çalıştıktan hemen sonra çalışır. (MVC Controller'lar için)
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop(); // Kronometreyi durdurur.
            Log(context.ActionDescriptor.DisplayName, _stopwatch.ElapsedMilliseconds); // Loglama yapar.
            _stopwatch.Reset(); // Kronometreyi sıfırlar.
        }

        // Bir Razor Page Handler'ı seçildikten sonra çalışır.(Razor Pages için)
        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {

        }

        // Bir Razor Page Handler'ı çalışmadan hemen önce çalışır.(Razor Pages için)
        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            _stopwatch.Start(); // Kronometreyi başlatır.
        }

        // Bir Razor Page Handler'ı çalıştıktan hemen sonra çalışır.
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            _stopwatch.Stop(); // Kronometreyi durdurur.
            Log(context.ActionDescriptor.DisplayName, _stopwatch.ElapsedMilliseconds); // Loglama yapar.
            _stopwatch.Reset(); // Kronometreyi sıfırlar.
        }

        // Log mesajını oluşturan yardımcı metot.
        private void Log(string? pageName, long elapsedMilliseconds)
        {
            _logger.LogInformation("==> '{PageName}' sayfası(linki) {ElapsedMilliseconds}ms içinde çalıştırıldı.", pageName, elapsedMilliseconds); // Loglama mesajını yazar.
        }
    }
}