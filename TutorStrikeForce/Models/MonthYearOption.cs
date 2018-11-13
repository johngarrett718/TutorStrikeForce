using TutorStrikeForce.Helpers;

namespace TutorStrikeForce.Models
{
    public class MonthYearOption
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string DisplayDate()
        {
            return $"{DateTimeHelper.GetAbbreviatedMonthNameFromNumber(Month)} {Year}";
        }
    }
}
