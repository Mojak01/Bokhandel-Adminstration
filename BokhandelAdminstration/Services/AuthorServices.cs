using BokhandelAdminstration.Models;

namespace BokhandelAdminstration.Services
{
    public class AuthorServices
    {
        private readonly BookStoreContext _context;

        public AuthorServices()
        {
            _context = new BookStoreContext();
        }
    }
}
