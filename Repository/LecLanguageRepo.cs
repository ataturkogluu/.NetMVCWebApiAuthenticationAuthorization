using DestekAybey.Models;
using JWTAuthentication.NET6._0.Auth;

namespace AuthenticationAndAuthorization.Repository
{
     public class LecLanguageRepo
    {
        private ApplicationDbContext _context;

        public LecLanguageRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool LanguageExists(int id)
        {
            return _context.LecLanguages.Any(l => l.Id == id);
        }        

        public bool CreateLanguage(LecLanguage lecLanguage)
        {
            lecLanguage.CreatedAt = DateTime.Now;
            lecLanguage.UpdatedAt = null;
            _context.Add(lecLanguage);
            return Save();
        }

        public bool DeleteLanguage(LecLanguage lecLanguage)
        {
            _context.Remove(lecLanguage);
            return Save();
        }

        public ICollection<LecLanguage> GetLanguages()
        {
            return _context.LecLanguages.ToList();
        }

        public LecLanguage GetLanguage(int id)
        {
            return _context.LecLanguages.Where(l => l.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLanguage(LecLanguage lecLanguage)
        {

            lecLanguage.UpdatedAt = DateTime.Now;
           // lecLanguage.CreatedAt = 
            _context.Update(lecLanguage);
            return Save();
        }
    }
}