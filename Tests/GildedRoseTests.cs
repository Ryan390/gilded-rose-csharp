using csharp;
using csharp.Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GildedRoseTests
    {
        [Test]
        public void When_UpdateQualityAndSellBy_ThenItemNameIsUnchanged()
        {
            var items = new List<Item> { new Item { Name = "Test Item" } };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.AreEqual("Test Item", actualResult[0].Name);
        }

        [Test]
        public void When_NonSpecialItem_Then_QualityAndSellByIsDecremented()
        {
            var items = new List<Item> { new Item { Name = "WoW Expansion", Quality = 10, SellIn = 10 } };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.AreEqual(9, actualResult[0].SellIn);
            Assert.AreEqual(9, actualResult[0].Quality);
        }

        [Test]
        public void When_After5Days_Then_ItemHasCorrectQualityAndSellBy()
        {
            var actualResult = new List<Item>();
            var items = new List<Item> { new Item { Name = "Starfish", Quality = 10, SellIn = 10 } };

            for (int i = 0; i < 5; i++)
            {
                actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();
            }

            Assert.AreEqual(5, actualResult[0].SellIn);
            Assert.AreEqual(5, actualResult[0].Quality);
        }

        [Test]
        public void When_QualityAlreadyZero_Then_QualityUnchanged()
        {
            var items = new List<Item> { new Item { Name = "Degraded Starfish", Quality = 0, SellIn = 0} };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.AreEqual(0, actualResult[0].Quality);
            Assert.AreEqual(-1, actualResult[0].SellIn);
        }

        [Test]
        public void When_SellInLessThanZero_Then_DecreaseQuality()
        {
            var items = new List<Item> { new Item { Name = "Time Bomb", Quality = 10, SellIn = -1 } };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.AreEqual(8, actualResult[0].Quality);
            Assert.AreEqual(-2, actualResult[0].SellIn);
        }

        [Test]
        public void Given_AgedBrie_When_QualityIsLessThanFifty_Then_QualityIsIncreased()
        {
            var items = new List<Item> { new Item { Name = "Aged Brie", Quality = 49, SellIn = 10 } };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.AreEqual(50, actualResult[0].Quality);
            Assert.AreEqual(9, actualResult[0].SellIn);
        }

        [Test]
        public void Given_AgedBrie_When_SellInZero_Then_QualityIsIncreasedTwice()
        {
            var items = new List<Item> { new Item { Name = "Aged Brie", Quality = 48, SellIn = 0 } };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.AreEqual(50, actualResult[0].Quality);
            Assert.AreEqual(-1, actualResult[0].SellIn);
        }

       [TestCase(10, 10)]
       [TestCase(5, 11)]
       [TestCase(0, 3)]
       [TestCase(0, 0)]
       [TestCase(3, -1)]
        public void Given_Sulfuras_Then_QualityAndSellIn_NeverChanged(int setAndExpectedQuality, int setAndExpectedSellIn)
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Sulfuras, Hand of Ragnaros", Quality = setAndExpectedQuality,
                    SellIn = setAndExpectedSellIn,
                    LegendaryItem = true
                }
            };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.IsEmpty(actualResult);
        }

        [Test]
        public void Given_ConcertTicket_When_QualityLessThan50_Then_QualityIsIncreased()
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 49, SellIn = 20 } };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.AreEqual(50, actualResult[0].Quality);
            Assert.AreEqual(19, actualResult[0].SellIn);
        }

        [Test]
        public void Given_ConcertTicket_When_MediumQuality_AndLowSellIn_Then_QualityIsIncreasedThreeTimes()
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 47, SellIn = 5 } };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.AreEqual(50, actualResult[0].Quality);
            Assert.AreEqual(4, actualResult[0].SellIn);
        }

        [Test]
        public void Given_ConcertTicket_When_SellInLessThan11_Then_QualityIsIncreasedTwice()
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 0, SellIn = 10 } };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.AreEqual(2, actualResult[0].Quality);
            Assert.AreEqual(9, actualResult[0].SellIn);
        }

        [Test]
        public void Given_ConcertTicket_When_SellInLessThan6_Then_QualityIsIncreasedThreeTimes()
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 0, SellIn = 5 } };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.AreEqual(3, actualResult[0].Quality);
            Assert.AreEqual(4, actualResult[0].SellIn);
        }

        [Test]
        public void Given_ConcertTicket_When_SellInIsZero_Then_AllQualityIsRemoved()
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 49, SellIn = 0 } };

            var actualResult = new GildedRose(new ExpirationManager(), items).UpdateQuality();

            Assert.AreEqual(0, actualResult[0].Quality);
            Assert.AreEqual(-1, actualResult[0].SellIn);
        }
    }
}
