using Microsoft.AspNetCore.Mvc;

// BURADAKİ KOD BLOKLARI Mustafa Kösem(05200000032) TARAFINDAN YAZILMIŞTIR

namespace UniBazaarLite.Filters
{
    // Bu sınıf, [ValidateEntityExists<T>] şeklinde kullanılacak olan attribute'tur.
    // TypeFilterAttribute'dan miras alarak, asıl filtre mantığını DI konteyneri üzerinden
    // çağırmamızı sağlar (T'nin bir sınıf olmalı örn: Event, Item)
    public class ValidateEntityExistsAttribute<T> : TypeFilterAttribute where T : class
    {
        // Constructor'ı bu attribute kullanıldığında çalışır.
        // Miras aldığı TypeFilterAttribute'ın constructor'ını çağırır. Parametre olarak filtre sınıfının Type'ını verir.
        public ValidateEntityExistsAttribute() : base(typeof(ValidateEntityExistsFilter<T>))
        {

        }
    }
}