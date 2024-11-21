using Discord;
using Discord.Interactions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rutils.Discord;

public class RequireRoleOrHigherInHierarchyAttribute : PreconditionAttribute
{
    private ulong? _roleId = null;
    private readonly string? _roleIdConfigPath = null;

    private readonly string _errorMsg = $"{Rutils.Discord.CustomEmojis.RedCross} You do not have the required role to run this command.";
    
    public RequireRoleOrHigherInHierarchyAttribute(ulong roleId, string? errorMessage = null)
    {
        _roleId = roleId;
        if (errorMessage != null)
        {
            _errorMsg = errorMessage;
        }
    }

    public RequireRoleOrHigherInHierarchyAttribute(string roleIdConfigPath, string? errorMessage = null)
    {
        _roleIdConfigPath = roleIdConfigPath;
        if (errorMessage != null)
        {
            _errorMsg = errorMessage;
        }
    }

    public override async Task<PreconditionResult> CheckRequirementsAsync(IInteractionContext context, ICommandInfo commandInfo, IServiceProvider services)
    {
        if (_roleId == null)
        {
            //no role id, check if we can get it from the config.
            if (_roleIdConfigPath == null)
            {
                throw new Exception("No roleId, or config path for roleId was provided.");
            }

            //asign roleId from config.
            var config = services.GetRequiredService<IConfiguration>();
            _roleId = config.GetRequiredSection(_roleIdConfigPath).Get<ulong>();
        }
        

        // If the user has the role, return success; otherwise, return an error
        return (await PermissionHelper.IsUserHierarchyHigherOrEqualToRoleAsync(context.Guild, context.User.Id, _roleId.Value))
            ? PreconditionResult.FromSuccess()
            : PreconditionResult.FromError(_errorMsg);
    }
}
