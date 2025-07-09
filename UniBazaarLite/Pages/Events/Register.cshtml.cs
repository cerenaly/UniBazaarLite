// Gerekli ASP.NET Core ve proje k�t�phanelerini ekliyoruz.
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using UniBazaarLite.Data;
using UniBazaarLite.Filters;
using UniBazaarLite.Models;

// BURADAK� KOD BLOKLARI Ceren Alya��z(05200000121) TARAFINDAN YAZILMI�TIR

namespace UniBazaarLite.Pages.Events
{
    // Bu s�n�f, sadece kay�t formundan gelen verileri tutmak i�in kullan�l�r. "Binding Model" veya "Input Model" denir.
    public class RegistrationModel
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Ad Soyad alan� zorunludur.")]
        [Display(Name = "Ad�n�z Soyad�n�z")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "E-posta alan� zorunludur.")]
        [EmailAddress]
        [Display(Name = "E-posta Adresiniz")]
        public string Email { get; set; } = "";
    }

    // [ValidateEntityExists<Event>], sayfa y�klenmeden �nce verilen 'id' ile
    // bir etkinli�in olup olmad���n� kontrol eden �zel filtremiz
    [ValidateEntityExists<Event>]
    public class RegisterModel : PageModel
    {
        private readonly IUniBazaarRepository _repository;

        // Sayfada g�sterilecek olan etkinlik bilgisini tutar.
        public Event Event { get; set; } = new();

        // [BindProperty], bu nesnenin formdan gelen verilerle
        // otomatik olarak doldurulaca��n� belirtir.
        [BindProperty]
        public RegistrationModel Registration { get; set; } = new();

        // Gerekli olan repository servisini enjekte eder.
        public RegisterModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET iste�i yap�ld���nda (sayfa ilk y�klendi�inde) �al���r.
        public IActionResult OnGet(int id)
        {
            // Repositoryden ilgili etkinli�i bulur. (Filtre sayesinde var oldu�undan eminiz).
            var eventToRegister = _repository.GetEventById(id);
            if (eventToRegister == null) return NotFound();

            // Bulunan etkinli�i ve ID'sini sayfan�n modellerine atar.
            Event = eventToRegister;
            Registration.EventId = id;

            // E�er kullan�c� giri� yapm��sa, formdaki alanlar� otomatik doldurur.
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Registration.FullName = User.FindFirstValue(ClaimTypes.Name) ?? "";
                Registration.Email = User.FindFirstValue(ClaimTypes.Email) ?? "";
            }

            return Page();
        }

        // Forma bir POST iste�i yap�ld���nda (form g�nderildi�inde) �al���r.
        public IActionResult OnPost()
        {
            // Formun do�rulama kurallar�n�n ge�erli olup olmad���n� kontrol eder.
            if (!ModelState.IsValid)
            {
                // Hata varsa, sayfan�n ba�l���nda vb. g�sterilen etkinlik bilgisini
                // yeniden y�kleyip sayfay� tekrar g�stermek gerekir.
                var eventToRegister = _repository.GetEventById(Registration.EventId);
                if (eventToRegister == null) return NotFound();
                Event = eventToRegister;

                return Page();
            }

            // Kay�t yap�lacak etkinli�i tekrar buluruz.
            var registeredEvent = _repository.GetEventById(Registration.EventId);
            if (registeredEvent == null) return NotFound();

            // Kullan�c�ya bir sonraki sayfada g�sterilecek bir ba�ar� mesaj� ayarlar�z.
            TempData["SuccessMessage"] = $"Say�n {Registration.FullName}, '{registeredEvent.Name}' etkinli�ine kayd�n�z ba�ar�yla al�nd�!";
            // Kullan�c�y� etkinlikler listesine geri y�nlendiririz.
            return RedirectToPage("./Index");
        }
    }
}