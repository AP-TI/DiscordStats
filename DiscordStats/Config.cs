using System;
namespace DiscordStats
{
    public class Config
    {
        public string ConnectionString { get; set; }
        public string ServerID { get; set; }
        public Config(string connectionString, string serverID)
        {
            ConnectionString = connectionString;
            ServerID = serverID;
        }
    }
}
