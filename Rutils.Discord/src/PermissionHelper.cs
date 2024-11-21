using Discord;
using Discord.WebSocket;

public static class PermissionHelper
{
    public static async Task<bool> IsUserHierarchyHigherOrEqualToRoleAsync(ISocketMessageChannel channel, ulong userId, ulong adminRoleId)
    {
        SocketGuild? guild = (channel as SocketGuildChannel)?.Guild;
        return await IsUserHierarchyHigherOrEqualToRoleAsync(guild, userId, adminRoleId);
    }

    public static async Task<bool> IsUserHierarchyHigherOrEqualToRoleAsync(IGuild? guild, ulong userId, ulong adminRoleId)
    {
        if (guild == null)
        {
            return false;
        }

        //get guild user
        IGuildUser? guildUser = await guild.GetUserAsync(userId);

        if (guildUser == null)
        {
            return false;
        }
        
        //get guild admin role
        IRole? adminRole = guild.GetRole(adminRoleId);

        if (adminRole == null)
        {
            return false;
        }

        //if user has a role that's higher or equal in hierarchy to the admin role
        return guildUser.Hierarchy >= adminRole.Position;
    }
}