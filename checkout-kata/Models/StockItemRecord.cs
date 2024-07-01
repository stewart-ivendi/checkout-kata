namespace checkout_kata.Models;

public record StockItemRecord
{
    public string Sku { get; set; }
    public int Price { get; set; }

    public int? OfferQuantity { get; set; } 
    public int? OfferPrice { get; set; }
}
