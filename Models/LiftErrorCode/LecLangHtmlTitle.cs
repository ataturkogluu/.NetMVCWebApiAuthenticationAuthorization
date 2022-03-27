using System.ComponentModel.DataAnnotations;

namespace DestekAybey.Models
{
    public class LecLangHtmlTitle
    {
        public LecLangHtmlTitle() 
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Key is required")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Value is required")]
        public string Value { get; set; }

        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }  
        
        public LecLanguage LecLanguage { get; set; }
    }
}