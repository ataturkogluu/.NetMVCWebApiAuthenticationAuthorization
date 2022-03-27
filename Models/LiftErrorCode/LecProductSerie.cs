using System.ComponentModel.DataAnnotations;

namespace DestekAybey.Models
{
    public class LecProductSerie
    {
        public LecProductSerie() 
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Serie is required")]
        public string ProductSerie { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }  

        public ICollection<LecFaq> LecFaqs { get; set; }

        public ICollection<LecErrorCodeProductSerie> LecErrorCodeProductSeries { get; set; }
    }
} 