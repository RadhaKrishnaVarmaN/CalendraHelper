# CalendarHelper

The `CalendarHelper` class is a utility class that provides methods for manipulating and calculating dates, specifically in the context of working days (Monday to Friday).

## Methods

### SetToWorkingDayForward(DateTime adjustDate)

This method adjusts a given date to the next working day. If the provided date is a Saturday, it adds two days to make it a Monday. If it's a Sunday, it adds one day to make it a Monday as well. If the date is neither Saturday nor Sunday (i.e., it's already a working day), the date remains unchanged.

### SetToWorkingDayBackward(DateTime adjustDate)

This method adjusts a given date to the previous working day. If the date is a Sunday, it subtracts two days to make it a Friday. If it's a Monday, it subtracts three days to make it a Friday. If the date is neither Sunday nor Monday (i.e., it's already a working day), the date remains unchanged.

### AddWorkingDays(DateTime adjustDate, int duration)

This method adds a certain number of working days to a given date. If the duration is zero, the original date is returned. Otherwise, the method first adjusts the date to the next or previous working day, depending on whether the duration is positive or negative. Then it calculates the day of the week as an integer, and finally adds the duration and an additional number of days to skip the weekends.

### GetWorkingDays(DateTime startDate, DateTime endDate)

This method calculates the number of working days between two given dates. It first adjusts the start and end dates to the next and previous working days, respectively. Then it calculates the total number of days, the number of weekends, and the number of days in the last week. Finally, it returns the number of working days, which is the total number of days minus twice the number of weekends.

These methods can be useful in various business scenarios where operations are based on working days, such as calculating delivery dates, project deadlines, or financial calculations.


# CalendarHelper2

# WorkDays and CalendarHelper

The `WorkDays` class represents a week's worth of workdays, with boolean properties for each day of the week. This allows for flexibility in defining which days are considered workdays.

The `CalendarHelper` class provides methods for working with dates in the context of these workdays. It uses a static `WorkDays` instance to determine which days are workdays.

## Methods

### IsWorkingDay(DayOfWeek dayOfWeek)

This method checks if a given day of the week is a workday.

### SetToWorkingDayForward(DateTime startDate)

This method adjusts a given date to the next workday.

### SetToWorkingDayBackward(DateTime endDate)

This method adjusts a given date to the previous workday.

### AddWorkingDays(DateTime adjustDate, int duration)

This method adds a certain number of workdays to a given date. It first adjusts the date to the next or previous workday, depending on whether the duration is positive or negative. Then it calculates the total weeks and remaining days, and finally adds the appropriate number of days, skipping non-workdays.

### GetWorkingDays(DateTime startDate, DateTime endDate)

This method calculates the number of workdays between two given dates. It first adjusts the start and end dates to the next and previous workdays, respectively. Then it calculates the total days, total weeks, and remaining days. Finally, it returns the number of workdays, which is the total weeks times the number of workdays per week, plus the number of workdays in the remaining days.

These classes provide a flexible way to work with dates in the context of workdays, as the `WorkDays` class can be customized to define which days are considered workdays. However, they assume that the `WorkDays` instance is static and doesn't change over time. If the workdays can vary, for example, due to holidays or other special occasions, you might need to adjust the code to account for this.


# CalendarHelper.sql

The `CalendarHelper.sql` script provides SQL equivalents of the methods in the `CalendarHelper` C# class for working with dates in the context of working days (Monday to Friday).

## SQL Blocks

### Set To Working Days Forward & Backward

This block declares a date variable `@adjust_date` and calculates the next working day (`working_day_forward`) and the previous working day (`working_day_backward`). It uses the `CHOOSE` function with `DATEPART(WEEKDAY, @adjust_date)` to decide how many days to add or subtract.

### Add Working Days

This block declares an integer variable `@duration` and calculates a new date (`ret_date`) by adding the `@duration` number of working days to `@adjust_date`. It first adjusts `@adjust_date` to the next or previous working day, depending on whether `@duration` is positive or negative. Then it adds `@duration` and an additional number of days to skip the weekends.

### Get Working Days

This block declares date variables `@start_date` and `@end_date` and an integer variable `@working_days`, and calculates the number of working days between `@start_date` and `@end_date`. It first adjusts `@start_date` and `@end_date` to the next and previous working days, respectively. Then it calculates the total number of days and the number of weekends, and finally sets `@working_days` to the number of working days, which is the total number of days minus twice the number of weekends.

This script assumes that the week starts on Sunday (`DateFirst = 7`) and that the language is US English. If these settings are different in your SQL Server, you might need to adjust the script accordingly.