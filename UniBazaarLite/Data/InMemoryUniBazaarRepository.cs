// Gerekli olan Model sınıflarını bu dosyada kullanabilmek için ekliyoruz.
using UniBazaarLite.Models;

// BURADAKİ KOD BLOKLARI SİYAM ACET(05200000080) TARAFINDAN YAZILMIŞTIR

namespace UniBazaarLite.Data
{
    // Bu sınıf, IUniBazaarRepository arayüzünü uygular.
    // Veri işlemlerini veritabanı yerine kodun içindeki listelerle yapar.
    public class InMemoryUniBazaarRepository : IUniBazaarRepository
    {
        // 'static' anahtar kelimesi, bu listelerin uygulama çalıştığı sürece
        // bellekte tek bir kopyasının olmasını sağlar. Bu sayede veriler kaybolmaz.

        // Uygulama başladığında kullanılacak olan başlangıç etkinlik verileri.
        private static List<Event> _events = new List<Event>()
        {
            new Event { Id = 1, Name = "Bahar Şenliği Konseri", Date = DateTime.Now.AddDays(10), Location = "Kampüs Amfitiyatro", Description = "Yaz konserleri başlıyor!"},
            new Event { Id = 2, Name = "Kariyer Günleri", Date = DateTime.Now.AddDays(20), Location = "Mühendislik Fakültesi", Description = "Firmalarla tanışma fırsatı."}
        };
        // Uygulama başladığında kullanılacak olan başlangıç ilan verileri.
        private static List<Item> _items = new List<Item>()
        {
            new Item { Id = 1, Name = "Calculus Ders Kitabı", Price = 150, SellerEmail = "student@example.com", Description = "Hiç kullanılmadı, sıfır gibi."},
            new Item { Id = 2, Name = "Az Kullanılmış Gitar", Price = 800, SellerEmail = "music@example.com", Description = "Başlangıç için harika bir enstrüman."}
        };

        // Yeni eklenecek etkinlik ve ilanlar için bir sonraki ID'yi takip eden sayaçlar.
        private static int _nextEventId = 3;
        private static int _nextItemId = 3;

        // Tüm etkinlik listesini döndürür.
        public List<Event> GetAllEvents() => _events;

        // Verilen id'ye göre listeden tek bir etkinliği bulur ve döndürür. Bulamazsa null döner.
        public Event? GetEventById(int id) => _events.FirstOrDefault(e => e.Id == id);

        // Listeye yeni bir etkinlik ekler. ID'sini otomatik olarak artırır.
        public void AddEvent(Event newEvent) { newEvent.Id = _nextEventId++; _events.Add(newEvent); }

        // Mevcut bir etkinliği günceller.
        public void UpdateEvent(Event eventToUpdate)
        {
            // Önce güncellenecek etkinliği listede bul.
            var existingEvent = GetEventById(eventToUpdate.Id);
            // Eğer etkinlik bulunduysa, özelliklerini yeni değerlerle değiştir.
            if (existingEvent != null)
            {
                existingEvent.Name = eventToUpdate.Name;
                existingEvent.Description = eventToUpdate.Description;
                existingEvent.Date = eventToUpdate.Date;
                existingEvent.Location = eventToUpdate.Location;
            }
        }

        // Tüm ilan listesini döndürür.
        public List<Item> GetAllItems() => _items;

        // Verilen id'ye göre listeden tek bir ilanı bulur ve döndürür.
        public Item? GetItemById(int id) => _items.FirstOrDefault(i => i.Id == id);

        // Listeye yeni bir ilan ekler. ID'sini otomatik olarak artırır.
        public void AddItem(Item newItem) { newItem.Id = _nextItemId++; _items.Add(newItem); }
    }
}