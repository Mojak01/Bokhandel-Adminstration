using BokhandelAdminstration.Models;
using Microsoft.EntityFrameworkCore;

namespace BokhandelAdminstration.Services
{
    public class AuthorServices
    {
        private readonly BookStoreContext _context;

        public AuthorServices()
        {
            _context = new BookStoreContext();
        }

        public async Task VisaAllaFörfattareAsync()
        {
            var författare = await _context.Författares.ToListAsync();

            foreach (var f in författare)
            {
                Console.WriteLine($"{f.Id}: {f.Förnamn} {f.Efternamn}");
            }
        }

        public async Task UppdateraFörfattareAsync()
        {
            await VisaAllaFörfattareAsync();

            Console.WriteLine("Ange ID på författare att uppdatera:");
            int id = int.Parse(Console.ReadLine());

            var författare = await _context.Författares
                .FirstOrDefaultAsync(f => f.Id == id);

            if (författare == null)
            {
                Console.WriteLine("Författare hittades inte.");
                return;
            }

            Console.WriteLine("Nytt förnamn:");
            författare.Förnamn = Console.ReadLine();

            Console.WriteLine("Nytt efternamn:");
            författare.Efternamn = Console.ReadLine();

            await _context.SaveChangesAsync();

            Console.WriteLine("Författaren har uppdaterats.");
        }

    }
}
