namespace checkout_kata.Models;

public record StockItemRecord
{
    public string Sku { get; set; }
    public int Price { get; set; }
}
