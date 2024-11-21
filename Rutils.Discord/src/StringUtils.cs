namespace Rutils.Discord;

using System.Text.RegularExpressions;
using global::Discord;

public static class StringUtils
{
    public static async Task<List<IUser>> ParseMentionsToUsersAsync(string mentions, IDiscordClient discordClient)
    {
        List<IUser> parsedUsers = new();
        
        var mentionPattern = @"<@!?(\d+)>"; // Regex pattern to extract user IDs from mentions

        foreach (Match match in Regex.Matches(mentions, mentionPattern))
        {
            if (ulong.TryParse(match.Groups[1].Value, out ulong userId))
            {
                var user = await discordClient.GetUserAsync(userId);
                if (user != null)
                {
                    parsedUsers.Add(user);
                }
            }
        }

        return parsedUsers;
    }
}