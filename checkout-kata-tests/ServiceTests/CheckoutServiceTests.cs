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
    public void When_OnlyNonExistantItems_Then_TotalPrice_0()
    {
        // ARRANGE

        // default behaviour : Unknown price
        _priceRepository.Setup(x => x.GetStockItemRecord(It.IsAny<string>())).Returns((StockItemRecord)null);
        // Specified Prices
        _priceRepository.Setup(x => x.GetStockItemRecord("A")).Returns(RecordA);
        _priceRepository.Setup(x => x.GetStockItemRecord("B")).Returns(RecordB);
        _priceRepository.Setup(x => x.GetStockItemRecord("C")).Returns(RecordC);
        _priceRepository.Setup(x => x.GetStockItemRecord("D")).Returns(RecordD);

        var checkout = new CheckoutService(_priceRepository.Object);

        // ACT

        checkout.Scan("W");
        checkout.Scan("X");
        checkout.Scan("Y");
        checkout.Scan("Z");

        var cost = checkout.GetTotalPrice();

        // ASSERT

        cost.Should().Be(0);
    }

    [Fact]
    public void When_OneOfEachValidSkuInBasket_Then_ExpectedPrice_115()
    {
        // ARRANGE
     
        // default behaviour : Unknown price
        _priceRepository.Setup(x => x.GetStockItemRecord(It.IsAny<string>())).Returns((StockItemRecord)null);
        // Specified Prices
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

        cost.Should().Be(115);
    }

    [Theory]
    [InlineData("A", 1, 50)]
    [InlineData("A", 2, 100)]
    [InlineData("A", 3, 130)]
    [InlineData("A", 4, 180)]
    [InlineData("A", 5, 230)]
    [InlineData("A", 6, 260)]
    [InlineData("A", 7, 310)]
    [InlineData("B", 1, 30)]
    [InlineData("B", 2, 45)]
    [InlineData("B", 3, 75)]
    [InlineData("B", 4, 90)]
    [InlineData("B", 5, 120)]
    [InlineData("C", 1, 20)]
    [InlineData("C", 2, 40)]
    [InlineData("C", 3, 60)]
    [InlineData("C", 4, 80)]
    [InlineData("D", 1, 15)]
    [InlineData("D", 2, 30)]
    [InlineData("D", 3, 45)]
    [InlineData("D", 4, 60)]
    public void When_MultipleSameItem_Then_ExpectedPrice(string sku, int quantity, int expectedCost)
    {
        // ARRANGE

        // default behaviour : Unknown price
        _priceRepository.Setup(x => x.GetStockItemRecord(It.IsAny<string>())).Returns((StockItemRecord)null);
        // Specified Prices
        _priceRepository.Setup(x => x.GetStockItemRecord("A")).Returns(RecordA);
        _priceRepository.Setup(x => x.GetStockItemRecord("B")).Returns(RecordB);
        _priceRepository.Setup(x => x.GetStockItemRecord("C")).Returns(RecordC);
        _priceRepository.Setup(x => x.GetStockItemRecord("D")).Returns(RecordD);

        var checkout = new CheckoutService(_priceRepository.Object);

        // ACT

        for (int i = 0; i < quantity; i++) {
            checkout.Scan(sku);
        }

        var cost = checkout.GetTotalPrice();

        // ASSERT

        cost.Should().Be(expectedCost);
    }

    #region data

    private static StockItemRecord RecordA = new StockItemRecord { Sku = "A", Price = 50, OfferQuantity = 3, OfferPrice = 130 };
    private static StockItemRecord RecordB = new StockItemRecord { Sku = "B", Price = 30, OfferQuantity = 2, OfferPrice = 45 };
    private static StockItemRecord RecordC = new StockItemRecord { Sku = "C", Price = 20 };
    private static StockItemRecord RecordD = new StockItemRecord { Sku = "D", Price = 15 };

    #endregion
}