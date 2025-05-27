using MongoDB.Driver;
using labb_3_databas.Models;

namespace labb_3_databas.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService()
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGO_CONN_STRING");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("BookstoreDB");
            _books = database.GetCollection<Book>("Books");
        }

        public List<Book> Get() => _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}
