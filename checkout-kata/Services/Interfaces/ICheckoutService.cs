namespace checkout_kata.Services.Interfaces;

public interface ICheckoutService
{
    void Scan(string item);
    int GetTotalPrice();
}
