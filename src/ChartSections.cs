using System;
using System.Linq;
using Bonwerk.Markdown;
using ScottPlot;

namespace Bonwerk.SnooStudy
{
    public static class ChartSections
    {
        public static void Add(FileLinker linker, SubData sub, Document page)
        {
            var section = page.Root.AddSection("Charts");

            var image = CreateAccuracyChart(linker, sub);
            var linkText = linker.LinkImage($"{sub.RName} R² ({sub.Scope})", page, image);
            section.AddText(linkText);

            image = CreateHitRatioChart(linker, sub);
            linkText = linker.LinkImage($"{sub.RName} Hit Ratio ({sub.Scope})", page, image);
            section.AddText(linkText);

            image = CreateDistCharts(linker, sub);
            linkText = linker.LinkImage($"{sub.RName} Distributions ({sub.Scope})", page, image);
            section.AddText(linkText);
        }

        private static Image CreateAccuracyChart(FileLinker linker, SubData sub)
        {
            var plot = new Plot();
            plot.Title($"{sub.RName} Accuracy ({sub.Scope})", fontSize: ProgramConfig.TitleSize);
            plot.PlotScatter(sub.DailyOADate, sub.DailyRSq, ProgramConfig.Color1, 2, 5, "R²");
            plot.PlotScatter(sub.DailyOADate, sub.DailyAccuracy, ProgramConfig.Color2, 2, 5, "Accuracy");
            plot.Axis(y1: -.05, y2: 1.05);
            plot.Ticks(dateTimeX: true);
            plot.YTicks(new[] {0, .25, .5, .75, 1}, new[] {"0", ".25", ".5", ".75", "1"});
            plot.Legend();

            ProgramConfig.StylePlot(plot);
            var image = linker.CreateImage($"images/{sub.Scope}_{sub.Name}_Accuracy.png");
            plot.SaveFig(image.Path);
            return image;
        }

        private static Image CreateHitRatioChart(FileLinker linker, SubData sub)
        {
            var plot = new Plot();
            plot.Title($"{sub.RName} Hit Ratio ({sub.Scope})", fontSize: ProgramConfig.TitleSize);
            plot.PlotScatter(sub.DailyOADate, sub.DailyHitRatio, ProgramConfig.Color1, 2, 5, "Hit");
            plot.PlotScatter(sub.DailyOADate, sub.DailyHypeRatio, ProgramConfig.Color2, 2, 5, "Hype");
            plot.Axis(y1: -.05, y2: 1.05);
            plot.Ticks(dateTimeX: true);
            plot.YTicks(new[] {0, .25, .5, .75, 1}, new[] {"0", ".25", ".5", ".75", "1"});
            // plot.Legend();

            ProgramConfig.StylePlot(plot);
            var image = linker.CreateImage($"images/{sub.Scope}_{sub.Name}_HitRatio.png");
            plot.SaveFig(image.Path);
            return image;
        }

        private static Image CreateDistCharts(FileLinker linker, SubData sub)
        {
            var recipes = DistRecipes.Recipes;
            
            const int cols = 4;
            const int plotHeight = 200;
            const int plotWidth = 200;
            const int bars = 20;
            var rows = (int) Math.Ceiling((float) recipes.Length / cols);
            
            var mp = new ScottPlot.MultiPlot(width: cols * plotWidth, height: plotHeight * rows, rows: rows, cols: cols);

            for (var column = 0; column < cols; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    var recipeIndex = row * cols + column;
                    var plot = mp.subplots[recipeIndex];
                    ProgramConfig.StylePlot(plot);
                    var c = ProgramConfig.BgColor;
                    plot.Style(c, c, c, c, c);
                    
                    if (recipeIndex >= recipes.Length) continue;

                    var recipe = recipes[recipeIndex];

                    var items = sub.Items;
                    if (recipe.IsValid != null)
                    {
                        items = items.Where(x => recipe.IsValid(recipe.Getter(x))).ToArray();
                    }
                    
                    plot.Title(recipe.Title);

                    if (items.Length == 0) continue;
                    
                    var min = items.Min(recipe.Getter);
                    var max = items.Max(recipe.Getter);
                    var average = items.Average(recipe.Getter);
                    var boostedAverage = average * 4;
                    var useBin = max > boostedAverage;
                    var upperLimit = useBin ? boostedAverage : max;
                    var intervalCount = useBin ? bars - 1 : bars;
                    
                    var interval = (upperLimit - min) / intervalCount;
                    var barValues = new double[bars];
                    var positions = new double[bars];
                    for (int i = 0; i < intervalCount; i++)
                    {
                        var lower = i * interval + min;
                        var upper = (i + 1) * interval + min;
                        barValues[i] = items.Count(x => recipe.Getter(x) > lower && recipe.Getter(x) < upper);
                        positions[i] = interval * i + interval * .5f + min;
                    }

                    if (useBin)
                    {
                        var i = bars - 1;
                        barValues[i] = items.Count(x => recipe.Getter(x) > upperLimit);
                        positions[i] = interval * i + interval * .5f + min;
                    }
                    
                    plot.Grid(enableHorizontal: true, enableVertical: false);

                    var maxCount = barValues.Max();
                    if (maxCount == 0) continue;

                    const int yTicksCount = 5;
                    var yTickValues = new double[yTicksCount];
                    var yTickLabels = new string[yTicksCount];
                    for (int i = 0; i < yTicksCount; i++)
                    {
                        yTickValues[i] = (double) i / (yTicksCount - 1) * maxCount;
                        yTickLabels[i] = yTickValues[i].ToString("N0");
                    }
                    
                    plot.Axis(min, interval * bars, 0, maxCount);
                    plot.YTicks(yTickValues, yTickLabels);
                    plot.XTicks(new[] {min, average, upperLimit}, 
                        new[] {min.ToString("N0"), average.ToString("N0"), upperLimit.ToString("N0")});
                    
                    plot.PlotBar(positions, barValues, interval * .75f);
                    plot.TightenLayout(5);
                    ProgramConfig.StylePlot(plot);
                }
            }
            
            var image = linker.CreateImage($"images/{sub.Scope}_{sub.Name}_Distributions.png");
            mp.SaveFig(image.Path);
            return image;
        }

        // private static Image CreateScoreCharts(FileLinker linker, SubData sub) 
    }
}