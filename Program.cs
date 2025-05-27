using labb_3_databas.Models;
using labb_3_databas.Services;

class Program
{
    static void Main(string[] args)
    {
        var bookService = new BookService();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("📚 BOKHANDEL - MONGO DB");
            Console.WriteLine("1. Visa alla böcker");
            Console.WriteLine("2. Lägg till bok");
            Console.WriteLine("3. Uppdatera bok");
            Console.WriteLine("4. Ta bort bok");
            Console.WriteLine("5. Avsluta");
            Console.Write("Välj ett alternativ: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    var books = bookService.Get();
                    foreach (var book in books)
                    {
                        Console.WriteLine($"{book.Id} - {book.Title} av {book.Author} - {book.Price} kr");
                    }
                    Pause();
                    break;

                case "2":
                    Console.Write("Titel: ");
                    var title = Console.ReadLine() ?? "";
                    Console.Write("Författare: ");
                    var author = Console.ReadLine() ?? "";
                    Console.Write("Pris: ");
                    var priceInput = Console.ReadLine();
                    if (!decimal.TryParse(priceInput, out var price))
                    {
                        Console.WriteLine("❌ Ogiltigt pris, försök igen.");
                        Pause();
                        break;
                    }

                    var newBook = new Book { Title = title, Author = author, Price = price };
                    bookService.Create(newBook);
                    Console.WriteLine("✅ Bok tillagd!");
                    Pause();
                    break;

                case "3":
                    Console.Write("Ange ID på boken du vill uppdatera: ");
                    var updateId = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(updateId))
                    {
                        Console.WriteLine("❌ Ogiltigt ID.");
                        Pause();
                        break;
                    }

                    var bookToUpdate = bookService.Get(updateId);
                    if (bookToUpdate == null)
                    {
                        Console.WriteLine("❌ Bok hittades inte.");
                    }
                    else
                    {
                        Console.Write("Ny titel: ");
                        bookToUpdate.Title = Console.ReadLine() ?? bookToUpdate.Title;
                        Console.Write("Ny författare: ");
                        bookToUpdate.Author = Console.ReadLine() ?? bookToUpdate.Author;
                        Console.Write("Nytt pris: ");
                        var newPriceInput = Console.ReadLine();
                        if (decimal.TryParse(newPriceInput, out var newPrice))
                        {
                            bookToUpdate.Price = newPrice;
                        }

                        bookService.Update(updateId, bookToUpdate);
                        Console.WriteLine("✅ Bok uppdaterad!");
                    }
                    Pause();
                    break;

                case "4":
                    Console.Write("Ange ID på boken du vill ta bort: ");
                    var deleteId = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(deleteId))
                    {
                        Console.WriteLine("❌ Ogiltigt ID.");
                        Pause();
                        break;
                    }

                    bookService.Remove(deleteId);
                    Console.WriteLine("🗑️ Bok borttagen!");
                    Pause();
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("❗ Ogiltigt val, försök igen.");
                    Pause();
                    break;
            }
        }
    }

    static void Pause()
    {
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }
}

