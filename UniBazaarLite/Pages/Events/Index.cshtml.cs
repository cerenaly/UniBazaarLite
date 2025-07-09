// Gerekli ASP.NET Core ve proje k�t�phanelerini ekliyoruz.
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniBazaarLite.Data;
using UniBazaarLite.Models;

// BURADAK� KOD BLOKLARI Ceren Alya��z(05200000121) TARAFINDAN YAZILMI�TIR

namespace UniBazaarLite.Pages.Events
{
    // Etkinlik listeleme sayfas�n�n arka plan kodunu i�eren s�n�f.
    public class IndexModel : PageModel
    {
        // Veri deposuna eri�im i�in 'readonly' bir alan.
        private readonly IUniBazaarRepository _repository;
        // Sayfada g�sterilecek olan etkinlik listesi.
        // 'get' public, 'private set' ise bu listenin sadece bu s�n�f i�inden atanabilece�ini belirtir.
        public List<Event> Events { get; private set; } = new List<Event>();

        // S�n�f olu�turulurken repository enjekte eder.
        public IndexModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET iste�i yap�ld���nda (sayfa ilk y�klendi�inde) �al���r.
        public void OnGet()
        {
            // Repositorydeki t�m etkinlikleri �eker ve 'Events' listesine atar.
            // Bu liste, .cshtml dosyas�nda kullan�lacakt�r.
            Events = _repository.GetAllEvents();
        }
    }
}