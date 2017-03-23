// ReSharper disable InconsistentNaming

namespace DiscordArchiver.data {

    public class DUserObject {
        public ulong id;
        public string username;
        public ushort discriminator;
        public string avatar;
        public bool bot;
        public bool mfa_enabled;
        public bool verified;
        public string email;
    }
}