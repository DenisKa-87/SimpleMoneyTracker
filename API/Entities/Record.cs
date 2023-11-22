using System.Text.Json.Serialization;

namespace API.Entities
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RecordType : byte
    {
        Expense = 1,
        Income = 2
    }
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AppUser AppUser { get; set; }
        public double Value { get; set; }
        public int RecordType { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}

