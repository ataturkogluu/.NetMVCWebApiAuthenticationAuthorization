using DestekAybey.Models;
using JWTAuthentication.NET6._0.Auth;

namespace AuthenticationAndAuthorization.Repository
{
    public class LecLangHtmlTitleRepo
    {
        private ApplicationDbContext _context;

        public LecLangHtmlTitleRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool HtmlTitleExists(int id)
        {
            return _context.LecLangHtmlTitles.Any(h => h.Id == id);
        }

        public bool CreateHtmlTitle(LecLangHtmlTitle lecLangHtmlTitle)
        {
            _context.Add(lecLangHtmlTitle);        
            return Save();
        }

        public bool UpdateHtmlTitle(string language, LecLangHtmlTitle lecLangHtmlTitle)
        {
            _context.Update(lecLangHtmlTitle);
            return Save();
        }

        public bool DeleteHtmlTitle(LecLangHtmlTitle lecLangHtmlTitle)
        {
            _context.Remove(lecLangHtmlTitle);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public LecLangHtmlTitle GetHtmlTitle(int id)
        {
            return _context.LecLangHtmlTitles.Where(h => h.Id == id).FirstOrDefault();
        }

        public ICollection<LecLangHtmlTitle> GetHtmlTitles()
        {
            return _context.LecLangHtmlTitles.ToList();
        }

        internal bool CreateHtmlTitle(object language, LecLangHtmlTitle htmlTitleMap)
        {
            throw new NotImplementedException();
        }
    }
}