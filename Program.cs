using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

Order order1 = new Order
{
    OrderId = 1,
    CustomerName = "Shohruh",
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


var memStream = GetStream(order1, jsonOptions);
string originalJson = GetString(memStream);
memStream.Position = 0;
Console.WriteLine(originalJson);

Console.WriteLine(new string('-', 40) + '\n');

JsonNode node = JsonNode.Parse(originalJson); // json ni JsonNode ga parse qilib olamiz
node["CustomerName"] = "Abbosjon";            // kerakli atrbutni o'zgartiramiz
string updatedJson = node.ToJsonString(jsonOptions); // jsonni jsonoptions asosida string qilib qaytaradi 
Console.WriteLine(updatedJson);               // ekranga chiqarish

Order newOrder = JsonSerializer.Deserialize<Order>(updatedJson, jsonOptions);
Console.WriteLine(newOrder.ToString());

using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
{
    memStream.CopyTo(fileStream);
    Console.WriteLine("\nMemoryStream malumotlari FileStream ga yozildi.");
}


string GetString(MemoryStream memStream) =>
    Encoding.UTF8.GetString(memStream.ToArray());

MemoryStream GetStream(Order order, JsonSerializerOptions jsonoptions)
{
    MemoryStream memStream = new MemoryStream();
    JsonSerializer.Serialize(memStream, order, jsonOptions);
    return memStream;
}