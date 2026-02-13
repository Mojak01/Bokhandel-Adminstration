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

        public async Task TaBortFörfattareAsync()
        {
            await VisaAllaFörfattareAsync();

            Console.WriteLine("Ange ID på författare att ta bort:");
            int id = int.Parse(Console.ReadLine());

            var författare = await _context.Författares
                .Include(f => f.Isbn13s)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (författare == null)
            {
                Console.WriteLine("Författare hittades inte.");
                return;
            }

            författare.Isbn13s.Clear();

            _context.Författares.Remove(författare);
            await _context.SaveChangesAsync();

            Console.WriteLine("Författaren har tagits bort.");

        }

        public async Task SkapaFörfattareAsync()
        {
            Console.WriteLine("Ange förnamn:");
            string förnamn = Console.ReadLine();

            Console.WriteLine("Ange efternamn:");
            string efternamn = Console.ReadLine();

            Console.WriteLine("Ange födelsedatum (yyyy-mm-dd):");
            string datumInput = Console.ReadLine();

            DateOnly? födelsedatum = null;

            if (!string.IsNullOrWhiteSpace(datumInput))
            {
                födelsedatum = DateOnly.Parse(datumInput);
            }

            var nyFörfattare = new Författare
            {
                Förnamn = förnamn,
                Efternamn = efternamn,
                Födelsedatum = födelsedatum
            };

            _context.Författares.Add(nyFörfattare);
            await _context.SaveChangesAsync();

            Console.WriteLine("Ny författare har skapats.");
        }

    }
}
