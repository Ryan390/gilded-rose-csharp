namespace csharp.Domain
{
    public class ExpirationManager :IExpirationManager
    {
        public void ManageExpiration(Item item)
        {
            item.SellIn -= 1;

            if (Expired(item))
            {
                if (item.Name == "Aged Brie")
                {
                    if (item.Quality < 50)
                    {
                        item.Quality += 1;
                    }
                }

                else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    item.Quality -= item.Quality;
                }

                else
                {
                    if (item.Quality > 0)
                    {
                        item.Quality -= 1;
                    }
                }
            }
        }

        private bool Expired(Item item)
        {
            return item.SellIn < 0;
        }
    }
}