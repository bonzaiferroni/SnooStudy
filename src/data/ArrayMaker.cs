using System;
using System.Collections.Generic;

namespace Bonwerk.SnooStudy
{
    public static class ArrayMaker
    {
        public static double[] GetValues<T>(T[] items, Func<List<T>, double> func, Func<T, DateTime> getTimeGroup)
        {
            if (items.Length == 0) return new double[0];

            var group = getTimeGroup(items[0]);

            var values = new List<double>();
            var dailyItems = new List<T>();
            
            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                var itemGroup = getTimeGroup(item);
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
    }
}