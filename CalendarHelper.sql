-- Note: Language = us_english; DateFirst = 7 (Sunday);

-- Set To Working Days Forward & Backward
DECLARE @adjust_date DATETIME = '07-03-2022' -- Sunday

SELECT
	  working_day_forward	= DATEADD(DAY, CHOOSE(DATEPART(WEEKDAY, @adjust_date),  1, 0, 0, 0, 0, 0,  2), @adjust_date)

	, working_day_backward	= DATEADD(DAY, CHOOSE(DATEPART(WEEKDAY, @adjust_date), -2, 0, 0, 0, 0, 0, -1), @adjust_date)


-- Add Working Days
DECLARE @duration INT = 1

SELECT ret_date = @adjust_date WHERE @duration = 0
-- return

SELECT @adjust_date = CASE	WHEN @duration > 0 
							THEN DATEADD(DAY, CHOOSE(DATEPART(WEEKDAY, @adjust_date), -2, 0, 0, 0, 0, 0, -1), @adjust_date)
							ELSE DATEADD(DAY, CHOOSE(DATEPART(WEEKDAY, @adjust_date), 1, 0, 0, 0, 0, 0, 2), @adjust_date)
						END

SELECT ret_date = DATEADD(DAY, (@duration + ((@duration + ((DATEPART(WEEKDAY, @adjust_date) + IIF(@duration > 0, 5, -13)) % 7)) / 5) * 2), @adjust_date)


-- Get Working Days
DECLARE @start_date DATETIME = '07-03-2022', @end_date DATETIME, @working_days INT

SELECT
	  @start_date = DATEADD(DAY, CHOOSE(DATEPART(WEEKDAY, @start_date), 1, 0, 0, 0, 0, 0, 2), @start_date)

	, @end_date	= DATEADD(DAY, CHOOSE(DATEPART(WEEKDAY, @end_date), -2, 0, 0, 0, 0, 0, -1), @end_date)
	
	-- Duration = (1 + Days betweek start and end dates) - (2 * Weekends between start & end dates)
	, @working_days = (1 + DATEDIFF(dd, @start_date, @end_date))
					- (2 * DATEDIFF(wk, @start_date, @end_date))

SELECT working_days = @working_days
