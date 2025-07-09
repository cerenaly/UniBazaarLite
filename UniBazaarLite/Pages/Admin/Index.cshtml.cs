// Gerekli ASP.NET Core kütüphanelerini ekliyoruz.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

// BURADAKİ KOD BLOKLARI Siyam Acet(05200000080) TARAFINDAN YAZILMIŞTIR

namespace UniBazaarLite.Pages.Admin
{
    // [Authorize] özniteliği, bu sayfanın yetkilendirme gerektirdiğini belirtir.
    // (Roles = "Admin") parametresi, erişim için kullanıcının "Admin" rolüne sahip olmasını zorunlu kılar.
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        // Bu metot, sayfaya bir GET isteği yapıldığında çalışır.
        public void OnGet()
        {

        }
    }
}