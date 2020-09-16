using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Assignment_3
{
    public class FileRepository : IRepository
    {
        public async Task<Player> Create(Player player)
        {

            Player[] playersArray = await ReadFile();

            List<Player> playerList = playersArray.ToList();

            playerList.Add(player);

            WriteFile(playerList.ToArray());

            return player;

        }

        public async Task<Player> Delete(Guid id)
        {

            Player[] players = await ReadFile();

            List<Player> playerlist = players.ToList();

            Player found = players.Where(x => x.Id == id).FirstOrDefault();

            playerlist.Remove(found);

            WriteFile(playerlist.ToArray());

            return found;
        }

        public async Task<Player> Get(Guid id)
        {
            Player[] players = await ReadFile();
            return players.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<Player[]> GetAll()
        {
            Player[] temp = await ReadFile();
            return temp;
        }

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
