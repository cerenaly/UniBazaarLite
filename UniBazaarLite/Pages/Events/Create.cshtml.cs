// Gerekli ASP.NET Core ve proje kütüphanelerini ekliyoruz.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniBazaarLite.Data;
using UniBazaarLite.Models;

// BURADAKÝ KOD BLOKLARI Ceren Alyaðýz(05200000121) TARAFINDAN YAZILMIÞTIR

namespace UniBazaarLite.Pages.Events
{
    // [Authorize] özniteliði, bu sayfanýn sadece giriþ yapmýþ kullanýcýlar
    // tarafýndan eriþilebilir olmasýný saðlar.
    [Authorize]
    public class CreateModel : PageModel
    {
        // Repository eriþim için 'readonly' bir alan.
        private readonly IUniBazaarRepository _repository;

        // [BindProperty], bu 'Event' nesnesinin formdan gelen verilerle
        // otomatik olarak doldurulacaðýný belirtir.
        [BindProperty]
        public Event Event { get; set; } = new();

        // Sýnýf oluþturulurken repository enjekte eder.
        public CreateModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET isteði yapýldýðýnda (sayfa ilk yüklendiðinde) çalýþýr.
        public void OnGet()
        {
            // Formdaki tarih alanýný, o anki tarih ve saat ile doldurur.
            Event.Date = DateTime.Now;
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

            // Hazýrlanan 'Event' nesnesini repository'e ekler.
            _repository.AddEvent(Event);

            // Kullanýcýya bir sonraki sayfada gösterilmek üzere bir baþarý mesajý ayarlar.
            TempData["SuccessMessage"] = "Yeni etkinlik baþarýyla oluþturuldu!";

            // Kullanýcýyý etkinliklerin listelendiði Index sayfasýna yönlendirir.
            return RedirectToPage("/Events/Index");
        }
    }
}