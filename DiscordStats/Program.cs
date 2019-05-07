using System;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;

namespace DiscordStats
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Config config;
            try
            {
                config = JsonConvert.DeserializeObject<Config>(System.IO.File.ReadAllText(@".discordstatsconfig.json"));
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.Write("Geef database server IP (meestal localhost): ");
                string serverIP = Console.ReadLine();
                Console.Write("Geef database naam: ");
                string databaseNaam = Console.ReadLine();
                Console.Write("Geef database Uid (meestal root): ");
                string uid = Console.ReadLine();
                Console.Write("Geef datbase wachtwoord: ");
                string wachtwoord = WachtwoordInvoer();
                Console.WriteLine();
                Console.Write("Geef discord server ID: ");
                string discordServerID = Console.ReadLine();
                config = new Config($"Server={serverIP};Database={databaseNaam};Uid={uid};Pwd={wachtwoord};", discordServerID);
                string configString = JsonConvert.SerializeObject(config);
                using (StreamWriter streamWriter = File.CreateText(@".discordstatsconfig.json"))
                {
                    streamWriter.WriteLine(configString);
                }
            }
            TelOnline telOnline = new TelOnline(config);


        }

        private static string WachtwoordInvoer()
        {
            StringBuilder invoer = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo toets = Console.ReadKey(true);
                if (toets.Key == ConsoleKey.Enter)
                    break;
                if (toets.Key == ConsoleKey.Backspace && invoer.Length > 0)
                    invoer.Remove(invoer.Length - 1, 1);
                else if (toets.Key != ConsoleKey.Backspace)
                    invoer.Append(toets.KeyChar);
            }
            return invoer.ToString();
        }

    }
}