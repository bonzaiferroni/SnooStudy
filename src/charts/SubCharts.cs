using System;
using System.Drawing;
using System.Linq;
using Bonwerk.Markdown;
using Image = Bonwerk.Markdown.Image;
using TextAlignment = ScottPlot.TextAlignment;

namespace Bonwerk.SnooStudy
{
    public static class SubCharts
    {
        public static void Add(FileLinker linker, Document page, SubData sub)
        {
            var section = page.Root.AddSection("Subreddit Charts");
            
            page.AddImage(CreateDistCharts(linker, sub), $"{sub.RName} Distributions", section);

            page.AddImage(CreateCategoricalCharts(linker, sub), $"{sub.RName} Categorical", section);

            page.AddImage(CreateCorrelationCharts(linker, sub), $"{sub.RName} Correlation", section);
        }

        private static Image CreateDistCharts(FileLinker linker, SubData sub)
        {
            var recipes = ChartRecipes.Distributions;
            
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
                        items = items.Where(x => recipe.IsValid(x)).ToArray();
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

        private static Image CreateCategoricalCharts(FileLinker linker, SubData sub)
        {
            // IsNSFW
            // IsOriginalContent
            // MediaType
            // Author
            // Domain
            // Flair
            // DayOfWeek
            // IsSticky

            var recipes = CatRecipes.Recipes;

            const int cols = 2;
            const int plotHeight = 200;
            const int plotWidth = 400;
            const int bars = 7;
            var rows = (int) Math.Ceiling((float) recipes.Length / cols);

            var mp = new ScottPlot.MultiPlot(width: cols * plotWidth, height: plotHeight * rows, rows: rows,
                cols: cols);

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

                    var groups = sub.Items.GroupBy(recipe.Getter).ToArray();

                    IGrouping<object, StudyItem>[] orderedGroups;
                    if (recipe.Sort == CatRecipe.SortType.Natural)
                    {
                        orderedGroups = groups.OrderBy(x => recipe.Getter(x.First())).ToArray();
                    }
                    else if (recipe.Sort == CatRecipe.SortType.Count)
                    {
                        orderedGroups = groups.OrderByDescending(x => x.Count()).ToArray();
                    }
                    else
                    {
                        orderedGroups = groups.OrderByDescending(x => x.Average(x1 => x1.Outcome)).ToArray();
                    }

                    var groupCount = orderedGroups.Count();
                    var addOther = groupCount > bars;
                    var maxIndex = addOther ? 6 : groupCount;
                    var barCount = addOther ? 7 : groupCount;

                    var xs = new double[barCount];
                    var ys = new double[barCount];
                    var labels = new string[barCount];
                    var names = new string[barCount];
                    
                    for (int i = 0; i < maxIndex; i++)
                    {
                        var group = orderedGroups[i];
                        xs[i] = i;
                        ys[i] = group.Average(x => x.Outcome);
                        labels[i] = $"{group.Count()}";
                        names[i] = group.Key.ToString();
                        
                        
                    }

                    if (addOther)
                    {
                        var remaining = orderedGroups.TakeLast(groupCount - 6).ToArray();
                        var sum = remaining.Select(x => x.Sum(x1 => x1.Outcome)).Sum();
                        var count = remaining.Select(x => x.Count()).Sum(); 
                        xs[6] = 6;
                        ys[6] = (float) sum / count;
                        labels[6] = $"{count}";
                        names[6] = "other";
                    }

                    var maxValue = ys.Max();
                    plot.Axis(0 - .5, barCount - .5, 0, maxValue);
                    plot.Title(recipe.Title);
                    plot.XTicks(xs, labels);
                    plot.Grid(enableVertical: false);
                    var barWidth = barCount > 1 ? .8 : .1;
                    plot.PlotBar(xs, ys, barWidth, color: Color.SeaGreen);

                    for (int i = 0; i < barCount; i++)
                    {
                        var name = names[i];
                        plot.PlotText(name, i - .15 * barCount / bars, 0, Color.White, fontSize: 8, bold: true, 
                            rotation: -90, alignment: TextAlignment.middleLeft);
                    }
                    
                    plot.TightenLayout(5);
                    ProgramConfig.StylePlot(plot);
                }
            }
            
            var image = linker.CreateImage($"images/{sub.Scope}_{sub.Name}_Catagorical.png");
            mp.SaveFig(image.Path);
            return image;
        }

        private static Image CreateCorrelationCharts(FileLinker linker, SubData sub)
        {
            var recipes = ChartRecipes.Correlation;
            
            const int cols = 2;
            const int plotHeight = 400;
            const int plotWidth = 400;
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
                        items = items.Where(x => recipe.IsValid(x)).ToArray();
                    }
                    
                    plot.Title(recipe.Title);

                    if (items.Length == 0) continue;

                    var xs = items.Select(recipe.Getter).ToArray();
                    var ys = items.Select(x => (double) x.Outcome).ToArray();

                    plot.PlotScatter(xs, ys, ProgramConfig.Color1, 0, 2.5);
                    plot.Ticks(useMultiplierNotation: false);
                    
                    // var model = new ScottPlot.Statistics.LinearRegressionLine(xs, ys);
                    // double x1 = xs[0];
                    // double x2 = xs[^1];
                    // double y1 = model.GetValueAt(x1);
                    // double y2 = model.GetValueAt(x2);
                    // plot.PlotLine(x1, y1, x2, y2, ProgramConfig.Color2, 2);
                    
                    plot.TightenLayout(5);
                    ProgramConfig.StylePlot(plot);
                }
            }
            
            var image = linker.CreateImage($"images/{sub.Scope}_{sub.Name}_Correlations.png");
            mp.SaveFig(image.Path);
            return image;
        }
    }
}