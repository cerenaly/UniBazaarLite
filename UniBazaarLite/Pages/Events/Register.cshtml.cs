// Gerekli ASP.NET Core ve proje kütüphanelerini ekliyoruz.
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using UniBazaarLite.Data;
using UniBazaarLite.Filters;
using UniBazaarLite.Models;


namespace UniBazaarLite.Pages.Events
{
    // Bu sınıf, sadece kayıt formundan gelen verileri tutmak için kullanılır. "Binding Model" veya "Input Model" denir.
    public class RegistrationModel
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
        [Display(Name = "Adınız Soyadınız")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress]
        [Display(Name = "E-posta Adresiniz")]
        public string Email { get; set; } = "";
    }

    // [ValidateEntityExists<Event>], sayfa yüklenmeden önce verilen 'id' ile
    // bir etkinliğin olup olmadığını kontrol eden özel filtremiz
    [ValidateEntityExists<Event>]
    public class RegisterModel : PageModel
    {
        private readonly IUniBazaarRepository _repository;

        // Sayfada gösterilecek olan etkinlik bilgisini tutar.
        public Event Event { get; set; } = new();

        // [BindProperty], bu nesnenin formdan gelen verilerle
        // otomatik olarak doldurulacağını belirtir.
        [BindProperty]
        public RegistrationModel Registration { get; set; } = new();

        // Gerekli olan repository servisini enjekte eder.
        public RegisterModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET isteği yapıldığında (sayfa ilk yüklendiğinde) çalışır.
        public IActionResult OnGet(int id)
        {
            // Repositoryden ilgili etkinliği bulur. (Filtre sayesinde var olduğundan eminiz).
            var eventToRegister = _repository.GetEventById(id);
            if (eventToRegister == null) return NotFound();

            // Bulunan etkinliği ve ID'sini sayfanın modellerine atar.
            Event = eventToRegister;
            Registration.EventId = id;

            // Eğer kullanıcı giriş yapmışsa, formdaki alanları otomatik doldurur.
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Registration.FullName = User.FindFirstValue(ClaimTypes.Name) ?? "";
                Registration.Email = User.FindFirstValue(ClaimTypes.Email) ?? "";
            }

            return Page();
        }

        // Forma bir POST isteği yapıldığında (form gönderildiğinde) çalışır.
        public IActionResult OnPost()
        {
            // Formun doğrulama kurallarının geçerli olup olmadığını kontrol eder.
            if (!ModelState.IsValid)
            {
                // Hata varsa, sayfanın başlığında vb. gösterilen etkinlik bilgisini
                // yeniden yükleyip sayfayı tekrar göstermek gerekir.
                var eventToRegister = _repository.GetEventById(Registration.EventId);
                if (eventToRegister == null) return NotFound();
                Event = eventToRegister;

                return Page();
            }

            // Kayıt yapılacak etkinliği tekrar buluruz.
            var registeredEvent = _repository.GetEventById(Registration.EventId);
            if (registeredEvent == null) return NotFound();

            // Kullanıcıya bir sonraki sayfada gösterilecek bir başarı mesajı ayarlarız.
            TempData["SuccessMessage"] = $"Sayın {Registration.FullName}, '{registeredEvent.Name}' etkinliğine kaydınız başarıyla alındı!";
            // Kullanıcıyı etkinlikler listesine geri yönlendiririz.
            return RedirectToPage("./Index");
        }
    }
}
