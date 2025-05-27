using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace labb_3_databas.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("Author")]
        public string Author { get; set; } = string.Empty;

        [BsonElement("Price")]
        public decimal Price { get; set; }
    }
}

    