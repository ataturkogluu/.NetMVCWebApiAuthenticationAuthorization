using System.ComponentModel.DataAnnotations;

namespace DestekAybey.Models
{
    public class LecErrorCode
    {
        
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Error Code is required")]
        public int ErrorCode { get; set; } 

        [Required(ErrorMessage = "Error Name is required")]
        public string ErrorName { get; set; }

        [Required(ErrorMessage = "Error Explanation is required")]
        public string ErrorExplanation { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }   

        public LecLanguage LecLanguage { get; set; }

        public ICollection<LecErrorCodeProductSerie> LecErrorCodeProductSeries { get; set; }
        
    }
}
    

