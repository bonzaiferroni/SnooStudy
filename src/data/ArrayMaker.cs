using System;
using System.Collections.Generic;
using System.Linq;

namespace Bonwerk.SnooStudy
{
    public static class ArrayMaker
    {
        public const double InvalidValue = -1;
        
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

        public static SeriesValues RemoveInvalid(double[] xs, double[] ys)
        {
            if (!FindInvalid(ys)) return new SeriesValues(xs, ys);

            var indices = ys
                .Select((x, index) => x != InvalidValue ? index : -1)
                .Where(x => x != -1)
                .ToArray();
            if (indices.Length == 0) return null;

            var newXs = new double[indices.Length];
            var newYs = new double[indices.Length];

            for (int i = 0; i < indices.Length; i++)
            {
                newXs[i] = xs[indices[i]];
                newYs[i] = ys[indices[i]];
            }

            return new SeriesValues(newXs, newYs);
        }

        private static bool FindInvalid(double[] ys)
        {
            foreach (var y in ys)
            {
                if (y == InvalidValue)
                {
                    return true;
                }
            }

            return false;
        }
    }
}