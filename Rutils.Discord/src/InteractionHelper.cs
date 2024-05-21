using System.ComponentModel;
using Discord;

namespace Rutils.Discord;

public static class InteractionHelper
{
    public static T? GetComponentById<T>(IReadOnlyCollection<ActionRowComponent> components, string customId) where T : class, IMessageComponent
    {
        //loop through actionrows
        foreach (ActionRowComponent row in components)
        {
            foreach (IMessageComponent component in row.Components)
            {
                if (component.CustomId == customId)
                {
                    return component as T;
                }
            }
        }

        return null;
    }
}