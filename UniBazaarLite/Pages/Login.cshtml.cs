using Microsoft.AspNetCore.Authentication; // Kimlik doğrulama işlemleri için kullanılır.
using Microsoft.AspNetCore.Mvc; // Controller ve PageModel gibi temel MVC/Razor Sayfaları bileşenleri için.
using Microsoft.AspNetCore.Mvc.RazorPages; // Razor Sayfaları için temel kütüphane.
using System.ComponentModel.DataAnnotations; // Model doğrulama öznitelikleri (Required, EmailAddress) için.
using System.Security.Claims; // Kullanıcı kimlik bilgilerini yönetmek için.
using UniBazaarLite.Data; // Projenin veri katmanına erişmek için.

namespace UniBazaarLite.Pages
{
    // Giriş sayfasının arka plan kod modelini tanımlar.
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration; // Yapılandırma ayarlarını (appsettings.json, User Secrets) okumak için.

        [BindProperty, Required, EmailAddress]
        // Formdan gelen 'Email' alanını modele bağlar ve gerekli, geçerli e-posta olmasını sağlar.
        public string Email { get; set; } = "";

        [TempData]
        // Geçici veri depolama alanı. Bir istekten diğerine hata mesajları taşımak için kullanılır.
        public string ErrorMessage { get; set; } = "";

        // Model oluşturulurken IConfiguration servisini Dependency Injection ile alır.
        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Sayfaya bir GET isteği yapıldığında (ilk yüklendiğinde) çalışır.
        public void OnGet() { }

        // Sayfaya bir POST isteği yapıldığında (form gönderildiğinde) asenkron olarak çalışır.
        public async Task<IActionResult> OnPostAsync()
        {
            // Model doğrulamasında bir hata varsa, sayfayı tekrar gösterir.
            if (!ModelState.IsValid) return Page();

            // Admin e-posta ve ismini User Secrets'tan okur.
            var adminEmail = _configuration["AdminEmail"];
            var adminName = _configuration["AdminName"];
            UserDto? user = null;

            // Girilen e-posta admin e-postasıyla eşleşiyorsa.
            if (string.Equals(Email, adminEmail, StringComparison.OrdinalIgnoreCase))
            {
                // Admin kullanıcısını oluşturur.
                user = new UserDto(adminName ?? "Admin", adminEmail!, "Admin");
            }
            else
            {
                // Simüle edilmiş kullanıcı verileri arasından e-posta ile kullanıcıyı bulur.
                user = SimulatedUserData.GetUserByEmail(Email);
            }

            if (user == null)
            {
                ErrorMessage = "Kullanıcı bulunamadı.";
                return Page();
            }

            // Kullanıcıya özel claims listesi oluşturulur.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name), // Kullanıcının adı.
                new Claim(ClaimTypes.Email, user.Email), // Kullanıcının e-postası.
                new Claim(ClaimTypes.Role, user.Role) // Kullanıcının rolü.
            };

            // claims kullanarak yeni bir ClaimsIdentity oluşturur (kimlik doğrulama şeması "CookieAuth").
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            // Kimlik doğrulama özellikleri. 'IsPersistent = true' çerezin kalıcı olmasını sağlar.
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            // Kullanıcıyı kimlik doğrulama şeması ("CookieAuth") ile sisteme giriş yapar.
            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

            // Giriş başarılı olursa ana sayfaya yönlendirir.
            return RedirectToPage("/Index");
        }
    }
}
