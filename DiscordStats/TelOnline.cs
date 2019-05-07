using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Common;
using Microsoft.CSharp.RuntimeBinder;

namespace DiscordStats
{
    public class TelOnline
    {
        public Config Config { get; set; }
        private int aantalBots = 0;
        public TelOnline(Config config)
        {
            Config = config;
            Task t = Task.Run(async () => {
                do
                {
                    GeefAantal();
                    await Task.Delay(60000);
                } while (true);
            });
            try
            {
                t.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
            


        public void GeefAantal()
        {
            string result;

            using (WebClient client = new WebClient())
            {
                client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                try
                {
                    result = client.DownloadString("https://discordapp.com/api/servers/" + Config.ServerID + "/widget.json?date="+DateTime.Now);
                    dynamic resultJson = JsonConvert.DeserializeObject(result);
                    Newtonsoft.Json.Linq.JArray jArray = (Newtonsoft.Json.Linq.JArray)resultJson.members;
                    foreach(dynamic item in jArray)
                    {
                        try
                        {
                            if (item.bot == "true")
                            {
                                aantalBots++;
                            }

                        }
                        catch(RuntimeBinderException)
                        {
                            Console.WriteLine(item.username + "geen bot");
                        }
                    }
                    int aantalOnline = ((Newtonsoft.Json.Linq.JArray)resultJson.members).Count - aantalBots;
                    aantalBots = 0;
                    UpdateData(aantalOnline);
                    Console.WriteLine($"{aantalOnline} - {DateTime.Now}");
                }
                catch(WebException wex)
                {
                    Console.WriteLine(wex.Message);
                }
            }
        }

        private void UpdateData(int aantalOnline)
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
