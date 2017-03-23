using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using DiscordArchiver.data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordArchiver {
        public static string BaseUrl, Channel, Token, Out, MessageId, ActiveKey;

        public static bool Debug;

        public static int Limit;

        private static void Main(string[] args)
        {
            ArgParse.Process(args);




            var fullLogs = new List<DMessage>();

            var counter = 0;
            var exit = false;
            var bw = new BackgroundWorker();

            bw.DoWork += (sender, eventArgs) => {
                while (true) {
                    counter++;
                    Console.WriteLine($"Downloading Logs Part {counter}");

                    var currentLog = "";
                    using (var wc = new WebClient()) {
                        currentLog = wc.DownloadString(string.Format(BaseUrl, Channel, Token, Before, Limit));
                    }

                    Console.WriteLine($"Downloaded Log Part {counter}, Parsing");

                    var jar = JArray.Parse(currentLog);

                    Before = jar[jar.Count - 1]["id"].ToString();
                    Console.WriteLine($"Before: {Before}");
                    var g = -1;
                    var mg = -1;
                    foreach (var jToken in jar) {
                        g++;
                        if ((string)jToken["id"] != After) continue;
                        mg = g;
                        break;
                    }

                    if (mg > -1) {
                        exit = true;
                        for (var i = mg; i < jar.Count - 1; i++) {
                            jar.RemoveAt(i);
                        }
                    }

                    fullLogs.InsertRange(0, JArray.Parse(currentLog).Select(jToken => jToken.ToObject<DMessage>()).Reverse().ToList());

                    if (counter % 50 == 0) {
                        Console.WriteLine($"Writing partial logs to file {Out}");
                        File.WriteAllText(Out, JsonConvert.SerializeObject(fullLogs, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto }));
                    }

                    Thread.Sleep(500);

                    if (jar.Count < Limit || exit) break;
                }
            };

            bw.RunWorkerAsync();

            bw.RunWorkerCompleted += (sender, eventArgs) => {
                Console.WriteLine($"Writing logs to file {Out}");
                File.WriteAllText(Out, JsonConvert.SerializeObject(fullLogs, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto }));
                Console.WriteLine("All done, press any key to exit...");
            };

            Console.ReadKey();
        }
    }
}