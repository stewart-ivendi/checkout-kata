namespace checkout_kata_tests.ServiceTests;

public class PriceRepositoryTests
{
    private readonly PriceRepository _priceRepository = new PriceRepository(Prices);

    [Fact]
    public void When_SkuNotFound_Then_0()
    {
        // ARRANGE - default arrangement
        // ACT

        var record = _priceRepository.GetStockItemRecord("X");

        // ASSERT

        record.Should().BeNull();
    }

    #region data

    private static List<StockItemRecord> Prices = [
        new StockItemRecord { Sku = "A", Price = 50 },
        new StockItemRecord { Sku = "B", Price = 30 },
        new StockItemRecord { Sku = "C", Price = 20 },
        new StockItemRecord { Sku = "D", Price = 10 }
    ];

    #endregion
}
