// Gerekli ASP.NET Core ve proje k�t�phanelerini ekliyoruz.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniBazaarLite.Data;
using UniBazaarLite.Models;

// BURADAK� KOD BLOKLARI Ceren Alya��z(05200000121) TARAFINDAN YAZILMI�TIR

namespace UniBazaarLite.Pages.Events
{
    // [Authorize] �zniteli�i, bu sayfan�n sadece giri� yapm�� kullan�c�lar
    // taraf�ndan eri�ilebilir olmas�n� sa�lar.
    [Authorize]
    public class CreateModel : PageModel
    {
        // Repository eri�im i�in 'readonly' bir alan.
        private readonly IUniBazaarRepository _repository;

        // [BindProperty], bu 'Event' nesnesinin formdan gelen verilerle
        // otomatik olarak doldurulaca��n� belirtir.
        [BindProperty]
        public Event Event { get; set; } = new();

        // S�n�f olu�turulurken repository enjekte eder.
        public CreateModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET iste�i yap�ld���nda (sayfa ilk y�klendi�inde) �al���r.
        public void OnGet()
        {
            // Formdaki tarih alan�n�, o anki tarih ve saat ile doldurur.
            Event.Date = DateTime.Now;
        }

        // Forma bir POST iste�i yap�ld���nda (form g�nderildi�inde) �al���r.
        public IActionResult OnPost()
        {
            // Modelin do�rulama kurallar�n�n ge�erli olup olmad���n� kontrol eder.
            if (!ModelState.IsValid)
            {
                // Ge�erli de�ilse, ayn� sayfay� hatalarla birlikte tekrar g�sterir.
                return Page();
            }

            // Haz�rlanan 'Event' nesnesini repository'e ekler.
            _repository.AddEvent(Event);

            // Kullan�c�ya bir sonraki sayfada g�sterilmek �zere bir ba�ar� mesaj� ayarlar.
            TempData["SuccessMessage"] = "Yeni etkinlik ba�ar�yla olu�turuldu!";

            // Kullan�c�y� etkinliklerin listelendi�i Index sayfas�na y�nlendirir.
            return RedirectToPage("/Events/Index");
        }
    }
}