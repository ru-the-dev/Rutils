using Discord;

namespace RuDiscordUtils;

public static class CustomEmojis
{
    //Global == 
    public static string RedCross = "❌";

    //Server1 ====
    public static Emote LoadingDots{ get => _loadingDots; }
    private static Emote _loadingDots = Emote.Parse("<a:loadingdots:1208002028442361926>");
}
