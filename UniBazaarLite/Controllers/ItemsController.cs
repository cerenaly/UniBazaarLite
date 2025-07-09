// Gerekli ASP.NET Core kütüphanelerini ve projemizin diğer katmanlarını (Data, Models, Filters)
// bu dosyada kullanabilmek için "using" direktiflerini ekliyoruz.
using Microsoft.AspNetCore.Mvc;
using UniBazaarLite.Data;
using UniBazaarLite.Models;
using UniBazaarLite.Filters;

// BURADAKİ KOD BLOKLARI Mustafa Kösem(05200000032) TARAFINDAN YAZILMIŞTIR

namespace UniBazaarLite.Controllers
{
    // ItemsController, ilanlarla (Items) ilgili web isteklerini yönetmekten sorumlu sınıftır.
    // Controller sınıfından miras alarak MVC yaparız.
    public class ItemsController : Controller
    {
        // _repository, verilere (bellek-içi liste) erişmek için kullanılacak olan bir field.
        private readonly IUniBazaarRepository _repository;

        // Constructor metodudur. ItemsController'ın bir örneği oluşturulduğunda çalışır.
        // Dependency Injection sayesinde, Program.cs'de kaydettiğimiz
        // IUniBazaarRepository servisinin bir örneği otomatik olarak bu metoda parametre olarak verilir.
        public ItemsController(IUniBazaarRepository repository)
        {
            _repository = repository;
        }

        // Bu metot, "/Items" veya "/Items/Index" adresine gelen GET isteklerini karşılar.
        // Bu, MVC'nin conventional routing kullanımıdır.
        public IActionResult Index()
        {
            // Veri deposundan tüm ilanların listesini çeker.
            var items = _repository.GetAllItems();
            // Aldığı 'items' listesini, Views/Items/Index.cshtml dosyasına model olarak göndererek
            // bir HTML sayfası oluşturur ve kullanıcıya döndürür.
            return View(items);
        }

        // Bu metot, bir ilanın detaylarını göstermek için kullanılır.
        // [ValidateEntityExists<Item>] filtremiz metoda girilmeden hemen önce çalışır.
        // Gelen 'id' ile bir Item olup olmadığını kontrol eder. Yoksa, 404 Not Found sayfası döndürür.
        [ValidateEntityExists<Item>]
        // [Route]: Bu attribute-based routing örneğidir. Geleneksel rotayı ezerek
        // bu metodun sadece "/items/details/{id}" formatındaki adreslere cevap vermesini sağlar.
        // {id:int} kısmı, bu bölümün bir tam sayı olması gerektiğini belirtir.
        [Route("/items/details/{id:int}")]
        public IActionResult Details(int id)
        {
            // Repositoryden ilgili id'ye sahip ilanı alır.
            var item = _repository.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            // Bulunan 'item' nesnesini, Views/Items/Details.cshtml dosyasına model olarak gönderir
            // ve kullanıcıya bir HTML sayfası döndürür.
            return View(item);
        }

        // Bu metot, projenin API özelliğini oluşturur.
        // [HttpGet]: Bu metodun sadece GET isteklerine ve sadece "/api/items" adresine
        // cevap vermesini sağlar.
        [HttpGet("/api/items")]
        public IActionResult GetItemsApi()
        {
            // Repositoryden tüm ilanları alır.
            var items = _repository.GetAllItems();
            // items listesini doğrudan JSON formatında veri olarak döndürür.
            return Json(items);
        }
    }
}