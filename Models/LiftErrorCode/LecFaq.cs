using System.ComponentModel.DataAnnotations;

namespace DestekAybey.Models
{
    public class LecFaq
    {
        public LecFaq() 
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }  
        
        public LecProductSerie LecProductSerie { get; set; }
        
        public LecLanguage LecLanguage { get; set; }
    }
}