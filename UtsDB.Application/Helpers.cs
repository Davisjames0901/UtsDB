using UtsDB.Domain.Data;

namespace UtsDB.Application;

public static class Helpers
{
    
    public static DateTime GetDataEndDate(DateTime start, int length, DataFrequency dataFrequency) => dataFrequency switch
    {
        DataFrequency.Daily => start.AddDays(length),
        DataFrequency.Weekly => start.AddDays(length * 7),
        DataFrequency.Monthly => start.AddMonths(length),
        DataFrequency.Quarterly => start.AddMonths(length * 3),
        DataFrequency.SemiAnnually => start.AddMonths(length * 6),
        DataFrequency.Annually => start.AddYears(length),
        _ => throw new NotImplementedException()
    };


    public static int GetDataLength(DateTime? start, DateTime end, DataFrequency dataFrequency)
    {
        start ??= end;
        var range = end - start;
        var monthDiff = ((end.Year - start.Value.Year) * 12) + end.Month - start.Value.Month;
        return dataFrequency switch
        {
            DataFrequency.Daily => range.Value.Days,
            DataFrequency.Weekly => range.Value.Days/7,
            DataFrequency.Monthly => monthDiff,
            DataFrequency.Quarterly => (int)Math.Floor((double)monthDiff/4),
            DataFrequency.SemiAnnually => (int)Math.Floor((double)monthDiff/6),
            DataFrequency.Annually => end.Year - start.Value.Year,
            _ => throw new NotImplementedException()
        };
    }
}