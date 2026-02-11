using BokhandelAdminstration.Models;
using Microsoft.EntityFrameworkCore;


namespace BokhandelAdminstration.Services
{

    public class StoreService
    {
        private readonly BookStoreContext _context;

        public StoreService()
        {
            _context = new BookStoreContext();
        }

        // VISAR LAGERSALDO PER BUTIK
        public async Task VisaLagerPerButikAsync()
        {
            var butiker = await _context.Butikers
                .Include(b => b.LagerSaldos)
                    .ThenInclude(ls => ls.Isbn13Navigation)
                .ToListAsync();

            foreach (var butik in butiker)
            {
                Console.WriteLine($"Butik: {butik.Butiksnamn}");

                foreach (var lager in butik.LagerSaldos)
                {
                    Console.WriteLine($"Bok: {lager.Isbn13Navigation.Titel} | Antal: {lager.Antal}");
                }

                Console.WriteLine();
            }
        }

        // LÄGGER TILL BOK I BUTIK
        public async Task LäggTillBokIButikAsync()
        {
            var butiker = await _context.Butikers.ToListAsync();

            Console.WriteLine("Välj butik (skriv siffran):");
            foreach (var butik in butiker)
            {
                Console.WriteLine($"{butik.Id}: {butik.Butiksnamn}");
            }

            int butikId = int.Parse(Console.ReadLine());

            var böcker = await _context.Böckers.ToListAsync();

            Console.WriteLine("Välj bok (skriv ISBN):");
            foreach (var bok in böcker)
            {
                Console.WriteLine($"{bok.Isbn13}: {bok.Titel}");
            }

            string isbn = Console.ReadLine();

            Console.WriteLine("Ange antal:");
            int antal = int.Parse(Console.ReadLine());

            var lagerSaldo = new LagerSaldo
            {
                ButikId = butikId,
                Isbn13 = isbn,
                Antal = antal
            };

            _context.LagerSaldos.Add(lagerSaldo);
            await _context.SaveChangesAsync();

            Console.WriteLine("Boken har lagts till i butiken.");
        }

        //TA BORT BÖCKER FRÅN BUTIKERNA
        public async Task TaBortBokFrånButikAsync()
        {
            
            var butiker = await _context.Butikers.ToListAsync();

            Console.WriteLine("Välj butik (skriv siffran):");
            foreach (var butik in butiker)
            {
                Console.WriteLine($"{butik.Id}: {butik.Butiksnamn}");
            }

            int butikId = int.Parse(Console.ReadLine());

            
            var lagerPoster = await _context.LagerSaldos
                .Include(ls => ls.Isbn13Navigation)
                .Where(ls => ls.ButikId == butikId)
                .ToListAsync();

            if (lagerPoster.Count == 0)
            {
                Console.WriteLine("Det finns inga böcker i denna butik.");
                return;
            }


            Console.WriteLine("Välj bok att ta bort (skriv ISBN):");
            foreach (var lager in lagerPoster)
            {
                Console.WriteLine($"{lager.Isbn13}: {lager.Isbn13Navigation.Titel}");
            }

            string isbn = Console.ReadLine();


            var lagerRad = await _context.LagerSaldos
                .FirstOrDefaultAsync(ls => ls.ButikId == butikId && ls.Isbn13 == isbn);

            if (lagerRad == null)
            {
                Console.WriteLine("Boken hittades inte i lagret.");
                return;
            }


            _context.LagerSaldos.Remove(lagerRad);
            await _context.SaveChangesAsync();

            Console.WriteLine("Boken har tagits bort från butiken.");
        }



    }


}

















  
