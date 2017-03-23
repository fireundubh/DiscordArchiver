// ReSharper disable InconsistentNaming

using System;

namespace DiscordArchiver.data {

    public class DEmbedObject {
        public string title;
        public string type;
        public string description;
        public string url;
        public DateTime timestamp;
        public uint color;
        public DEmbedFooterObject footer;
        public DEmbedImageObject image;
        public DEmbedThumbnailObject thumbnail;
        public DEmbedVideoObject video;
        public DEmbedProviderObject provider;
        public DEmbedAuthorObject author;
        public DEmbedFieldObject[] fields;
    }
}