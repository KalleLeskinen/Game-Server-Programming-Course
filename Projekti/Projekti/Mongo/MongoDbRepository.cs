using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Projekti
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Racer> _playerCollection;
        private readonly IMongoCollection<RaceTrack> _trackCollection;
        private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("projekti");
            _playerCollection = database.GetCollection<Racer>("racers");
            _trackCollection = database.GetCollection<RaceTrack>("tracks");
            _bsonDocumentCollection = database.GetCollection<BsonDocument>("racers");

        }

        public async Task<Racer> Create(Racer player)
        {
            await _playerCollection.InsertOneAsync(player);
            return player;
        }

        public async Task<Racer[]> GetAll()
        {
            var players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
            return players.ToArray();
        }

        public Task<Racer> Get(Guid id)
        {
            var filter = Builders<Racer>.Filter.Eq(player => player.Id, id);
            return _playerCollection.Find(filter).FirstAsync();
        }



        public async Task<Racer> UpdateRacer(Racer player)
        {
            FilterDefinition<Racer> filter = Builders<Racer>.Filter.Eq(p => p.Id, player.Id);
            await _playerCollection.ReplaceOneAsync(filter, player);
            return player;
        }



        public async Task<Racer> Delete(Guid playerId)
        {
            FilterDefinition<Racer> filter = Builders<Racer>.Filter.Eq(p => p.Id, playerId);
            return await _playerCollection.FindOneAndDeleteAsync(filter);
        }

        public Task<Racer> Modify(Guid id, ModifiedRacer player)
        {
            throw new NotImplementedException();
        }

        public async Task<LapTime> SetLapTime(Guid playerId, LapTime item)
        {
            Racer tempRacer = await Get(playerId);


            //Tarkistetaan onko pelaajan nopeus mahdollinen
            if(item.Time <= item.Track.FastestPossible)
            {
                throw new TooFastException("Too Fast! Your time was " + item.Time + " and " + item.Track.FastestPossible + " is the fastest possible time on this track.");
            } else
            {
                tempRacer.LapTimes.Add(item);
                await UpdateRacer(tempRacer);
            }

            return item;
        }

        public async Task<LapTime> GetLapTime(Guid playerId, Guid itemId)
        {
            Racer tempRacer = await Get(playerId);

            LapTime found = tempRacer.LapTimes.Find(x => x.Id == itemId);

            return found;
        }

        public async Task<LapTime[]> GetAllLapTimes(Guid playerId)
        {
            Racer tempRacer = await Get(playerId);

            return tempRacer.LapTimes.ToArray();
        }

        public async Task<LapTime> DeleteLapTime(Guid playerId, Guid lapId)
        {
            Racer tempRacer = await Get(playerId);

            var found = tempRacer.LapTimes.Where(x => x.Id == lapId).FirstOrDefault();

            tempRacer.LapTimes.Remove(found);

            await UpdateRacer(tempRacer);

            return found;
        }


        public async Task<RaceTrack> CreateTrack(RaceTrack raceTrack)
        {
            await _trackCollection.InsertOneAsync(raceTrack);
            return raceTrack;
        }

        public async Task<RaceTrack[]> GetAllTracks()
        {
            var tracks = await _trackCollection.Find(new BsonDocument()).ToListAsync();
            return tracks.ToArray();
        }

        public async Task<RaceTrack> DeleteTrack(Guid id)
        {
            FilterDefinition<RaceTrack> filter = Builders<RaceTrack>.Filter.Eq(RaceTrack => RaceTrack.Id, id);
            return await _trackCollection.FindOneAndDeleteAsync(filter);
        }

        public async Task<RaceTrack> UpdateTrack(Guid id, RaceTrack RaceTrack)
        {
            var filter = Builders<RaceTrack>.Filter.Eq(RaceTrack => RaceTrack.Id, id);

            var tempTrack = await _trackCollection.Find(filter).FirstAsync();

            tempTrack.Name = RaceTrack.Name;
            tempTrack.FastestPossible = RaceTrack.FastestPossible;

            await _trackCollection.ReplaceOneAsync(filter, tempTrack);
            return tempTrack;

        }

        public async Task<RaceTrack> GetTrack(Guid id)
        {
            var filter = Builders<RaceTrack>.Filter.Eq(RaceTrack => RaceTrack.Id, id);
            return await _trackCollection.Find(filter).FirstAsync();
        }
    }

}
