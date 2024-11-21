using Discord;

namespace Rutils.Discord;

public static class CustomEmojis
{
    //Global == 
    public static string RedCross = "❌";
    public static string GreenCheck = "✅";
    public static string Info = "ℹ️";
    public static string Warning = "⚠️";
    public static string RobotFace = "🤖";
    public static string PaperScroll = "📜";
    public static string HammerAndWrench = "🛠️";
    public static string MoneyBag = "💰";
    public static string Coin = "🪙";
    public static string[] NumberEmojis = 
    [
        "0️⃣",
        "1️⃣",
        "2️⃣",
        "3️⃣",
        "4️⃣",
        "5️⃣",
        "6️⃣",
        "7️⃣",
        "8️⃣",
        "9️⃣",
        "🔟"
    ];
    public static string ArrowLeft = "⬅️";
    public static string ArrowRight = "➡️";

    

    //Server1 = https://discord.gg/AZmkqhXJ6K
    public static Emote LoadingDots{ get => _loadingDots;  }
    private static Emote _loadingDots = Emote.Parse("<a:loadingdots:1208002028442361926>");


    public static Emote RepeatableQuest{ get => _repeatableQuest; }
    private static Emote _repeatableQuest = Emote.Parse("<:RepeatableQuest:1270047207323729991>");


    public static Emote LegendaryQuest{ get => _legendaryQuest; }
    private static Emote _legendaryQuest = Emote.Parse("<:OneTimeQuest:1270046773808861216>");


    public static Emote ExperiencePoints{ get => _experiencePoints; }
    private static Emote _experiencePoints = Emote.Parse("<:ExperiencePoints:1270369000794361978>");


    public static Emote Level{ get => _level; }
    private static Emote _level = Emote.Parse("<:Level:1270387437004259369>");

    //== exp bars ==
    public static Emote ExpBarLeftEmpty { get => _expBarLeftEmpty; }
    private static Emote _expBarLeftEmpty = Emote.Parse("<:ExpBar_Left_Empty:1271445454223179777>");

    public static Emote ExpBarLeftFilled { get => _expBarLeftFilled; }
    private static Emote _expBarLeftFilled = Emote.Parse("<:ExpBar_Left_Filled:1271445455720546368>");

    public static Emote ExpBarMiddleEmpty { get => _expBarMiddleEmpty; }
    private static Emote _expBarMiddleEmpty = Emote.Parse("<:ExpBar_Middle_Empty:1271445457532358767>");

    public static Emote ExpBarMiddleFilled { get => _expBarMiddleFilled; }
    private static Emote _expBarMiddleFilled = Emote.Parse("<:ExpBar_Middle_Filled:1271445458883055616>");

    public static Emote ExpBarRightEmpty { get => _expBarRightEmpty; }
    private static Emote _expBarRightEmpty = Emote.Parse("<:ExpBar_Right_Empty:1271445459994546208>");
    
    public static Emote ExpBarRightFilled { get => _expBarRightFilled; }
    private static Emote _expBarRightFilled = Emote.Parse("<:ExpBar_Right_Filled:1271445461114552422>");
    
    public static Emote InfinitelyRepeatingQuest { get => _infinitelyRepeatingQuest; }
    private static Emote _infinitelyRepeatingQuest = Emote.Parse("<:InfinitelyRepeatingQuest:1281315575586357449>");


    public static string CreateExpBarString(int currentExpInLevel, int maxExpToLevel, int barSegments = 8)
    {
        string expBarString = "";
        float expPerSegment = maxExpToLevel / barSegments;

        for (int segmentNr = 1; segmentNr <= barSegments; ++segmentNr)
        {
            bool isFilled = currentExpInLevel >= segmentNr * expPerSegment;
            if (segmentNr == 1)
            {
                //left bar
                expBarString += isFilled ? ExpBarLeftFilled : ExpBarLeftEmpty;
            }
            else if (segmentNr == barSegments)
            {
                //right bar
                expBarString += isFilled ? ExpBarRightFilled : ExpBarRightEmpty;
            }
            else
            {
                //middle bar
                expBarString += isFilled ? ExpBarMiddleFilled : ExpBarMiddleEmpty;
            }
        }

        return expBarString;
    }
}