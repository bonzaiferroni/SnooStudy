using System;
using System.Linq;
using Bonwerk.Markdown;

namespace Bonwerk.SnooStudy
{
    public static class SubPages
    {
        public static void Add(FileLinker linker, ScopeData[] scopes)
        {
            var home = linker.GetPage("index");
            
            foreach (var scope in scopes)
            {
                foreach (var sub in scope.Subreddits)
                {
                    var subPage = linker.CreatePage(sub.RName, $"subs/{scope.Name}_{sub.Name}.md");
                    subPage.Root.AddText(linker.LinkPage("Home", subPage, home));

                    var table = new Table();
                    table.AddColumns(TextAlignment.Left, "Model", "Trainer");
                    table.AddColumns(TextAlignment.Right, "n", "R²");
                    
                    foreach (var model in sub.Models)
                    {
                        var modelPage = linker.CreatePage($"{sub.RName}: {model.Name}",
                            $"subs/models/{scope.Name}_{sub.Name}_{model.Name}.md");
                        var modelLinkText = linker.LinkPage(model.Name, subPage, modelPage);
                        table.AddRow(modelLinkText, model.ModelParams.TrainerName, model.ModelParams.N.ToString("N0"),
                            model.ModelParams.RSquared.ToString("N2"));

                        modelPage.Root.AddText(linker.LinkPage("Home", modelPage, home));
                        modelPage.Root.AddText(linker.LinkPage($"Back ({sub.RName})", modelPage, subPage));
                        
                        ChartSections.Add(linker, model, modelPage);
                    }
                    
                    subPage.Root.AddContent(table);
                    
                    var image = CreateDistCharts(linker, sub);
                    var linkText = linker.LinkImage($"{sub.RName} Distributions ({sub.Scope})", subPage, image);
                    subPage.Root.AddText(linkText);
                }
            }
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
    }
}