using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordStats
{
    class MySQLServer : DatabaseServer
    {
        public void UpdateData(Config config, int aantalOnline)
        {
            MySqlConnection conn = new MySqlConnection(config.ConnectionString);
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
