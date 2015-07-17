using System.ComponentModel;
using System.Reflection;

namespace ReviewMe.Common.Enums
{
    public static class EnumHelper
    {
        public static string DescriptionAttr<T>(T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(
                typeof (DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            return source.ToString();
        }
    }
}