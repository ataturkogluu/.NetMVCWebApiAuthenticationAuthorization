namespace DestekAybey.Models
{
    public class LecErrorCodeProductSerie
    {
        public int ErrorCodeId { get; set; }

        public int ProductSerieId { get; set; }
        
        public LecErrorCode LecErrorCode { get; set; }
        
        public LecProductSerie LecProductSerie { get; set; }
    }
}