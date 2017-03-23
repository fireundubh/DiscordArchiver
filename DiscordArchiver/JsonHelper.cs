using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using DiscordArchiver.data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordArchiver
{
    public static class JsonHelper
    {
        private static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> akItems, Func<T, TKey> akProperty)
        {
            return akItems.GroupBy(akProperty).Select(x => x.First());
        }

        private static string Serialize(IEnumerable<DMessageObject> akMessages)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                Converters = { new DecimalJsonConverter() }
            };

            IOrderedEnumerable<DMessageObject> orderedLogs = akMessages
                .DistinctBy(x => x.id)
                .Reverse()
                .ToList()
                .OrderBy(x => x.timestamp);

            return JsonConvert.SerializeObject(orderedLogs, settings);
        }

        public static string ReadURI(string asUriAddress)
        {
            using (WebClient wc = new WebClient())
            {
                return wc.DownloadString(asUriAddress);
            }
        }

        public static void WriteJSON(IEnumerable<DMessageObject> akMessages, string asOutputFile, string asLogMessage)
        {
            Log.Info($"{asLogMessage} {asOutputFile}");

            string serializedJson = string.Empty;

            try
            {
                serializedJson = Serialize(akMessages);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
            }

            File.WriteAllText(asOutputFile, serializedJson);
        }

        public static void Update(string asSourceJson, List<DMessageObject> akJoinedMessages)
        {
            List<DMessageObject> rangeToInsert = new List<DMessageObject>();

            try
            {
                rangeToInsert = JArray.Parse(asSourceJson)
                    .OrderBy(x => x["id"])
                    .Select(x => x.ToObject<DMessageObject>())
                    .ToList();
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                Environment.Exit(1);
            }

            akJoinedMessages.InsertRange(0, rangeToInsert);
        }
    }
}
