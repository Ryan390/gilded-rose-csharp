namespace csharp.Domain
{
    public class Item
    {
        public string Name { get; set; }
        public bool LegendaryItem { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        public override string ToString()
        {
            return Name + ", " + SellIn + ", " + Quality;
        }  
    }
}
