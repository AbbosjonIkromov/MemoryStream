using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

Order order1 = new Order
{
    OrderId = 1,
    CustomerName = "Abbos",
    TotalAmount = 500_000,
    Status = Status.Pending,
    OrderItems = new List<OrderItem>
    {
        new()
        {
            ItemName = "pencil",
            Quantity = 4,
            Price = 3_000
        },
        new()
        {
            ItemName = "pen",
            Quantity = 2,
            Price = 2_500
        }

    }

};

string path = @"C:\Users\Lenovo 5i Pro\MemoryStream\_json.json";

var jsonOptions = new JsonSerializerOptions()
{
    PropertyNameCaseInsensitive = true,
    WriteIndented = true,
    Converters = { new JsonStringEnumConverter() },
    ReferenceHandler = ReferenceHandler.Preserve
};

string text = JsonSerializer.Serialize(order1, jsonOptions);

using MemoryStream memStream = new MemoryStream();
JsonSerializer.Serialize(memStream, order1, jsonOptions);
string originalJson = Encoding.UTF8.GetString(memStream.ToArray());
Console.WriteLine(originalJson);

Console.WriteLine(new string('-', 40) + '\n');

string updatedJson = Encoding.UTF8.GetString(memStream.ToArray());
updatedJson = updatedJson.Replace("\"CustomerName\": \"Abbos\"", "\"CustomerName\": \"Abbosjon\"");
Console.WriteLine(updatedJson);

Order newOrder = JsonSerializer.Deserialize<Order>(updatedJson, jsonOptions);
Console.WriteLine(newOrder.ToString());



using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
{
    memStream.Seek(0, SeekOrigin.Begin);
    memStream.CopyTo(fileStream);
    Console.WriteLine("\nMemoryStream malumotlari FileStream ga yozildi.");
}


