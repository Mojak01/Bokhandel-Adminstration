using BokhandelAdminstration.Services;

var storeService = new StoreServices();
var bookService = new Bookservices();
var authorService = new AuthorServices();

bool kör = true;

while (kör)
{
    Console.WriteLine();
    Console.WriteLine("Vällkommen till Bokhandel");
    Console.WriteLine("1. Visa lager per butik");
    Console.WriteLine("2. Lägg till bok i butik");
    Console.WriteLine("3. Ta bort bok från butik");
    Console.WriteLine("4. Skapa ny bok");
    Console.WriteLine("5. Uppdatera bok");
    Console.WriteLine("6. Ta bort bok");
    Console.WriteLine("7. Skapa författare");
    Console.WriteLine("8. Uppdatera författare");
    Console.WriteLine("9. Ta bort författare");
    Console.WriteLine("0. Avsluta");
    Console.WriteLine("Välj ett alternativ:");

    string val = Console.ReadLine();

    switch (val)
    {
        case "1":
            await storeService.VisaLagerPerButikAsync();
            break;

        case "2":
            await storeService.LäggTillBokIButikAsync();
            break;

        case "3":
            await storeService.TaBortBokFrånButikAsync();
            break;

        case "4":
            await bookService.SkapaNyBokAsync();
            break;

        case "5":
            await bookService.UppdateraBokAsync();
            break;

        case "6":
            await bookService.TaBortBokAsync();
            break;

        case "7":
            await authorService.SkapaFörfattareAsync();
            break;

        case "8":
            await authorService.UppdateraFörfattareAsync();
            break;

        case "9":
            await authorService.TaBortFörfattareAsync();
            break;

        case "0":
            kör = false;
            break;

        default:
            Console.WriteLine("Fel val, försök igen.");
            break;
    }
}
