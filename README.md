# DiscordArchiver Fork

DiscordArchiver continuously downloads Discord channel messages to a JSON file.

## Usage

Short | Long | Required | Default | Help
--- | --- | --- | --- | ---
c | channel | true | | Get messages from this channel ID
t | token | true | | Use this user token to authenticate with Discord API
o | output | false | `{channel_id} - {activekey} {message_id}.json` | Write messages to this file
r | around | false | | Get messages around this message ID
b | before | false | | Get messages before this message ID
a | after | false | | Get messages after this message ID
l | limit | false | 100 | Max number of messages to return (1-100)
d | debug | false | false | Toggle debug output

### Example

`DiscordArchiver.exe -c <channel_id> -t <user_token> -a <message_id>`
