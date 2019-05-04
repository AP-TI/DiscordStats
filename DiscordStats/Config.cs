using System;
namespace DiscordStats
{
    public class Config
    {
        public string ConnectionString { get; set; }
        public string ServerID { get; set; }
        public int AantalBots { get; set; }
        public Config(string connectionString, string serverID, int aantalBots)
        {
            ConnectionString = connectionString;
            ServerID = serverID;
            AantalBots = aantalBots;
        }
    }
}
