using UniBazaarLite.Models;

// BURADAKİ KOD BLOKLARI SİYAM ACET(05200000080) TARAFINDAN YAZILMIŞTIR

namespace UniBazaarLite.Data
{
    // IUniBazaarRepository interface'ini tanımlarız.
    public interface IUniBazaarRepository
    {

        // Tüm etkinlikleri bir liste olarak döndürecek bir metot
        List<Event> GetAllEvents();

        // Verilen id'ye göre tek bir etkinliği döndürecek bir metot
        Event? GetEventById(int id);

        // Yeni bir etkinlik eklemek için kullanılacak metot
        void AddEvent(Event newEvent);

        // Mevcut bir etkinliği güncellemek için kullanılacak metot
        void UpdateEvent(Event eventToUpdate);

        // Tüm ilanları bir liste olarak döndürecek bir metot
        List<Item> GetAllItems();

        // Verilen 'id'ye göre tek bir ilanı döndürecek bir metot
        Item? GetItemById(int id);

        // Yeni bir ilan eklemek için kullanılacak metot
        void AddItem(Item newItem);
    }
}