// Gerekli ASP.NET Core ve proje kütüphanelerini ekliyoruz.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using UniBazaarLite.Data;
using UniBazaarLite.Models;

// BURADAKİ KOD BLOKLARI Ceren Alyağız(05200000121) TARAFINDAN YAZILMIŞTIR

namespace UniBazaarLite.Pages.Classifieds
{
    // [Authorize] özniteliği, bu sayfanın sadece giriş yapmış kullanıcılar
    // tarafından erişilebilir olmasını sağlar.
    [Authorize]
    public class CreateModel : PageModel
    {
        // Repository erişim için 'readonly' bir alan tanımlıyoruz.
        private readonly IUniBazaarRepository _repository;

        // [BindProperty], bu 'Item' nesnesinin formdan gelen verilerle
        // otomatik olarak doldurulacağını belirtir.
        [BindProperty]
        public Item Item { get; set; } = new();

        // Sınıf oluşturulurken repository enjekte eder.
        public CreateModel(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Sayfaya bir GET isteği yapıldığında (sayfa ilk yüklendiğinde) çalışır.
        public void OnGet()
        {
            // O an giriş yapmış olan kullanıcının kimlik bilgilerinden (claims)
            // e-posta adresini bulur ve formdaki ilgili alana atar.
            Item.SellerEmail = User.FindFirstValue(ClaimTypes.Email) ?? "bulunamadi@example.com";
        }

        // Forma bir POST isteği yapıldığında (form gönderildiğinde) çalışır.
        public IActionResult OnPost()
        {
            // Modelin doğrulama kurallarının (örn: [Required]) geçerli olup olmadığını kontrol eder.
            if (!ModelState.IsValid)
            {
                // Geçerli değilse, aynı sayfayı hatalarla birlikte tekrar gösterir.
                return Page();
            }

            Item.SellerEmail = User.FindFirstValue(ClaimTypes.Email) ?? "bulunamadi@example.com";

            // Hazırlanan 'Item' nesnesini veri deposuna ekler.
            _repository.AddItem(Item);

            // Kullanıcıya bir sonraki sayfada gösterilmek üzere bir başarı mesajı ayarlar.
            TempData["SuccessMessage"] = "Yeni ilan başarıyla yayınlandı!";

            // Kullanıcıyı, ilanların listelendiği MVC Controller'ındaki 'Index' action'ına yönlendirir.
            return RedirectToAction("Index", "Items");
        }
    }
}