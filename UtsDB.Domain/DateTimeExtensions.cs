using UtsDB.Domain.Data;

namespace UtsDB.Domain;

public static class DateTimeExtensions
{
    //Todo: implement me
    public static DateTime AdvanceByPeriod(this DateTime datetime, DataFrequency frequency, int numberOfPeriods = 1)
    {
        return datetime;
    }

    public static DateTime RetardByPeriod(this DateTime datetime, DataFrequency frequency, int numberOfPeriods = 1)
    {
        return AdvanceByPeriod(datetime, frequency, numberOfPeriods * -1);
    }
    
    public static bool DateFallsInRange(this DateTime target, DateTime start, DateTime end) => target >= start && target <= end;
}