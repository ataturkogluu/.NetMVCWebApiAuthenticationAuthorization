using DestekAybey.Models;
using JWTAuthentication.NET6._0.Auth;

namespace AuthenticationAndAuthorization.Repository
{
    public class LecFaqRepo
    {
        private ApplicationDbContext _context;

        public LecFaqRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool FaqExists(int id)
        {
            return _context.LecFaqs.Any(l => l.Id == id);
        }      

        public bool CreateFaq(LecFaq lecFaq)
        {
            _context.Add(lecFaq);
            return Save();
        }

        public bool DeleteFaq(LecFaq lecFaq)
        {
            _context.Remove(lecFaq);
            return Save();
        }

        public ICollection<LecFaq> GetFaqs()
        {
            return _context.LecFaqs.ToList();
        }

        public LecFaq GetFaq(int id)
        {
            return _context.LecFaqs.Where(l => l.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFaq(LecFaq lecFaq)
        {
            _context.Update(lecFaq);
            return Save();
        }
    }
}
