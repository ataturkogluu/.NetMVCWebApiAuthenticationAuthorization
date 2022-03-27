using DestekAybey.Models;
using JWTAuthentication.NET6._0.Auth;

namespace AuthenticationAndAuthorization.Repository
{
    public class LecErrorCodeRepo
    {
        private ApplicationDbContext _context;

        public LecErrorCodeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ErrorCodeExists(int id)
        {
            return _context.LecErrorCodes.Any(l => l.Id == id);
        }        

        public bool ErrorNameExists(string errorName)
        {
            return _context.LecErrorCodes.Any(e => e.ErrorName == errorName);
        }

        public bool CreateErrorCode(int productSerieId, LecErrorCode lecErrorCode)
        {
            _context.Add(lecErrorCode);
            return Save();
        }

        public bool DeleteErrorCode(LecErrorCode lecErrorCode)
        {
            _context.Remove(lecErrorCode);
            return Save();
        }

        public ICollection<LecErrorCode> GetErrorCodes()
        {
            return _context.LecErrorCodes.ToList();
        }

        public LecErrorCode GetErrorCode(int id)
        {
            return _context.LecErrorCodes.Where(l => l.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateErrorCode(LecErrorCode lecErrorCode)
        {
            _context.Update(lecErrorCode);
            return Save();
        }

        public ICollection<LecErrorCode> GetErrorCodesByErrorName(int languageId, int productSerieId, string errorName)
        {

            var errorCodes = _context.LecErrorCodes.Where(e => 
                e.ErrorName.Contains(errorName) && 
                e.LecLanguage.Id == languageId //&& e => e.productSerieId == productSerieId
            ).ToList();

            return errorCodes;

        }

        public ICollection<LecErrorCode> GetErrorNamesByErrorCode(int languageId, int productSerieId, int errorCode)
        {
            return _context.LecErrorCodes.Where(e => e.ErrorCode == errorCode).ToList();
        }

        public ICollection<LecErrorCode> GetErrorNamesErrorCodes(int languageId, int productSerieId)
        {
            return _context.LecErrorCodes.Where(e =>  
                e.LecLanguage.Id == languageId
            ).ToList();
        }
    }
}
