using CommandLine;

namespace DiscordArchiver
{
    internal class Options
    {
        [Option('c', "channel", Required = true, HelpText = "Get messages from this channel ID")]
        public string Channel { get; set; }

        [Option('t', "token", Required = true, HelpText = "Use this user token to authenticate with Discord API")]
        public string Token { get; set; }

        [Option('o', "output", Required = false, HelpText = "Write messages to this file")]
        public string Out { get; set; }

        [Option('r', "around", Required = false, HelpText = "Get messages around this message ID")]
        public string Around { get; set; }

        [Option('b', "before", Required = false, HelpText = "Get messages before this message ID")]
        public string Before { get; set; }

        [Option('a', "after", Required = false, HelpText = "Get messages after this message ID")]
        public string After { get; set; }

        [Option('l', "limit", Required = false, DefaultValue = 100, HelpText = "Max number of messages to return (1-100)")]
        public int Limit { get; set; }

        [Option('d', "debug", Required = false, DefaultValue = false, HelpText = "Toggle debug output")]
        public bool Debug { get; set; }
    }
}
