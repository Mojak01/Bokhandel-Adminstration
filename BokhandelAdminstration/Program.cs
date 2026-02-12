using BokhandelAdminstration.Services;

//var storeService = new StoreService();


//await storeService.LäggTillBokIButikAsync();

var bookService = new Bookservices();

//await bookService.SkapaNyBokAsync();

await bookService.UppdateraBokAsync();