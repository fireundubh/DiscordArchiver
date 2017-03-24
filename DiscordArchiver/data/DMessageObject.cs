using System;
// ReSharper disable InconsistentNaming

namespace DiscordArchiver.data {
    public class DMessageObject {
        public ulong id;
        public ulong channel_id;
        public DUserObject author;
        public string content;
        public DateTime timestamp;
        public DateTime? edited_timestamp = null;
        public bool tts;
        public bool mention_everyone;
        public DUserObject[] mentions;
        public ulong[] mention_roles;
        public DAttachmentObject[] attachments;
        public DEmbedObject[] embeds;
        public DReactionObject[] reactions;
        public ulong? nonce;
        public bool pinned;
        public string webhook_id;
    }
}
