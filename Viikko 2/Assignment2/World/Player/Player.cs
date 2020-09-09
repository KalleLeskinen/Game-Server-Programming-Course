using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;



namespace Assignment2.World
{
    public class Player
    {

        public Guid Id { get; set; }
        public int Score { get; set; }
        public List<Item> Items { get; set; }

        public Player()
        {
            Items = new List<Item>();
        }

        public void AddItem(int level)
        {
            Items.Add(new Item(level));
        }

        //2. GetHighestLevelItem
        public Item GetHighestLevelItem()
        {

            try
            {
                var item = (Item)Items.OrderByDescending(x => x.Level).First();
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine("Players inventory is empty!");
            }

            return new Item(0);

        }


        //3. Returns the items using normal C# loops
        public Item[] GetItems()
        {

            try
            {
                Item[] itemArray = new Item[Items.Count];

                for (int i = 0; i < Items.Count; i++)
                {
                    itemArray[i] = Items[i];
                }
                return itemArray;

            }
            catch (Exception e)
            {
                Console.WriteLine("Players inventory is empty!");
            }

            return new Item[0];

        }

        //3.1 Returns the items using Linq
        public Item[] GetItemsWithLinq()
        {

            try
            {
                var itemArray = Items.Select(x => x).ToArray();
                return itemArray;
            }
            catch (Exception e)
            {
                Console.WriteLine("Players inventory is empty!");
            }

            return new Item[0];

        }


        //4. Returns the first item using C#
        public Item GetFirstPlayerItem()
        {

            if (Items.Count != 0)
            {

                return Items[0];

            }
            else
            {
                return null;
            }

        }

        //4.1 Returns the first item using LINQ
        public Item GetFirstPlayerItemWithLINQ()
        {

            if (Items.Count != 0)
            {

                return Items.First();

            }
            else
            {
                return null;
            }    

        }


    }
}
