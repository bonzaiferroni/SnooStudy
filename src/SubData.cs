using System;
using System.Collections.Generic;
using System.Linq;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    public class SubData
    {
        public SubData(string name, string scope, StudyItem[] items, ModelParams currentParams)
        {
            Name = name;
            Scope = scope;
            Items = items;
            CurrentParams = currentParams;

            DailyOADate = GetDailyValues(x => x[0].Created.Date.ToOADate());
            DailyRSq = GetDailyValues(x => x.Average(x1 => x1.RSquared));
            DailyAccuracy = GetDailyValues(x => (double) x.Count(x1 => x1.IsAccurate()) / x.Count);
            DailyHitRatio = GetDailyValues(x => (double) x.Count(x1 => x1.IsHit) / x.Count(x1 => x1.IsPopular));
            DailyHypeRatio = GetDailyValues(x => (double) x.Count(x1 => x1.IsHype) / x.Count(x1 => !x1.IsPopular));
        }

        public string Name { get; }
        public string Scope { get; }
        public StudyItem[] Items { get; }
        public ModelParams CurrentParams { get; }
        
        public double[] DailyOADate { get; }
        public double[] DailyRSq { get; }
        public double[] DailyAccuracy { get; }
        public double[] DailyHitRatio { get; }
        public double[] DailyHypeRatio { get; }

        public double[] GetDailyValues(Func<List<StudyItem>, double> func)
        {
            if (Items.Length == 0) return new double[0];

            var date = Items[0].Created.Date;

            var values = new List<double>();
            var dailyItems = new List<StudyItem>();
            
            for (int i = 0; i < Items.Length; i++)
            {
                var item = Items[i];
                if (item.Created.Date != date && dailyItems.Count > 0)
                {
                    date = item.Created.Date;
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
    }
}