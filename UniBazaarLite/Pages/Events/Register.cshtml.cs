// Gerekli ASP.NET Core ve proje kütüphanelerini ekliyoruz.
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using UniBazaarLite.Data;
using UniBazaarLite.Filters;
using UniBazaarLite.Models;

// BURADAKÝ KOD BLOKLARI Ceren Alyaðýz(05200000121) TARAFINDAN YAZILMIÞTIR

namespace UniBazaarLite.Pages.Events
{
    // Bu sýnýf, sadece kayýt formundan gelen verileri tutmak için kullanýlýr. "Binding Model" veya "Input Model" denir.
    public class RegistrationModel
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Ad Soyad alaný zorunludur.")]
        [Display(Name = "Adýnýz Soyadýnýz")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "E-posta alaný zorunludur.")]
        [EmailAddress]
        [Display(Name = "E-posta Adresiniz")]
        public string Email { get; set; } = "";
    }

    // [ValidateEntityExists<Event>], sayfa yüklenmeden önce verilen 'id' ile
    // bir etkinliðin olup olmadýðýný kontrol eden özel filtremiz
    [ValidateEntityExists<Event>]
    public class RegisterModel : PageModel
    {
        private readonly IUniBazaarRepository _repository;

        // Sayfada gösterilecek olan etkinlik bilgisini tutar.
        public Event Event { get; set; } = new();

        // [BindProperty], bu nesnenin formdan gelen verilerle
        // otomatik olarak doldurulacaðýný belirtir.
        [BindProperty]
        public RegistrationModel Registration { get; set; } = new();

        // Gerekli olan repository servisini enjekte eder.
        public RegisterModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET isteði yapýldýðýnda (sayfa ilk yüklendiðinde) çalýþýr.
        public IActionResult OnGet(int id)
        {
            // Repositoryden ilgili etkinliði bulur. (Filtre sayesinde var olduðundan eminiz).
            var eventToRegister = _repository.GetEventById(id);
            if (eventToRegister == null) return NotFound();

            // Bulunan etkinliði ve ID'sini sayfanýn modellerine atar.
            Event = eventToRegister;
            Registration.EventId = id;

            // Eðer kullanýcý giriþ yapmýþsa, formdaki alanlarý otomatik doldurur.
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Registration.FullName = User.FindFirstValue(ClaimTypes.Name) ?? "";
                Registration.Email = User.FindFirstValue(ClaimTypes.Email) ?? "";
            }

            return Page();
        }

        // Forma bir POST isteði yapýldýðýnda (form gönderildiðinde) çalýþýr.
        public IActionResult OnPost()
        {
            // Formun doðrulama kurallarýnýn geçerli olup olmadýðýný kontrol eder.
            if (!ModelState.IsValid)
            {
                // Hata varsa, sayfanýn baþlýðýnda vb. gösterilen etkinlik bilgisini
                // yeniden yükleyip sayfayý tekrar göstermek gerekir.
                var eventToRegister = _repository.GetEventById(Registration.EventId);
                if (eventToRegister == null) return NotFound();
                Event = eventToRegister;

                return Page();
            }

            // Kayýt yapýlacak etkinliði tekrar buluruz.
            var registeredEvent = _repository.GetEventById(Registration.EventId);
            if (registeredEvent == null) return NotFound();

            // Kullanýcýya bir sonraki sayfada gösterilecek bir baþarý mesajý ayarlarýz.
            TempData["SuccessMessage"] = $"Sayýn {Registration.FullName}, '{registeredEvent.Name}' etkinliðine kaydýnýz baþarýyla alýndý!";
            // Kullanýcýyý etkinlikler listesine geri yönlendiririz.
            return RedirectToPage("./Index");
        }
    }
}