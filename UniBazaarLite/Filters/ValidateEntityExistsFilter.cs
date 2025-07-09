using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UniBazaarLite.Data;

// BURADAKİ KOD BLOKLARI Mustafa Kösem(05200000032) TARAFINDAN YAZILMIŞTIR

namespace UniBazaarLite.Filters
{
    // Bu sınıf, asıl filtreleme mantığını içerir ve IActionFilter arayüzünü uygular.
    public class ValidateEntityExistsFilter<T> : IActionFilter where T : class
    {
        // Repository erişimi için gerekli alanlar.
        private readonly IUniBazaarRepository _repository;
        private readonly string _entityType;

        // Sınıf oluşturulurken repository enjekte edilir
        public ValidateEntityExistsFilter(IUniBazaarRepository repository)
        {
            _repository = repository;
            // Gelen T tipinin adını (örn: "Event", "Item") saklar.
            _entityType = typeof(T).Name;
        }

        // Bir Action metodu çalışmadan hemen önce çalışır.
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Action'ın argümanları arasında "id" anahtarı olup olmadığını kontrol eder.
            if (!context.ActionArguments.TryGetValue("id", out var idValue))
            {
                return; // 'id' yoksa filtreyi sonlandır.
            }

            // 'id' değerini integer'a çevirir.
            var id = (int)(idValue ?? 0);
            bool exists = false;

            // Gelen varlık tipine göre ilgili repositoryden veriyi kontrol eder.
            if (_entityType == "Event")
            {
                exists = _repository.GetEventById(id) != null;
            }
            else if (_entityType == "Item")
            {
                exists = _repository.GetItemById(id) != null;
            }

            // Eğer varlık bulunamazsa sonucu "404 Not Found" olarak ayarlar ve Action'ın çalışmasını engeller.
            if (!exists)
            {
                context.Result = new NotFoundResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}