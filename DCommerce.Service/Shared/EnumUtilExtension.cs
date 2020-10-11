using System;
namespace DCommerce.Service.Shared
{
    public class EnumUtilExtension
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
