using System.ComponentModel;

namespace HelperLibrary
{
    public class EnumerationHelperExtension
    {
        public static T GetValueFromDescription<T>(string description) where T : struct
        {
            var type = typeof(T);

            if (!type.IsEnum)
            {
                throw new ArgumentException("T must be an enum");
            }

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute != null)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentOutOfRangeException("description");
            // or return default(T);
        }
    }
    public enum EnumTaxNameType
    {
        [Description("Progressive")]
        Progressive = 1,
        [Description("Flat Value")]
        FlatValue = 2,
        [Description("Flat Rate")]
        FlatRate = 3,
    }
}
