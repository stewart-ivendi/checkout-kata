namespace checkout_kata_tests.ServiceTests;

public class CheckoutServiceTests
{
    [Fact]
    public void When_EmptyBasket_Then_TotalPrice_0()
    {
        // ARRANGE

        var checkout = new CheckoutService();

        // ACT

        var result = checkout.GetTotalPrice();

        // ASSERT

        result.Should().Be(0);

    }
}