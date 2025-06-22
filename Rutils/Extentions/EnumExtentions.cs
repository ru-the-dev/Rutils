using System.ComponentModel;
using System.Reflection;

namespace Rutils.Extentions;


public static class EnumExtentions
{
    public static IEnumerable<Enum> GetFlags(this Enum e)
    {
        return Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);
    }


    public static string GetDescription(this Enum e)
    {
        //Tries to find a DescriptionAttribute for a potential friendly name
        //for the enum
        MemberInfo[] memberInfo = e.GetType().GetMember(e.ToString());
        if (memberInfo != null && memberInfo.Length > 0)
        {
            object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
                //Pull out the description value
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        //If we have no description attribute, just return the ToString of the enum
        return e.ToString();
    }

    public static T FromDescription<T>(string description) where T : Enum
    {
        Type type = typeof(T);
        foreach (var field in type.GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == description)
                    return (T)field.GetValue(null)!; // we can assert since Enum Values are static
            }
            else
            {
                if (field.Name == description)
                    return (T)field.GetValue(null)!; // we can assert since Enum Values are static
            }
        }
        throw new ArgumentException($"No enum with description {description} found.", nameof(description));
    }
}