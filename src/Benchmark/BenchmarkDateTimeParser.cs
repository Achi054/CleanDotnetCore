using System;
using BenchmarkDotNet.Attributes;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class BenchmarkDateTimeParser
    {
        private readonly string datetime = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ssZ");
        private readonly DateTimeParser _parser = new DateTimeParser();

        [Benchmark(Baseline = true)]
        public void Benchmar_GetYearFromDate()
        {
            _parser.GetYearFromDate(datetime);
        }

        [Benchmark]
        public void Benchmar_GetYearFromDateSplit()
        {
            _parser.GetYearFromDateSplit(datetime);
        }

        [Benchmark]
        public void Benchmar_GetYearFromDateSubstring()
        {
            _parser.GetYearFromDateSubstring(datetime);
        }

        [Benchmark]
        public void Benchmar_GetYearFromDateAsSpan()
        {
            _parser.GetYearFromDateAsSpan(datetime);
        }
    }
}
