using BokhandelAdminstration.Models;

namespace BokhandelAdminstration.Services
{
    public class Bookservices
    {
        private readonly BookStoreContext _context;

        public Bookservices()
        {
            _context = new BookStoreContext();
        }
    }
}
