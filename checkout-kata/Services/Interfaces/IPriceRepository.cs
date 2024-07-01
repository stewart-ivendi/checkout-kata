namespace checkout_kata.Services.Interfaces;

public interface IPriceRepository
{
    public StockItemRecord GetStockItemRecord(string sku);
}
