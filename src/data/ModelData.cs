using System;
using System.Collections.Generic;
using System.Linq;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    public class ModelData
    {
        public ModelData(string name, string subName, string scope, StudyItem[] items, ModelParams modelParams)
        {
            Name = name;
            SubName = subName;
            Scope = scope;
            Items = items;
            ModelParams = modelParams;

            Interval = GetInterval(items.Last().Created - items.First().Created);

            HourlyOADate = ArrayMaker.GetValues(items, x => GetHourGroup(x[0]).ToOADate(), GetHourGroup);
            HourlyRSq = ArrayMaker.GetValues(items, x => x.Average(x1 => x1.RSquared), GetHourGroup);
            HourlyAccuracy = ArrayMaker.GetValues(items, 
                x => (double) x.Count(x1 => x1.IsAccurate()) / x.Count, GetHourGroup);
            HourlyHitRatio = ArrayMaker.GetValues(items, 
                x => (double) x.Count(x1 => x1.IsHit) / x.Count(x1 => x1.IsTop), GetHourGroup);
            HourlyHypeRatio = ArrayMaker.GetValues(items, 
                x => (double) x.Count(x1 => x1.IsHype) / x.Count(x1 => !x1.IsTop), GetHourGroup);
        }

        public string Name { get; }
        public string SubName { get; }
        public string RName => $"r/{SubName}";
        public string Scope { get; }
        public StudyItem[] Items { get; }
        public ModelParams ModelParams { get; }
        public SampleInterval Interval { get; }
        
        public double[] HourlyOADate { get; }
        public double[] HourlyRSq { get; }
        public double[] HourlyAccuracy { get; }
        public double[] HourlyHitRatio { get; }
        public double[] HourlyHypeRatio { get; }

        private DateTime GetHourGroup(StudyItem item)
        {
            var created = item.Created;
            switch (Interval)
            {
                case SampleInterval.Hourly:
                    return new DateTime(created.Year, created.Month, created.Day, created.Hour, 0, 0);
                case SampleInterval.Daily:
                    return new DateTime(created.Year, created.Month, created.Day, 0, 0, 0);
                case SampleInterval.Monthly:
                    return new DateTime(created.Year, created.Month, 0, 0, 0, 0);
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private SampleInterval GetInterval(in TimeSpan span)
        {
            const int minSamples = 8;
            var interval = span / minSamples;
            if (interval > TimeSpan.FromDays(30)) return SampleInterval.Monthly;
            if (interval > TimeSpan.FromDays(1)) return SampleInterval.Daily;
            return SampleInterval.Hourly;
        }

        public enum SampleInterval
        {
            Hourly,
            Daily,
            Monthly,
        }
    }
}