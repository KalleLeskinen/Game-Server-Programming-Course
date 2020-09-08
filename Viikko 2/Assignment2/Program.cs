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



            //2 Extension method, Write code that creates a player with various items and use the GetHighestLevelItem function to find the highest level item and print the result.
            Console.WriteLine("\n2.\n-----------------------------------\n");

            CheckPlayerItemLevel(Players[0]);

            //3 LINQ, Create two variations of a function that takes a Player as a parameter and returns all Item objects in the list as an array.
            Console.WriteLine("\n3.\n-----------------------------------\n");
            
            CheckPlayersItems(Players[0]);

            //4 LINQ2, Create two variations of a function that takes a Player as a parameter and returns the first Item object in the Item-list owned by the player.
            Console.WriteLine("\n3.\n-----------------------------------\n");

            //CheckFirstPlayerItem(Players[0]);


        }

        //2. Checks the players inventory and gets the highest level item
        static private void CheckPlayerItemLevel(Player p)
        {
            Console.WriteLine("Player 0's highest level item is...");
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
                Console.WriteLine("Item " + (i + 1) + " | id: " + arr[i].Id + " level: " + arr[i].Level);
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
                Console.WriteLine("Item " + (i + 1) + " | id: " + arr[i].Id + " level: " + arr[i].Level);
            }
        }

        //4. Prints out the first item in players inventory
        static private void CheckFirstPlayerItem(Player p)
        {

        }

        //4.1 Prints out the first item using normal C#
        static private void CheckFirstPlayerItemNormally(Player p)
        {

        }

        //4.2 Prints out the first item using LINQ
        static private void CheckFirstPlayerItemLINQ(Player p)
        {

        }


    }
}
