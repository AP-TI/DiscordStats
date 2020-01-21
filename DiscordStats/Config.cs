using System;
namespace DiscordStats
{
    public class Config
    {
        public string ConnectionString { get; set; }
        public string ServerID { get; set; }
        public Database Database { get; set; }
        public string DatabaseNaam { get; set; }
        public Config(string connectionString, string serverID, Database database, string databaseNaam)
        {
            ConnectionString = connectionString;
            ServerID = serverID;
            Database = database;
            DatabaseNaam = databaseNaam;
        }
    }
}
