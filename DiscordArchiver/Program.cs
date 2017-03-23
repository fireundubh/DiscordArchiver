using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using DiscordArchiver.data;
using Newtonsoft.Json.Linq;

namespace DiscordArchiver
{
    internal static class Program
    {
        public static string BaseUrl, Channel, Token, Out, MessageId, ActiveKey;

        public static bool Debug;

        public static int Limit;

        private static void Main(string[] args)
        {
            ArgParse.Process(args);

            // ReSharper disable once CollectionNeverUpdated.Local
            List<DMessageObject> joinedMessages = new List<DMessageObject>();

            int counter = 0;
            bool exit = false;
            BackgroundWorker bw = new BackgroundWorker();

            bw.DoWork += (sender, eventArgs) =>
            {
                while (true) {
                    counter++;

                    string sourceJson = JsonHelper.ReadURI(string.Format(BaseUrl, Channel, Token, MessageId, Limit));                    

                    JArray arrayOfJsonTokens = JArray.Parse(sourceJson);

                    // discord api produces json output from newest to oldest
                    string newestMessageId = arrayOfJsonTokens[0]["id"].ToString();
                    string oldestMessageId = arrayOfJsonTokens[arrayOfJsonTokens.Count - 1]["id"].ToString();

                    // this needs to be set so the background worker can continually retrieve new uris
                    // after retrieving the user-specified uri
                    MessageId = newestMessageId;

                    // reverse order for retrieving before uris - untested
                    if (ActiveKey == "before")
                    {
                        MessageId = oldestMessageId;
                    } 

                    Log.Info($"Processing message range ({arrayOfJsonTokens.Count} items): {oldestMessageId} to {newestMessageId}");

                    int jsonTokenIterations = -1;
                    bool reachedIterationLimit = false;

                    foreach (JToken jsonToken in arrayOfJsonTokens)
                    {
                        jsonTokenIterations++;

                        string jsonTokenId = (string) jsonToken["id"];

                        // this is probably not right, but it works!
                        if (jsonTokenId.Any())
                        {
                            continue;
                        }

                        reachedIterationLimit = true;
                        break;
                    }

                    if (reachedIterationLimit)
                    {
                        exit = true;

                        for (int i = jsonTokenIterations; i < arrayOfJsonTokens.Count - 1; i++)
                        {
                            arrayOfJsonTokens.RemoveAt(i);
                        }
                    }

                    JsonHelper.Update(sourceJson, joinedMessages);

                    if (counter % 5 == 0) {
                        JsonHelper.WriteJSON(joinedMessages, Out, "Writing partial log to file");
                    }

                    Thread.Sleep(500);

                    if (arrayOfJsonTokens.Count < Limit)
                    {
                        Log.Error($"Number of tokens was less than limit, exiting...");
                        break;
                    }

                    if (!exit) continue;

                    Log.Error($"Forced exit was {exit}, exiting...");
                    break;
                }
            };

            bw.RunWorkerAsync();

            bw.RunWorkerCompleted += (sender, eventArgs) =>
            {
                JsonHelper.WriteJSON(joinedMessages, Out, "Writing log to file");
                Log.Info("All done! Press any key to exit...");
            };

            Console.ReadKey();
        }
    }
}