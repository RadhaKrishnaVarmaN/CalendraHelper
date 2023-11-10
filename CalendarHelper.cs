using System;

public static class CalendarHelper
{
    /// <summary>
    /// Adjusts the date to the next working day.
    /// </summary>
    /// <param name="adjustDate">The date to adjust.</param>
    /// <returns>The adjusted date.</returns>
    public static DateTime SetToWorkingDayForward(DateTime adjustDate)
    {
        int daysToAdjust = 0;

        switch (adjustDate.DayOfWeek)
        {
            case DayOfWeek.Saturday:
                daysToAdjust = 2;
                break;
            case DayOfWeek.Sunday:
                daysToAdjust = 1;
                break;
        }

        return adjustDate.Date.AddDays(daysToAdjust);
    }

    /// <summary>
    /// Adjusts the date to the previous working day.
    /// </summary>
    /// <param name="adjustDate">The date to adjust.</param>
    /// <returns>The adjusted date.</returns>
    public static DateTime SetToWorkingDayBackward(DateTime adjustDate)
    {
        int daysToAdjust = 0;

        switch (adjustDate.DayOfWeek)
        {
            case DayOfWeek.Sunday:
                daysToAdjust = -2;
                break;
            case DayOfWeek.Monday:
                daysToAdjust = -3;
                break;
        }

        return adjustDate.Date.AddDays(daysToAdjust);
    }

    public static DateTime AddWorkingDays(DateTime adjustDate, int duration)
    {
        if (duration == 0)
        {
            return adjustDate;
        }

        adjustDate = (duration > 0) ? SetToWorkingDayBackward(adjustDate)
                                    : SetToWorkingDayForward(adjustDate);

        var dayOfWeek = ((int)adjustDate.DayOfWeek + (duration > 0 ? 6 : -12)) % 7;

        return adjustDate.Date.AddDays(duration + ((duration + dayOfWeek) / 5) * 2);
    }

    public static int GetWorkingDays(DateTime startDate, DateTime endDate)
    {
        startDate = SetToWorkingDayForward(startDate);
        endDate = SetToWorkingDayBackward(endDate);

        int totalDays = (endDate - startDate).Days;
        int totalWeekEnds = (totalDays / 7);
        int lastWeekDays = (totalWeekEnds == 0 ? totalDays : (totalDays % 7)) + (int)startDate.DayOfWeek;
        totalWeekEnds += (lastWeekDays > 5 ? 1 : 0);

        // Working Days = (1 + Days between Start and End Dates) - (2 * WeekEnds between Start and End Dates)
        return (1 + totalDays) - (2 * totalWeekEnds);
    }

}
