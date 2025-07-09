// Gerekli ASP.NET Core ve proje kütüphanelerini ekliyoruz.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniBazaarLite.Data;
using UniBazaarLite.Filters;
using UniBazaarLite.Models;

// BURADAKÝ KOD BLOKLARI Ceren Alyaðýz(05200000121) TARAFINDAN YAZILMIÞTIR

namespace UniBazaarLite.Pages.Events
{
    // [Authorize], bu sayfanýn sadece giriþ yapmýþ kullanýcýlar tarafýndan eriþilebilir olmasýný saðlar.
    [Authorize]
    // [ValidateEntityExists<Event>], OnGet metodu çalýþmadan önce, verilen 'id' ile bir
    // etkinliðin olup olmadýðýný kontrol eden özel filtremizdir.
    [ValidateEntityExists<Event>]
    public class EditModel : PageModel
    {
        // Repositroy eriþim için 'readonly' bir alan.
        private readonly IUniBazaarRepository _repository;

        // [BindProperty], bu 'Event' nesnesinin formdan gelen verilerle
        // otomatik olarak doldurulacaðýný belirtir.
        [BindProperty]
        public Event Event { get; set; } = new();

        // Sýnýf oluþturulurken repository enjekte eder.
        public EditModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET isteði yapýldýðýnda (örn: /Events/Edit/5) çalýþýr.
        public IActionResult OnGet(int id)
        {
            // Filtremiz kontrolü yaptýðý için, buraya ulaþtýðýmýzda etkinliðin var olduðunu biliriz.
            // Repositoryden ilgili etkinliði bulup 'Event' modelimize atarýz.
            var eventFromDb = _repository.GetEventById(id);
            Event = eventFromDb!;
            return Page();
        }

        // Forma bir POST isteði yapýldýðýnda (form gönderildiðinde) çalýþýr.
        public IActionResult OnPost()
        {
            // Modelin doðrulama kurallarýnýn geçerli olup olmadýðýný kontrol eder.
            if (!ModelState.IsValid)
            {
                // Geçerli deðilse, ayný sayfayý hatalarla birlikte tekrar gösterir.
                return Page();
            }

            // Formdan gelen 'Event' modelini, repositorydeki güncelleme metoduna gönderir.
            _repository.UpdateEvent(Event);

            // Kullanýcýya bir sonraki sayfada gösterilmek üzere bir baþarý mesajý ayarlar.
            TempData["SuccessMessage"] = "Etkinlik baþarýyla güncellendi!";
            // Kullanýcýyý, ayný klasördeki 'Index' sayfasýna yönlendirir.
            return RedirectToPage("./Index");
        }
    }
}