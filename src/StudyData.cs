using System;
using System.Collections.Generic;
using System.Linq;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    public class StudyData
    {
        public StudyData(StudyItem[] items, ModelParams currentParams)
        {
            Items = items;
            CurrentParams = currentParams;

            DailyOADate = GetDailyValues(x => x[0].Recorded.Date.ToOADate());
            DailyRSq = GetDailyValues(x => x.Average(x1 => x1.RSquared));
            DailyAccuracy = GetDailyValues(x => (double) x.Count(x1 => x1.IsAccurate()) / x.Count);
        }

        public StudyItem[] Items { get; }
        public ModelParams CurrentParams { get; }
        
        public double[] DailyOADate { get; }
        public double[] DailyRSq { get; }
        public double[] DailyAccuracy { get; }

        public double[] GetDailyValues(Func<List<StudyItem>, double> func)
        {
            if (Items.Length == 0) return new double[0];

            var date = Items[0].Recorded.Date;

            var values = new List<double>();
            var dailyItems = new List<StudyItem>();
            
            for (int i = 0; i < Items.Length; i++)
            {
                var item = Items[i];
                if (item.Recorded.Date != date && dailyItems.Count > 0)
                {
                    date = item.Recorded.Date;
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