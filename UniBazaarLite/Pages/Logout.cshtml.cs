using Microsoft.AspNetCore.Authentication; // Kimlik doğrulama işlemleri için kullanılır.
using Microsoft.AspNetCore.Authorization; // Yetkilendirme öznitelikleri (Authorize) için kullanılır.
using Microsoft.AspNetCore.Mvc; // Controller ve PageModel gibi temel MVC/Razor Sayfaları bileşenleri için.
using Microsoft.AspNetCore.Mvc.RazorPages; // Razor Sayfaları için temel kütüphane.

namespace UniBazaarLite.Pages
{
    [Authorize] // Bu sayfaya sadece giriş yapmış kullanıcılar erişebilir.
    // Çıkış yapma işleminin arka plan kod modelini tanımlar.
    public class LogoutModel : PageModel
    {
        // Kullanıcı "Çıkış Yap" linkine tıkladığında bu metot çalışır
        // ve onay sayfasını gösterir.
        public void OnGet()
        {
        }

        // Kullanıcı onay sayfasındaki butona tıkladığında bu metot çalışır.
        public async Task<IActionResult> OnPostAsync()
        {
            // "CookieAuth" şemasını kullanarak kullanıcının oturumunu sonlandırır (çıkış yapar).
            await HttpContext.SignOutAsync("CookieAuth");
            // Çıkış başarılı olduktan sonra kullanıcıyı ana sayfaya yönlendirir.
            return RedirectToPage("/Index");
        }
    }
}
