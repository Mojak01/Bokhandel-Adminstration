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

            Console.WriteLine("Ange språk:");
            string språk = Console.ReadLine();

            Console.WriteLine("Ange pris:");
            decimal pris = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Ange utgivningsdatum (yyyy-mm-dd):");
            DateOnly utgivningsdatum = DateOnly.Parse(Console.ReadLine());
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
                Console.WriteLine($"{k.Id}: {k.KategoriNamn}");
            }
            int kategoriId = int.Parse(Console.ReadLine());


            var nyBok = new Böcker
            {
                Isbn13 = isbn,
                Titel = titel,
                Språk = språk,
                Pris = pris,
                Utgivningsdatum = utgivningsdatum,
                FörlagId = förlagId,
                KategoriId = kategoriId
            };

            _context.Böckers.Add(nyBok);
            await _context.SaveChangesAsync();

            Console.WriteLine("Boken skapades.");


            var författare = await _context.Författares.ToListAsync();

            Console.WriteLine("Välj författare (ID):");
            foreach (var f in författare)
            {
                Console.WriteLine($"{f.Id}: {f.Förnamn} {f.Efternamn}");
            }

            int författareId = int.Parse(Console.ReadLine());

            var valdFörfattare = await _context.Författares
                .FirstOrDefaultAsync(f => f.Id == författareId);

            if (valdFörfattare != null)
            {
                nyBok.Författares.Add(valdFörfattare);
                await _context.SaveChangesAsync();
                Console.WriteLine("Författare kopplad till boken.");
            }























        }

    }
}