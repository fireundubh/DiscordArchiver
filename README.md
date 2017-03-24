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


## Required Data

### How to find the channel ID

With the Discord app running:

1. Open User Settings.
2. Navigate to the Appearance tab.
3. Check the Developer Mode box.
4. Click the Done button.
5. Right-click on a channel to open the context menu.
6. Copy the channel ID by clicking Copy ID on the context menu.

### How to find your user token

With the Discord app running in your browser:

1. In Chrome, press `Ctrl+Shift+I` to open the Developer Tools pane.
2. Navigate to the Application tab.
3. Copy the value of the `token` key.
