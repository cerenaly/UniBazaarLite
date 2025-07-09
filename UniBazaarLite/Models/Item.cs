// Form doğrulama ve gösterim için gerekli olan kütüphaneyi ekliyoruz.
using System.ComponentModel.DataAnnotations;

// BURADAKİ KOD BLOKLARI SİYAM ACET(05200000080) TARAFINDAN YAZILMIŞTIR

namespace UniBazaarLite.Models
{
    // Bir "İlan" nesnesinin yapısını ve kurallarını tanımlayan model sınıfı.
    public class Item
    {
        // İlanın id'si.
        public int Id { get; set; }

        // İlanın başlığı.
        [Required(ErrorMessage = "İlan başlığı zorunludur.")] // Bu alanın zorunlu olduğunu belirtir.
        [Display(Name = "İlan Başlığı")] // Formda "İlan Başlığı" olarak gözükür.
        public string Name { get; set; } = string.Empty;

        // İlanın açıklaması.
        [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        // İlanın fiyatı.
        [Required(ErrorMessage = "Fiyat alanı zorunludur.")]
        // Değerin 0.01'den büyük olması gerektiğini belirtir.
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
        [Display(Name = "Fiyat (₺)")]
        public decimal Price { get; set; }

        // Satıcının e-posta adresi.
        [Required]
        [EmailAddress] // Geçerli bir e-posta formatında olmasını zorunlu kılar.
        [Display(Name = "Satıcının E-postası")]
        public string SellerEmail { get; set; } = string.Empty;
    }
}