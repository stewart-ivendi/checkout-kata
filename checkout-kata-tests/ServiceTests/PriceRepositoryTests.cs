namespace checkout_kata_tests.ServiceTests;

public class PriceRepositoryTests
{
    private readonly PriceRepository _priceRepository = new PriceRepository(Prices);

    [Fact]
    public void When_SkuNotFound_Then_NullReturned()
    {
        // ARRANGE - default arrangement
        // ACT

        var record = _priceRepository.GetStockItemRecord("X");

        // ASSERT

        record.Should().BeNull();
    }

    [Theory]
    [ClassData(typeof(ValidSkuData))]
    public void When_SkuIsFound_Then_RecordReturned(string sku, StockItemRecord expectedResult)
    {
        // ARRANGE - default arrangement
        // ACT

        var record = _priceRepository.GetStockItemRecord(sku);

        // ASSERT

        record.Should().BeEquivalentTo(expectedResult);
    }

    #region data

    private static StockItemRecord RecordA = new StockItemRecord { Sku = "A", Price = 50 };
    private static StockItemRecord RecordB = new StockItemRecord { Sku = "B", Price = 30 };
    private static StockItemRecord RecordC = new StockItemRecord { Sku = "C", Price = 20 };
    private static StockItemRecord RecordD = new StockItemRecord { Sku = "D", Price = 10 };

    private static List<StockItemRecord> Prices = [
        RecordA,
        RecordB,
        RecordC,
        RecordD
    ];

    public class ValidSkuData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var item in Prices)
                yield return new object[] { item.Sku, item };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion
}
