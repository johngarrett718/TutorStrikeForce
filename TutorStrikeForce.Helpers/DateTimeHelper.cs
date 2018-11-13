using System.Globalization;

namespace TutorStrikeForce.Helpers
{
    public static class DateTimeHelper
    {
        public static string GetMonthNameFromNumber(int monthNumber)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNumber);
        }

        public static string GetAbbreviatedMonthNameFromNumber(int monthNumber)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(monthNumber);
        }
    }
}
