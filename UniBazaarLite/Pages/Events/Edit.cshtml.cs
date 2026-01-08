// Gerekli ASP.NET Core ve proje kütüphanelerini ekliyoruz.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniBazaarLite.Data;
using UniBazaarLite.Filters;
using UniBazaarLite.Models;


namespace UniBazaarLite.Pages.Events
{
    // [Authorize], bu sayfanın sadece giriş yapmış kullanıcılar tarafından erişilebilir olmasını sağlar.
    [Authorize]
    // [ValidateEntityExists<Event>], OnGet metodu çalışmadan önce, verilen 'id' ile bir
    // etkinliğin olup olmadığını kontrol eden özel filtremizdir.
    [ValidateEntityExists<Event>]
    public class EditModel : PageModel
    {
        // Repositroy erişim için 'readonly' bir alan.
        private readonly IUniBazaarRepository _repository;

        // [BindProperty], bu 'Event' nesnesinin formdan gelen verilerle
        // otomatik olarak doldurulacağını belirtir.
        [BindProperty]
        public Event Event { get; set; } = new();

        // Sınıf oluşturulurken repository enjekte eder.
        public EditModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET isteği yapıldığında (örn: /Events/Edit/5) çalışır.
        public IActionResult OnGet(int id)
        {
            // Filtremiz kontrolü yaptığı için, buraya ulaştığımızda etkinliğin var olduğunu biliriz.
            // Repositoryden ilgili etkinliği bulup 'Event' modelimize atarız.
            var eventFromDb = _repository.GetEventById(id);
            Event = eventFromDb!;
            return Page();
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

            // Formdan gelen 'Event' modelini, repositorydeki güncelleme metoduna gönderir.
            _repository.UpdateEvent(Event);

            // Kullanıcıya bir sonraki sayfada gösterilmek üzere bir başarı mesajı ayarlar.
            TempData["SuccessMessage"] = "Etkinlik başarıyla güncellendi!";
            // Kullanıcıyı, aynı klasördeki 'Index' sayfasına yönlendirir.
            return RedirectToPage("./Index");
        }
    }
}
