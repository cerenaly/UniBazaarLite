// Gerekli ASP.NET Core ve proje k�t�phanelerini ekliyoruz.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniBazaarLite.Data;
using UniBazaarLite.Filters;
using UniBazaarLite.Models;

// BURADAK� KOD BLOKLARI Ceren Alya��z(05200000121) TARAFINDAN YAZILMI�TIR

namespace UniBazaarLite.Pages.Events
{
    // [Authorize], bu sayfan�n sadece giri� yapm�� kullan�c�lar taraf�ndan eri�ilebilir olmas�n� sa�lar.
    [Authorize]
    // [ValidateEntityExists<Event>], OnGet metodu �al��madan �nce, verilen 'id' ile bir
    // etkinli�in olup olmad���n� kontrol eden �zel filtremizdir.
    [ValidateEntityExists<Event>]
    public class EditModel : PageModel
    {
        // Repositroy eri�im i�in 'readonly' bir alan.
        private readonly IUniBazaarRepository _repository;

        // [BindProperty], bu 'Event' nesnesinin formdan gelen verilerle
        // otomatik olarak doldurulaca��n� belirtir.
        [BindProperty]
        public Event Event { get; set; } = new();

        // S�n�f olu�turulurken repository enjekte eder.
        public EditModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET iste�i yap�ld���nda (�rn: /Events/Edit/5) �al���r.
        public IActionResult OnGet(int id)
        {
            // Filtremiz kontrol� yapt��� i�in, buraya ula�t���m�zda etkinli�in var oldu�unu biliriz.
            // Repositoryden ilgili etkinli�i bulup 'Event' modelimize atar�z.
            var eventFromDb = _repository.GetEventById(id);
            Event = eventFromDb!;
            return Page();
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

            // Formdan gelen 'Event' modelini, repositorydeki g�ncelleme metoduna g�nderir.
            _repository.UpdateEvent(Event);

            // Kullan�c�ya bir sonraki sayfada g�sterilmek �zere bir ba�ar� mesaj� ayarlar.
            TempData["SuccessMessage"] = "Etkinlik ba�ar�yla g�ncellendi!";
            // Kullan�c�y�, ayn� klas�rdeki 'Index' sayfas�na y�nlendirir.
            return RedirectToPage("./Index");
        }
    }
}