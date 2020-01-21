using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace DiscordStats
{
    class MongoServer : DatabaseServer
    {
        public Config Config { get; set; }
        public MongoServer(Config config)
        {
            Config = config;
        }
        public void UpdateData(int aantalOnline, JArray jArray)
        {
            var client = new MongoClient(Config.ConnectionString);
            var database = client.GetDatabase(Config.DatabaseNaam);
            var collection = database.GetCollection<BsonDocument>("aantalOnline");
            List<dynamic> list = new List<dynamic>();
            foreach (dynamic item in jArray)
            {
                string naam = "";
                naam = item.username;
                string status = item.status;
                list.Add(new BsonDocument
            {
                { "naam", naam },
                    {"status",  status}
            });
            }
            var document = new BsonDocument
            {
                { "aantal", aantalOnline },
                {   "personen", new BsonArray(list) }
            };
            collection.InsertOne(document);
        }
    }
}
