using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DiscordStats
{
    class MongoServer : DatabaseServer
    {
        public Config Config { get; set; }
        public MongoServer(Config config)
        {
            Config = config;
        }
        public void UpdateData(int aantalOnline)
        {
            var client = new MongoClient(Config.ConnectionString);
            var database = client.GetDatabase(Config.DatabaseNaam);
            var collection = database.GetCollection<BsonDocument>("aantalOnline");
            var document = new BsonDocument
            {
                { "aantal", aantalOnline }
            };
            collection.InsertOne(document);
        }
    }
}
