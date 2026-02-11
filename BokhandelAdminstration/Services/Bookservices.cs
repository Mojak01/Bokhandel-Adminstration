using BokhandelAdminstration.Models;
using Microsoft.EntityFrameworkCore;

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

            var förlag = await _context.Förlags.ToListAsync();
            Console.WriteLine("Välj Förlag:");
            foreach (var f in förlag)
            {
                Console.WriteLine($"{f.Id}: {f.Namn}");
            }
            int förlagId = int.Parse(Console.ReadLine());

            var kategorier = await _context.Kategoriers.ToListAsync();
            Console.WriteLine("Välj Kategori:");
            foreach (var k in kategorier)
            {
                Console.WriteLine($"{k.Id}: {k.Namn}");
            }
            int kategoriId = int.Parse(Console.ReadLine());



























        }

    }
}