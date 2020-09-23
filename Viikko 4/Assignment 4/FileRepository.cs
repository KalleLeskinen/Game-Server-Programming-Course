using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Data;

namespace Assignment_4
{
    public class FileRepository : IRepository
    {
        //Creates the player
        public async Task<Player> Create(Player player)
        {

            Player[] playersArray = await ReadFile();

            List<Player> playerList = playersArray.ToList();

            playerList.Add(player);

            WriteFile(playerList.ToArray());

            return player;

        }

        //Deletes the player
        public async Task<Player> Delete(Guid id)
        {

            Player[] players = await ReadFile();

            //temp list
            List<Player> playerlist = players.ToList();

            //finding player in list
            Player found = players.Where(x => x.Id == id).FirstOrDefault();

            //removing from list
            playerlist.Remove(found);

            //creating a array from the list
            WriteFile(playerlist.ToArray());

            return found;
        }

        public async Task<Item> CreateItem(Guid playerId, Item item)
        {

            Player tempPlayer = await Get(playerId);

            tempPlayer.Items.Add(item);

            await UpdatePlayerItems(playerId, tempPlayer);

            return item;
        }

        public async Task<Item[]> GetAllItems(Guid playerId)
        {
            Player tempPlayer = await Get(playerId);

            return tempPlayer.Items.ToArray();
        }

        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            Player tempPlayer = await Get(playerId);

            Item found = tempPlayer.Items.Find(x => x.Id == itemId);

            return found;
        }

        public async Task<Item> UpdateItem(Guid playerId, Item item)
        {

            Player tempPlayer = await Get(playerId);

            tempPlayer.Items.Where(x => x.Id == item.Id).FirstOrDefault().Level = item.Level;

            await UpdatePlayerItems(playerId, tempPlayer);

            return item;

        }

        public async Task<Item> DeleteItem(Guid playerId, Item item)
        {
            Player tempPlayer = await Get(playerId);

            var found = tempPlayer.Items.Where(x => x.Id == item.Id).FirstOrDefault();

            tempPlayer.Items.Remove(found);

            await UpdatePlayerItems(playerId, tempPlayer);

            return item;
        }

        public async Task<Player> Get(Guid id)
        {
            Player[] players = await ReadFile();
            return players.Where(x => x.Id == id).FirstOrDefault();
        }

        //Returns a array of all the players
        public async Task<Player[]> GetAll()
        {
            Player[] temp = await ReadFile();
            return temp;
        }

        //Updates the players items
        public async Task<Player> UpdatePlayerItems(Guid id, Player player)
        {
            Player[] players = await ReadFile();
            players.Where(x => x.Id == id).FirstOrDefault().Items = new List<Item>();
            players.Where(x => x.Id == id).FirstOrDefault().Items = player.Items;
            WriteFile(players);

            return players.Where(x => x.Id == id).FirstOrDefault();
        }

        //Modifies the player score
        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            Player[] players = await ReadFile();

            players.Where(x => x.Id == id).FirstOrDefault().Score = player.Score;

            WriteFile(players);

            return players.Where(x => x.Id == id).FirstOrDefault();
        }

        async Task<Player[]> ReadFile()
        {
            String jsonS = "";

            using (var reader = File.OpenText("game-dev.txt"))
            {
                jsonS = await reader.ReadToEndAsync();
            }

            if (jsonS.Equals(""))
            {
                return new Player[0];
            }

            Player[] playerlist = JsonConvert.DeserializeObject<Player[]>(jsonS);
            return playerlist;
        }

        void WriteFile(Player[] players)
        {
            File.WriteAllText(Path.GetFullPath("game-dev.txt"), JsonConvert.SerializeObject(players));
        }
    }
}
