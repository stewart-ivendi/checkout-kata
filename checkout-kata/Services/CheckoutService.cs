namespace checkout_kata.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IPriceRepository _priceRepository;

    public CheckoutService(IPriceRepository priceRepository)
    {
        _priceRepository = priceRepository;
    }

    public int GetTotalPrice()
    {
        return 0;
    }

    public void Scan(string item)
    {
        throw new NotImplementedException();
    }
}
