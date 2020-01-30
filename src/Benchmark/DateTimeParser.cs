using System;

namespace Benchmark
{
    internal class DateTimeParser
    {
        internal int GetYearFromDate(string datetime)
        {
            var datetimeInfo = DateTime.Parse(datetime);
            return datetimeInfo.Year;
        }

        internal int GetYearFromDateSplit(string datetime)
        {
            var dateTimeCollection = datetime.Split('-');
            return int.Parse(dateTimeCollection[0]);
        }

        internal int GetYearFromDateSubstring(string datetime)
        {
            var index = datetime.IndexOf('-');
            return int.Parse(datetime.Substring(0, index));
        }

        internal int GetYearFromDateAsSpan(ReadOnlySpan<char> datetimecollection)
        {
            var index = datetimecollection.IndexOf('-');
            return int.Parse(datetimecollection.Slice(0, index));
        }
    }
}
