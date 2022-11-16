using System.Collections.Generic;
using System.Linq;

namespace csharp.Domain
{
    public class GildedRose
    {
        private readonly IExpirationManager _expirationManager;
        private readonly IList<Item> _items;

        public GildedRose(IExpirationManager expirationManager, IList<Item> items)
        {
            _expirationManager = expirationManager;
            _items = items;
        }

        public List<Item> UpdateQuality()
        {
            var eligibleItems = _items.Where(x => !x.LegendaryItem);
            var itemList = eligibleItems.ToList();

            foreach (var item in itemList)
            {
                if (item.Name == "Aged Brie")
                {
                    HandleAgedBrie(item);
                }
                else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    HandleConcertTickets(item);
                }
                else
                {
                    HandleRegularItems(item);
                }
            }

            return itemList;
        }

        private void HandleRegularItems(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality -= 1;
            }

            _expirationManager.ManageExpiration(item);
        }

        private void HandleAgedBrie(Item item)
        {
            IncreaseQualityIfBelow50(item);
            _expirationManager.ManageExpiration(item);
        }

        private void HandleConcertTickets(Item item)
        {
            IncreaseQualityIfBelow50(item);

            if (item.Quality < 50)
            {
                if (item.SellIn < 11)
                {
                    item.Quality += 1;
                }

                if (item.SellIn < 6)
                {
                    item.Quality += 1;
                }
            }

            _expirationManager.ManageExpiration(item);
        }

        private void IncreaseQualityIfBelow50(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality += 1;
            }
        }
    }
}
