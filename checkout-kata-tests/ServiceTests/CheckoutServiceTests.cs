namespace checkout_kata_tests.ServiceTests;

public class CheckoutServiceTests
{
    private readonly Mock<IPriceRepository> _priceRepository = new();


    [Fact]
    public void When_EmptyBasket_Then_TotalPrice_0()
    {
        // ARRANGE

        var checkout = new CheckoutService(_priceRepository.Object);

        // ACT

        var result = checkout.GetTotalPrice();

        // ASSERT

        result.Should().Be(0);

    }

    [Fact]
    public void When_OneOfEachValidSkuInBasket_ThenExpectedPrice_110()
    {
        // ARRANGE
     
        // default behaviour : Unknown price
        _priceRepository.Setup(x => x.GetStockItemRecord(It.IsAny<string>())).Returns((StockItemRecord)null);
        
        _priceRepository.Setup(x => x.GetStockItemRecord("A")).Returns(RecordA);
        _priceRepository.Setup(x => x.GetStockItemRecord("B")).Returns(RecordB);
        _priceRepository.Setup(x => x.GetStockItemRecord("C")).Returns(RecordC);
        _priceRepository.Setup(x => x.GetStockItemRecord("D")).Returns(RecordD);

        var checkout = new CheckoutService(_priceRepository.Object);

        // ACT

        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("C");
        checkout.Scan("D");

        var cost = checkout.GetTotalPrice();

        // ASSERT

        cost.Should().Be(110);
    }

    #region data

    private static StockItemRecord RecordA = new StockItemRecord { Sku = "A", Price = 50 };
    private static StockItemRecord RecordB = new StockItemRecord { Sku = "B", Price = 30 };
    private static StockItemRecord RecordC = new StockItemRecord { Sku = "C", Price = 20 };
    private static StockItemRecord RecordD = new StockItemRecord { Sku = "D", Price = 10 };

    #endregion
}