using csharp.Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public  class ExpirationManagerTests
    {
        private ExpirationManager _manager;

        [SetUp]
        public void SetUp()
        {
            _manager = new ExpirationManager();
        }

        [Test]
        public void Given_NonExpiredItem_Then_ExpiryReducedOnce()
        {
            var item = new Item
            {
                Name = "Maneki-neko",
                SellIn = 100,
                Quality = 10
            };

            _manager.ManageExpiration(item);

            Assert.AreEqual(99, item.SellIn);
        }

        [Test]
        public void Given_ExpiredAgedBrie_AndQualityLessThan50_ThenQualityIncreased()
        {
            var item = new Item
            {
                Name = "Aged Brie",
                SellIn = 0,
                Quality = 49
            };

            _manager.ManageExpiration(item);

            Assert.AreEqual(50, item.Quality);
        }

        [Test]
        public void Given_ExpiredConcertTicket_ThenQualityIsNegated()
        {
            var item = new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 0,
                Quality = 30
            };

            _manager.ManageExpiration(item);

            Assert.AreEqual(0, item.Quality);
        }
    }
}
