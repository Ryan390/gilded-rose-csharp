using System;
using csharp.Domain;
using csharp.Repositories;

namespace csharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var items = new ItemRepository().GetAll();

            Console.WriteLine("OMGHAI!");

            var app = new GildedRose(new ExpirationManager(), items);
            
            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < items.Count; j++)
                {
                    Console.WriteLine(items[j]);
                }

                Console.WriteLine("");
                app.UpdateQuality();
            }
        }
    }
}
