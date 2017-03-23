using System.Collections.Generic;
using DiscordArchiver.data;

namespace DiscordArchiver
{
    public class Archive
    {
        private static Archive _archive;

        public static Archive GetInstance() => _archive ?? (_archive = new Archive());

        public List<DMessageObject> MessageArchive = new List<DMessageObject>(); 
    }
}
