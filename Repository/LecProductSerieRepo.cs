using DestekAybey.Models;
using JWTAuthentication.NET6._0.Auth;

namespace AuthenticationAndAuthorization.Repository
{
    public class LecProductSerieRepo
    {
        private ApplicationDbContext _context;

        public LecProductSerieRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ProductSerieExists(int id)
        {
            return _context.LecProductSeries.Any(l => l.Id == id);
        }        

        public bool CreateProductSerie(LecProductSerie lecProductSerie)
        {
            _context.Add(lecProductSerie);
            return Save();
        }

        public bool DeleteProductSerie(LecProductSerie lecProductSerie)
        {
            _context.Remove(lecProductSerie);
            return Save();
        }

        public ICollection<LecProductSerie> GetProductSeries()
        {
            return _context.LecProductSeries.ToList();
        }

        public LecProductSerie GetProductSerie(int id)
        {
            return _context.LecProductSeries.Where(l => l.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProductSerie(LecProductSerie lecProductSerie)
        {
            _context.Update(lecProductSerie);
            return Save();
        }
    }
}