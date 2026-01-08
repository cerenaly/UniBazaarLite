// Gerekli ASP.NET Core ve proje kütüphanelerini ekliyoruz.
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniBazaarLite.Data;
using UniBazaarLite.Models;


namespace UniBazaarLite.Pages.Events
{
    // Etkinlik listeleme sayfasının arka plan kodunu içeren sınıf.
    public class IndexModel : PageModel
    {
        // Veri deposuna erişim için 'readonly' bir alan.
        private readonly IUniBazaarRepository _repository;
        // Sayfada gösterilecek olan etkinlik listesi.
        // 'get' public, 'private set' ise bu listenin sadece bu sınıf içinden atanabileceğini belirtir.
        public List<Event> Events { get; private set; } = new List<Event>();

        // Sınıf oluşturulurken repository enjekte eder.
        public IndexModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET isteği yapıldığında (sayfa ilk yüklendiğinde) çalışır.
        public void OnGet()
        {
            // Repositorydeki tüm etkinlikleri çeker ve 'Events' listesine atar.
            // Bu liste, .cshtml dosyasında kullanılacaktır.
            Events = _repository.GetAllEvents();
        }
    }
}
