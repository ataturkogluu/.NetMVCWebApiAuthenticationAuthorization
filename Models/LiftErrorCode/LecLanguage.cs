using System.ComponentModel.DataAnnotations;

namespace DestekAybey.Models
{
    public class LecLanguage
    {
        
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; }

        public DateTime? CreatedAt { get; set; } 

        public DateTime? UpdatedAt { get; set; } 

        public ICollection<LecErrorCode> LecErrorCodes  { get; set; }

        public ICollection<LecFaq> LecFaqs { get; set; }

        public ICollection<LecLangHtmlTitle> LecLangHtmlTitles { get; set; }
    }
}
