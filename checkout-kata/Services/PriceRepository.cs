namespace checkout_kata.Services;

public class PriceRepository : IPriceRepository
{
    private readonly Dictionary<string, StockItemRecord> _stockPrices;

    public PriceRepository(List<StockItemRecord> stock)
    {
        _stockPrices = stock.GroupBy(x => x.Sku)
                            .ToDictionary(x => x.Key, x => x.First());
    }

    public StockItemRecord GetStockItemRecord(string sku)
        => _stockPrices.ContainsKey(sku) 
                ? _stockPrices[sku]
                : null;
}
