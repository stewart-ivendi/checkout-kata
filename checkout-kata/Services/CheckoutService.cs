using System.Diagnostics.CodeAnalysis;

namespace checkout_kata.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IPriceRepository _priceRepository;
    private readonly List<string> _basket = new();

    public CheckoutService(IPriceRepository priceRepository)
    {
        _priceRepository = priceRepository;
    }

    public int GetTotalPrice()
    {
        var runningTotal = 0;

        foreach (var itemGroup in _basket.GroupBy(x => x).Select(x => (x.Key, x.Count())))
        {
            runningTotal += _priceRepository.GetStockItemRecord(itemGroup.Key) switch
            {
                StockItemRecord i when i.OfferQuantity is null || i.OfferPrice is null => i.Price * itemGroup.Item2,
                StockItemRecord i => (itemGroup.Item2 % i.OfferQuantity.Value * i.Price) + (itemGroup.Item2 / i.OfferQuantity.Value * i.OfferPrice.Value),
                _ => 0
            };
        }

        return runningTotal;
    }

    public void Scan(string item)
    {
        _basket.Add(item);
    }
}
