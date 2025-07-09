using System.ComponentModel.DataAnnotations;

// BURADAKİ KOD BLOKLARI SİYAM ACET(05200000080) TARAFINDAN YAZILMIŞTIR

namespace UniBazaarLite.Models
{
    // Bir "Etkinlik" nesnesinin yapısını tanımlayan model sınıfı.
    public class Event
    {
        // Etkinliğin IDsi.
        public int Id { get; set; }

        // Etkinlik adı.
        [Required(ErrorMessage = "Etkinlik adı zorunludur.")] // Bu alanın doldurulması zorunludur.
        [StringLength(100)] // Maksimum 100 karakter olabilir.
        [Display(Name = "Etkinlik Adı")] // Formda "Etkinlik Adı" olarak gözükür.
        public string Name { get; set; } = string.Empty;

        // Etkinlik açıklaması.
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        // Etkinliğin tarihi ve saati.
        [Required(ErrorMessage = "Tarih zorunludur.")]
        [Display(Name = "Etkinlik Tarihi ve Saati")]
        // Tarih seçme aracının formatını belirler, saniyeleri göstermez.
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        // Etkinliğin konumu.
        [Required(ErrorMessage = "Konum zorunludur.")]
        [Display(Name = "Konum")]
        public string Location { get; set; } = string.Empty;
    }
}