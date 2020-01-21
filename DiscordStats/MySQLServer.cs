using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
namespace DiscordStats
{
    class MySQLServer : DatabaseServer
    {
        public Config Config { get; set; }
        public MySQLServer(Config config)
        {
            Config = config;
        }
        public void UpdateData(int aantalOnline, JArray jArray)
        {
            MySqlConnection conn = new MySqlConnection(Config.ConnectionString);
            conn.Open();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO AantalOnline(Aantal, Datum) VALUES(?aantal,?datum)";
                cmd.Parameters.Add("?aantal", MySqlDbType.Int32).Value = aantalOnline;
                cmd.Parameters.Add("?datum", MySqlDbType.DateTime).Value = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }
    }
}
