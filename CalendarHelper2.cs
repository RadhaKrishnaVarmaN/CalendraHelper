using System;

public class WorkDays
{
    public int WorkDayID { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
}

public static class CalendarHelper
{
    public static readonly WorkDays workDays;
    public static readonly int workingDays;

    static CalendarHelper()
    {
        workDays = new WorkDays { Monday = true, Tuesday = true, Wednesday = true, Thursday = true, Friday = true };

        workingDays = (workDays.Sunday ? 1 : 0) + (workDays.Monday ? 1 : 0)
                + (workDays.Tuesday ? 1 : 0) + (workDays.Wednesday ? 1 : 0)
                + (workDays.Thursday ? 1 : 0) + (workDays.Friday ? 1 : 0)
                + (workDays.Saturday ? 1 : 0);
    }

    public static bool IsWorkingDay(DayOfWeek dayOfWeek)
    {
        if (dayOfWeek == DayOfWeek.Monday)
        {
            return workDays.Monday;
        }

        if (dayOfWeek == DayOfWeek.Tuesday)
        {
            return workDays.Tuesday;
        }

        if (dayOfWeek == DayOfWeek.Wednesday)
        {
            return workDays.Wednesday;
        }

        if (dayOfWeek == DayOfWeek.Thursday)
        {
            return workDays.Thursday;
        }

        if (dayOfWeek == DayOfWeek.Friday)
        {
            return workDays.Friday;
        }

        if (dayOfWeek == DayOfWeek.Saturday)
        {
            return workDays.Saturday;
        }

        //if (dayOfWeek == DayOfWeek.Sunday)
        {
            return workDays.Sunday;
        }
    }

    public static DateTime SetToWorkingDayForward(DateTime startDate)
    {
        int addDays = 0;
        DayOfWeek dayOfWeek = startDate.DayOfWeek;

        while (!IsWorkingDay(dayOfWeek))
        {
            addDays++;
            dayOfWeek = (dayOfWeek == DayOfWeek.Saturday) ? 0 : (dayOfWeek + 1);
        }

        return startDate.Date.AddDays(addDays);
    }

    public static DateTime SetToWorkingDayBackward(DateTime endDate)
    {
        int addDays = 0;
        DayOfWeek dayOfWeek = endDate.DayOfWeek;

        while (!IsWorkingDay(dayOfWeek))
        {
            addDays--;
            dayOfWeek = (dayOfWeek == DayOfWeek.Sunday) ? (DayOfWeek)6 : (dayOfWeek - 1);
        }

        return endDate.Date.AddDays(addDays);
    }

    public static DateTime AddWorkingDays(DateTime adjustDate, int duration)
    {
        if (duration == 0)
        {
            return adjustDate;
        }

        adjustDate = (duration > 0) ? SetToWorkingDayBackward(adjustDate)
                                    : SetToWorkingDayForward(adjustDate);

        int totalWeeks = (duration / workingDays);
        int lastWeekDays = (duration % workingDays);

        if (lastWeekDays == 0)
        {
            if (duration > 0)
            {
                totalWeeks--;
                lastWeekDays = workingDays;
            }
            else
            {
                totalWeeks++;
                lastWeekDays = -workingDays;
            }
        }

        int addDays = 0;
        DayOfWeek dayOfWeek = adjustDate.DayOfWeek;

        while (lastWeekDays != 0)
        {
            if (lastWeekDays > 0)
            {
                lastWeekDays--;
                addDays++;
                dayOfWeek = (dayOfWeek == DayOfWeek.Saturday) ? 0 : (dayOfWeek + 1);

                while (!IsWorkingDay(dayOfWeek))
                {
                    addDays++;
                    dayOfWeek = (dayOfWeek == DayOfWeek.Saturday) ? 0 : (dayOfWeek + 1);
                }
            }
            else
            {
                lastWeekDays++;
                addDays--;
                dayOfWeek = (dayOfWeek == DayOfWeek.Sunday) ? (DayOfWeek)6 : (dayOfWeek - 1);

                while (!IsWorkingDay(dayOfWeek))
                {
                    addDays--;
                    dayOfWeek = (dayOfWeek == DayOfWeek.Sunday) ? (DayOfWeek)6 : (dayOfWeek - 1);
                }
            }
        }

        return adjustDate.Date.AddDays((totalWeeks * 7) + addDays);
    }

    public static int GetWorkingDays(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            DateTime tmpDate = startDate;
            startDate = endDate;
            endDate = tmpDate;
        }

        startDate = SetToWorkingDayForward(startDate);
        endDate = SetToWorkingDayBackward(endDate);

        int totalDays = (endDate - startDate).Days + 1;
        int totalWeeks = (totalDays / workingDays);
        totalWeeks -= (totalWeeks > 0 ? 1 : 0);
        int remainingDays = totalDays - (totalWeeks * 7);

        int addDays = 0;
        DayOfWeek dayOfWeek = startDate.DayOfWeek;

        while (remainingDays > 0)
        {
            remainingDays--;
            if (IsWorkingDay(dayOfWeek))
            {
                addDays++;
            }
            dayOfWeek = (dayOfWeek == DayOfWeek.Saturday) ? 0 : (dayOfWeek + 1);
        }

        return (totalWeeks * workingDays) + addDays;
    }
}
