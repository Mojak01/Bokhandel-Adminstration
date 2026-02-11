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

        public async Task SkapaNyBokAsync()
        {
            Console.WriteLine("Ange ISBN (13 siffror):");
            string isbn = Console.ReadLine();

            Console.WriteLine("Ange titel:");
            string titel = Console.ReadLine();

            Console.WriteLine("Ange pris:");
            decimal pris = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Ange utgivningsdatum (yyyy-mm-dd):");
            DateTime utgivningsdatum = DateTime.Parse(Console.ReadLine());
        }
    }
}