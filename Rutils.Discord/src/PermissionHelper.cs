using Discord.WebSocket;

public static class PermissionHelper
{
    public static bool IsUserHigherHierarchyThanRole(ISocketMessageChannel channel, ulong userId, ulong adminRoleId)
    {
        SocketGuild? guild = (channel as SocketGuildChannel)?.Guild;
        return IsUserHigherHierarchyThanRole(guild, userId, adminRoleId);
    }

    public static bool IsUserHigherHierarchyThanRole(SocketGuild? guild, ulong userId, ulong adminRoleId)
    {
        if (guild == null)
        {
            return false;
        }

        //get guild user
        SocketGuildUser? guildUser = guild.GetUser(userId);

        if (guildUser == null)
        {
            return false;
        }
        
        //get guild admin role
        SocketRole? adminRole = guild.GetRole(adminRoleId);

        if (adminRole == null)
        {
            return false;
        }

        //if user has a role that's higher or equal in hierarchy to the admin role
        return guildUser.Hierarchy >= adminRole.Position;
    }
}