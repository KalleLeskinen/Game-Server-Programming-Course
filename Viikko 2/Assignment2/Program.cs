using System;
using System.Collections.Generic;
using System.Linq;
using Assignment2.World;


namespace Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Assignment 2!\n");


            //1. Creating 1 000 000 players

            List<Player> Players = new List<Player>();

            for (int i = 0; i < 1000000; i++)
            {
                Player p = new Player();
                p.Id = Guid.NewGuid();
                Players.Add(p);
            }

            Console.WriteLine("Players: " + Players.Count);

            //1.1 Check for duplicates

            if (Players.Count != Players.Distinct().Count())
            {
                Console.WriteLine("Duplicate GUID's detected");
            }


            Console.WriteLine("Player 0's GUID " + Players[0].Id + "\n...\n");


            Random rand = new Random();

            //Populating the players inventories
            foreach(Player p in Players)
            {
                for(int i = 0; i < rand.Next(2, 6); i++)
                {
                    p.AddItem(rand.Next(1, 100));
                }

            }

            Player playerZero = Players[0];

            //2 Extension method, Write code that creates a player with various items and use the GetHighestLevelItem function to find the highest level item and print the result.
            Console.WriteLine("\n2.\n-----------------------------------");

            CheckPlayerItemLevel(playerZero);

            //3 LINQ, Create two variations of a function that takes a Player as a parameter and returns all Item objects in the list as an array.
            Console.WriteLine("\n3.\n-----------------------------------");
            
            CheckPlayersItems(playerZero);

            //4 LINQ2, Create two variations of a function that takes a Player as a parameter and returns the first Item object in the Item-list owned by the player.
            Console.WriteLine("\n4.\n-----------------------------------");

            CheckFirstPlayerItem(playerZero);

            //5 Delegates, Implement a function with signature void ProcessEachItem(Player player, Action<Item> process);. This function should call the delegate on each of the Item objects owned by the Player object.
            Console.WriteLine("\n5.\n-----------------------------------");

            ProcessPlayerItems(playerZero);

            //6 Lamda, Call the ProcessEachItem function (implemented in the previous exercise) with a lambda function that does the same thing as the function in the previous excercise (so print the Id and Level to the console).
            Console.WriteLine("\n6.\n-----------------------------------");

            ProcessPlayerItemsWithLambda(playerZero);


            Console.WriteLine("\nPress ANY key to exit...");
            Console.ReadKey();

        }

        //2. Checks the players inventory and gets the highest level item
        static private void CheckPlayerItemLevel(Player p)
        {
            Console.WriteLine("\nPlayer 0's highest level item is...");
            Item hL = p.GetHighestLevelItem();
            Console.WriteLine("id: " + hL.Id + " level: " + hL.Level);
        }

        //3. Prints out the players inventory
        static private void CheckPlayersItems(Player p)
        {
            CheckPlayersItemsNormally(p);
            CheckPlayersItemsWithLINQ(p);
        }

        //3.1 Checks the players inventory using normal C# loops
        static private void CheckPlayersItemsNormally(Player p)
        {

            var arr = p.GetItems();
            Console.WriteLine("\nArray using normal c# loops");
            Console.WriteLine("Player 0's inventory\n");

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("Item " + (i + 1) + " | " + arr[i].ToString());
            }
        }

        //3.2 Checks the players inventory using LINQ
        static private void CheckPlayersItemsWithLINQ(Player p)
        {
            var arr = p.GetItemsWithLinq();
            Console.WriteLine("\nArray using LINQ");
            Console.WriteLine("Player 0's inventory\n");

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("Item " + (i + 1) + " | " + arr[i].ToString());
            }
        }

        //4. Prints out the first item in players inventory
        static private void CheckFirstPlayerItem(Player p)
        {
            CheckFirstPlayerItemNormally(p);
            CheckFirstPlayerItemLINQ(p);
        }


        //4.1 Prints out the first item using normal C#
        static private void CheckFirstPlayerItemNormally(Player p)
        {
            Console.WriteLine("\nChecking player inventory with normal C#");
            var item = p.GetFirstPlayerItem();
            Console.WriteLine("First item in inventory: " + item.ToString());
        }

        //4.2 Prints out the first item using LINQ
        static private void CheckFirstPlayerItemLINQ(Player p)
        {
            Console.WriteLine("\nChecking player inventory with LINQ");
            var item = p.GetFirstPlayerItemWithLINQ();
            Console.WriteLine("First item in inventory: " + item.ToString());
        }

        //5 Delegates

        static private void ProcessPlayerItems(Player p)
        {
            Console.WriteLine("\nProcessing player items... \n");
            ProcessEachItem(p, PrintItem);
        }

        //5.1
        static private void PrintItem(Item item)
        {
            Console.WriteLine(item.ToString());
        }

        private static void ProcessEachItem(Player player, Action<Item> process)
        {
            foreach(Item i in player.Items)
            {
                process(i);
            }
        }


        //6 Lambda

        static private void ProcessPlayerItemsWithLambda(Player p)
        {
            Console.WriteLine("\nProcessing player items with Lambda...");
            ProcessEachItem(p, item => Console.WriteLine(item.ToString()));
        }


    }
}
