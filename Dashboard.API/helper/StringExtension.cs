using System.Globalization;

namespace Dashboard.API.helper
{
    public static class StringExtension
    {
        public static string ToTitleCase(this string title)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
        }
    }
}
