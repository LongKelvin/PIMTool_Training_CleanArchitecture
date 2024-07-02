using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProjectManagement.Common.Extensions
{
    public static class EnumExtension
    {
        public static string Name(this Enum @enum)
        {
            if (@enum == null)
            {
                return string.Empty;
            }

            return @enum
                .GetType()
                .GetMember(@enum.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName() ?? @enum.ToString();
        }

        public static string Description(this Enum @enum)
        {
            if (@enum == null)
            {
                return string.Empty;
            }

            var fieldInfo = @enum.GetType().GetField(@enum.ToString());
            if (fieldInfo == null)
            {
                return string.Empty;
            }

            var attributes = (DescriptionAttribute[])fieldInfo
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : @enum.ToString();
        }
    }
}