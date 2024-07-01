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
            runningTotal += _priceRepository.GetStockItemRecord(itemGroup.Key)?.Price ?? 0 * itemGroup.Item2;

        return runningTotal;
    }

    public void Scan(string item)
    {
        _basket.Add(item);
    }
}
