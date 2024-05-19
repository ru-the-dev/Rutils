using Discord;

namespace Rutils.Discord;

public static class CustomEmojis
{
    //Global == 
    public static string RedCross = "❌";
    public static string GreenCheck = "✅";
    public static string Info = "ℹ️";
    public static string Warning = "⚠️";
    

    //Server1 = https://discord.gg/AZmkqhXJ6K
    public static Emote LoadingDots{ get => _loadingDots; }
    private static Emote _loadingDots = Emote.Parse("<a:loadingdots:1208002028442361926>");
}
