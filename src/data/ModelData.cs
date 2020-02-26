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

            HourlyOADate = GetDailyValues(x => GetTimeGroup(x[0].Created).ToOADate());
            HourlyRSq = GetDailyValues(x => x.Average(x1 => x1.RSquared));
            HourlyAccuracy = GetDailyValues(x => (double) x.Count(x1 => x1.IsAccurate()) / x.Count);
            HourlyHitRatio = GetDailyValues(x => (double) x.Count(x1 => x1.IsHit) / x.Count(x1 => x1.IsTop));
            HourlyHypeRatio = GetDailyValues(x => (double) x.Count(x1 => x1.IsHype) / x.Count(x1 => !x1.IsTop));
        }

        public string Name { get; }
        public string SubName { get; }
        public string RName => $"r/{SubName}";
        public string Scope { get; }
        public StudyItem[] Items { get; }
        public ModelParams ModelParams { get; }
        
        public double[] HourlyOADate { get; }
        public double[] HourlyRSq { get; }
        public double[] HourlyAccuracy { get; }
        public double[] HourlyHitRatio { get; }
        public double[] HourlyHypeRatio { get; }

        public double[] GetDailyValues(Func<List<StudyItem>, double> func)
        {
            if (Items.Length == 0) return new double[0];

            var group = GetTimeGroup(Items[0].Created);

            var values = new List<double>();
            var dailyItems = new List<StudyItem>();
            
            for (int i = 0; i < Items.Length; i++)
            {
                var item = Items[i];
                var itemGroup = GetTimeGroup(item.Created);
                if (itemGroup != group && dailyItems.Count > 0)
                {
                    group = itemGroup;
                    var value = func(dailyItems);
                    values.Add(value);
                    dailyItems.Clear();
                }
                
                dailyItems.Add(item);
            }

            // last one
            if (dailyItems.Count > 0)
            {
                var value = func(dailyItems);
                values.Add(value);
                values.Add(value);
            }

            return values.ToArray();
        }

        private DateTime GetTimeGroup(in DateTime created)
        {
            return new DateTime(created.Year, created.Month, created.Day, created.Hour, 0, 0);
        }
    }
}