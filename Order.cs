public class Order
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalAmount { get; set; }
    public Status Status { get; set; }
    public List<OrderItem> OrderItems { get; set; }

    public override string ToString()
    {
        return $"OlderId: {OrderId}\nCustomerName: {CustomerName}\nTotalAmout: {TotalAmount}\nStatus: {Status}";
    }
}

public enum Status
{
    Pending = 1,
    Shipper,
    Delivered
}