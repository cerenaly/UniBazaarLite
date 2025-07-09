// Gerekli ASP.NET Core ve proje kütüphanelerini ekliyoruz.
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniBazaarLite.Data;
using UniBazaarLite.Models;

// BURADAKÝ KOD BLOKLARI Ceren Alyaðýz(05200000121) TARAFINDAN YAZILMIÞTIR

namespace UniBazaarLite.Pages.Events
{
    // Etkinlik listeleme sayfasýnýn arka plan kodunu içeren sýnýf.
    public class IndexModel : PageModel
    {
        // Veri deposuna eriþim için 'readonly' bir alan.
        private readonly IUniBazaarRepository _repository;
        // Sayfada gösterilecek olan etkinlik listesi.
        // 'get' public, 'private set' ise bu listenin sadece bu sýnýf içinden atanabileceðini belirtir.
        public List<Event> Events { get; private set; } = new List<Event>();

        // Sýnýf oluþturulurken repository enjekte eder.
        public IndexModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET isteði yapýldýðýnda (sayfa ilk yüklendiðinde) çalýþýr.
        public void OnGet()
        {
            // Repositorydeki tüm etkinlikleri çeker ve 'Events' listesine atar.
            // Bu liste, .cshtml dosyasýnda kullanýlacaktýr.
            Events = _repository.GetAllEvents();
        }
    }
}