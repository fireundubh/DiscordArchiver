namespace DiscordArchiver
{
    public class ArgParse
    {
        public static void Process(string[] asArguments)
        {
            Options options = new Options();
            bool isValid = CommandLine.Parser.Default.ParseArgumentsStrict(asArguments, options);

            if (!isValid)
            {
                Log.Error("Invalid arguments");
                return;
            }

            // get messages from this channel ID
            Program.Channel = options.Channel;

            // use this user token to authenticate with Discord API
            Program.Token = options.Token;

            if (string.IsNullOrEmpty(options.Around) && string.IsNullOrEmpty(options.Before) && string.IsNullOrEmpty(options.After)) return;

            // get messages before this message ID
            if (!string.IsNullOrEmpty(options.Around))
            {
                Program.ActiveKey = "around";
                Program.MessageId = options.Around;
                Program.BaseUrl = "https://discordapp.com/api/channels/{0}/messages?token={1}&around={2}&limit={3}";
            }

            // get messages before this message ID
            if (!string.IsNullOrEmpty(options.Before))
            {
                Program.ActiveKey = "before";
                Program.MessageId = options.Before;
                Program.BaseUrl = "https://discordapp.com/api/channels/{0}/messages?token={1}&before={2}&limit={3}";
            }

            // get messages after this message ID
            if (!string.IsNullOrEmpty(options.After))
            {
                Program.ActiveKey = "after";
                Program.MessageId = options.After;
                Program.BaseUrl = "https://discordapp.com/api/channels/{0}/messages?token={1}&after={2}&limit={3}";
            }

            // max number of messages to return (1-100)
            if (options.Limit > 0)
            {
                Program.Limit = options.Limit;
            }

            // output to this file
            if (!string.IsNullOrEmpty(options.Out))
            {
                Program.Out = options.Out;
            }
            else if (!string.IsNullOrEmpty(Program.ActiveKey))
            {
                Program.Out = $"{Program.Channel} - {Program.ActiveKey} {Program.MessageId}.json";
            }

            Program.Debug = options.Debug;
        }

    }
}
