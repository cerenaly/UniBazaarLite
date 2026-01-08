// Gerekli ASP.NET Core ve proje kütüphanelerini ekliyoruz.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniBazaarLite.Data;
using UniBazaarLite.Models;


namespace UniBazaarLite.Pages.Events
{
    // [Authorize] özniteliği, bu sayfanın sadece giriş yapmış kullanıcılar
    // tarafından erişilebilir olmasını sağlar.
    [Authorize]
    public class CreateModel : PageModel
    {
        // Repository erişim için 'readonly' bir alan.
        private readonly IUniBazaarRepository _repository;

        // [BindProperty], bu 'Event' nesnesinin formdan gelen verilerle
        // otomatik olarak doldurulacağını belirtir.
        [BindProperty]
        public Event Event { get; set; } = new();

        // Sınıf oluşturulurken repository enjekte eder.
        public CreateModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET isteği yapıldığında (sayfa ilk yüklendiğinde) çalışır.
        public void OnGet()
        {
            // Formdaki tarih alanını, o anki tarih ve saat ile doldurur.
            Event.Date = DateTime.Now;
        }

        // Forma bir POST isteği yapıldığında (form gönderildiğinde) çalışır.
        public IActionResult OnPost()
        {
            // Modelin doğrulama kurallarının geçerli olup olmadığını kontrol eder.
            if (!ModelState.IsValid)
            {
                // Geçerli değilse, aynı sayfayı hatalarla birlikte tekrar gösterir.
                return Page();
            }

            // Hazırlanan 'Event' nesnesini repository'e ekler.
            _repository.AddEvent(Event);

            // Kullanıcıya bir sonraki sayfada gösterilmek üzere bir başarı mesajı ayarlar.
            TempData["SuccessMessage"] = "Yeni etkinlik başarıyla oluşturuldu!";

            // Kullanıcıyı etkinliklerin listelendiği Index sayfasına yönlendirir.
            return RedirectToPage("/Events/Index");
        }
    }
}
