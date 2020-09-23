using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Assignment_4
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> _playerCollection;
        private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("game");
            _playerCollection = database.GetCollection<Player>("players");

            _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
        }

        public async Task<Player> Create(Player player)
        {
            await _playerCollection.InsertOneAsync(player);
            return player;
        }

        public async Task<Player[]> GetAll()
        {
            var players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
            return players.ToArray();
        }

        public Task<Player> Get(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq(player => player.Id, id);
            return _playerCollection.Find(filter).FirstAsync();
        }

        public async Task<Player[]> GetBetweenLevelsAsync(int minLevel, int maxLevel)
        {
            var filter = Builders<Player>.Filter.Gte(p => p.Level, 18) & Builders<Player>.Filter.Lte(p => p.Level, 30);
            var players = await _playerCollection.Find(filter).ToListAsync();
            return players.ToArray();
        }


        public Task<Player> IncreasePlayerScoreAndRemoveItem(Guid playerId, Guid itemId, int score)
        {
            var pull = Builders<Player>.Update.PullFilter(p => p.Items, i => i.Id == itemId);
            var inc = Builders<Player>.Update.Inc(p => p.Score, score);
            var update = Builders<Player>.Update.Combine(pull, inc);
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);

            return _playerCollection.FindOneAndUpdateAsync(filter, update);
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
            await _playerCollection.ReplaceOneAsync(filter, player);
            return player;
        }

        public async Task<Player[]> GetAllSortedByScoreDescending()
        {
            var sortDef = Builders<Player>.Sort.Descending(p => p.Score);
            var players = await _playerCollection.Find(new BsonDocument()).Sort(sortDef).ToListAsync();

            return players.ToArray();
        }

        public async Task<Player> IncrementPlayerScore(string id, int increment)
        {
            var filter = Builders<Player>.Filter.Eq("_id", id);
            var incrementScoreUpdate = Builders<Player>.Update.Inc(p => p.Score, increment);
            var options = new FindOneAndUpdateOptions<Player>()
            {
                ReturnDocument = ReturnDocument.After
            };
            Player player = await _playerCollection.FindOneAndUpdateAsync(filter, incrementScoreUpdate, options);
            return player;
        }

        public async Task<Player> Delete(Guid playerId)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            return await _playerCollection.FindOneAndDeleteAsync(filter);
        }

        public async Task<LevelCount[]> GetLevelCounts()
        {
            List<LevelCount> levelCounts =
                await _playerCollection.Aggregate()
                    .Project(p => p.Level)
                    .Group(l => l, p => new LevelCount { Id = p.Key, Count = p.Sum() })
                    .SortByDescending(l => l.Count)
                    .Limit(3)
                    .ToListAsync();

            return levelCounts.ToArray();
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> CreateItem(Guid playerId, Item item)
        {
            Player tempPlayer = await Get(playerId);

            tempPlayer.Items.Add(item);

            //await UpdatePlayerItems(playerId, tempPlayer);

            await UpdatePlayer(tempPlayer);

            return item;
        }

        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            Player tempPlayer = await Get(playerId);

            Item found = tempPlayer.Items.Find(x => x.Id == itemId);

            return found;
        }

        public async Task<Item[]> GetAllItems(Guid playerId)
        {
            Player tempPlayer = await Get(playerId);

            return tempPlayer.Items.ToArray();
        }

        public async Task<Item> UpdateItem(Guid playerId, Item item)
        {
            Player tempPlayer = await Get(playerId);

            tempPlayer.Items.Where(x => x.Id == item.Id).FirstOrDefault().Level = item.Level;

            await UpdatePlayer(tempPlayer);

            return item;
        }

        public async Task<Item> DeleteItem(Guid playerId, Item item)
        {
            Player tempPlayer = await Get(playerId);

            var found = tempPlayer.Items.Where(x => x.Id == item.Id).FirstOrDefault();

            tempPlayer.Items.Remove(found);

            await UpdatePlayer(tempPlayer);

            return item;
        }

        
    }
}

public class LevelCount
{
    public int Id { get; set; }
    public int Count { get; set; }
}