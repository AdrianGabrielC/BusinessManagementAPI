namespace GlanzCleanAPI.Utilities.RequestFeatures
{
    public class WorkParameters: RequestParameters
    {
        // Year. If year is not provided, the default value is the current year. 
        public int Year { get; set; } = DateTime.Now.Year;
        public bool ValidYear => Year > 0;

        // Month.  If month is not provided, the default value is null and all dates from the current year will be retrieved. 
        public int? Month { get; set; } = null;
        public bool ValidMonth => Month > 0 && Month < 13;

        // Day.  If day is not provided, the default value is the null and all days will be retrieved.
        public int? Day { get; set; } = null;
        public bool ValidDay => Day > 0 && Day <= DateTime.DaysInMonth(DateTime.Now.Year, (Month ?? DateTime.Now.Month));
    }
}
