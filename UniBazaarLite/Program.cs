using Microsoft.AspNetCore.Authentication.Cookies; // Çerez tabanlı kimlik doğrulama için gerekli.
using UniBazaarLite.Data; // Veri katmanı arayüzleri ve uygulamaları için.
using UniBazaarLite.Filters; // Özel filtreler için.


var builder = WebApplication.CreateBuilder(args); // Web uygulamasını oluşturmak için bir builder başlatır.

builder.Configuration.AddUserSecrets<Program>(); // Hassas ayarları (örn. AdminEmail) User Secrets'tan yükler.

// Uygulamanın ihtiyacı olan servisleri (dependency injection) tanımlar.
// Bellek içi depolama için bir singleton repository servisi kaydeder.
builder.Services.AddSingleton<IUniBazaarRepository, InMemoryUniBazaarRepository>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<LogActivityFilter>(); // Tüm controller'lar için global aktivite loglama filtresi ekler.
});
// MVC Controller'lar ve görünümler için gerekli servisleri ekler.

builder.Services.AddRazorPages(); // Razor Sayfaları için gerekli servisleri ekler.

builder.Services.AddAuthentication("CookieAuth") // "CookieAuth" adında bir kimlik doğrulama şeması ekler.
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "UniBazaar.Auth"; // Kimlik doğrulama çerezinin adını belirler.
        options.LoginPath = "/Login"; // Giriş yapılmamışsa yönlendirilecek sayfa.
        options.LogoutPath = "/Logout"; // Çıkış yapılan sayfa.
        options.AccessDeniedPath = "/AccessDenied"; // Erişim reddedildiğinde yönlendirilecek sayfa.
    });

var app = builder.Build(); // Uygulamayı builder'dan oluşturur.

// Gelen HTTP isteklerinin nasıl işleneceğini tanımlar.
if (app.Environment.IsDevelopment()) // Uygulama geliştirme ortamındaysa.
{
    app.UseDeveloperExceptionPage(); // Detaylı hata sayfasını gösterir.
}
else
{
    app.UseExceptionHandler("/Error"); // Genel hata sayfasını kullanır.
    app.UseHsts(); // HTTP Strict Transport Security kullanır.
}

// 404 gibi hataları yakalayıp /Error sayfasına yönlendirir.
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'ye yönlendirir.
app.UseStaticFiles(); // wwwroot klasöründeki statik dosyaları (CSS, JS, resimler) sunar.
app.UseRouting(); // Rota eşleştirmeyi etkinleştirir.

app.UseAuthentication(); // Kimlik doğrulama ara yazılımını etkinleştirir.
app.UseAuthorization(); // Yetkilendirme ara yazılımını etkinleştirir.

// HTTP isteklerinin hangi sayfa veya controller'a gideceğini belirler.
app.MapRazorPages(); // Razor Sayfaları için rota eşleştirmeyi yapar (dosya tabanlı).
app.MapControllerRoute( // MVC Controller'lar için varsayılan rota tanımlar.
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // {controller}/{action}/{id?} şeklinde bir rota deseni.

app.Run(); // Uygulamayı başlatır.
