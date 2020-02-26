using System;
using System.Linq;
using Bonwerk.LearningML.Auto;
using Bonwerk.Markdown;
using Bonwerk.RedditSpy;
using ScottPlot;

namespace Bonwerk.SnooStudy
{
    public static class ChartSections
    {
        public static void Add(FileLinker linker, ModelData model, Document page)
        {
            var section = page.Root.AddSection("Charts");

            var image = CreateAccuracyChart(linker, model);
            var linkText = linker.LinkImage($"{model.RName} R² ({model.Scope})", page, image);
            section.AddText(linkText);

            image = CreateHitRatioChart(linker, model);
            linkText = linker.LinkImage($"{model.RName} Hit Ratio ({model.Scope})", page, image);
            section.AddText(linkText);
            
            image = CreateScoreCharts(linker, model);
            linkText = linker.LinkImage($"{model.RName} Score Averages ({model.Scope})", page, image);
            section.AddText(linkText);
        }

        private static Image CreateAccuracyChart(FileLinker linker, ModelData model)
        {
            var plot = new Plot();
            plot.Title($"{model.RName}: {model.Name} Accuracy ({model.Scope})", fontSize: ProgramConfig.TitleSize);
            plot.PlotScatter(model.OADate, model.RSquare, ProgramConfig.Color1, 2, 5, "R²");
            plot.PlotScatter(model.OADate, model.Accuracy, ProgramConfig.Color2, 2, 5, "Accuracy");
            plot.Axis(y1: -.05, y2: 1.05);
            plot.Ticks(dateTimeX: true);
            plot.YTicks(new[] {0, .25, .5, .75, 1}, new[] {"0", ".25", ".5", ".75", "1"});
            plot.Legend();

            ProgramConfig.StylePlot(plot);
            var image = linker.CreateImage($"images/models/{model.Scope}_{model.SubName}_{model.Name}_Accuracy.png");
            plot.SaveFig(image.Path);
            return image;
        }

        private static Image CreateHitRatioChart(FileLinker linker, ModelData model)
        {
            var plot = new Plot();
            plot.Title($"{model.RName}: {model.Name} Hit Ratio ({model.Scope})", fontSize: ProgramConfig.TitleSize);

            var series = ArrayMaker.RemoveInvalid(model.OADate, model.HitRatio);
            if (series != null)
            {
                plot.PlotScatter(series.Xs, series.Ys, ProgramConfig.Color1, 2, 5, "Hit");
            }
            
            series = ArrayMaker.RemoveInvalid(model.OADate, model.HypeRatio);
            if (series != null)
            {
                plot.PlotScatter(series.Xs, series.Ys, ProgramConfig.Color2, 2, 5, "Hype");
            }
            
            plot.Axis(y1: -.05, y2: 1.05);
            plot.Ticks(dateTimeX: true);
            plot.YTicks(new[] {0, .25, .5, .75, 1}, new[] {"0", ".25", ".5", ".75", "1"});
            // plot.Legend();

            ProgramConfig.StylePlot(plot);
            var image = linker.CreateImage($"images/models/{model.Scope}_{model.SubName}_{model.Name}_HitRatio.png");
            plot.SaveFig(image.Path);
            return image;
        }

        private static Image CreateScoreCharts(FileLinker linker, ModelData model)
        {
            var top = GetScoreAverages(model.Items.Where(x => x.IsTop).ToArray());
            var bottom = GetScoreAverages(model.Items.Where(x => !x.IsTop).ToArray());
            var hits = GetScoreAverages(model.Items.Where(x => x.IsHit).ToArray());
            var hypes = GetScoreAverages(model.Items.Where(x => x.IsHype).ToArray());
            var xs = ScottPlot.DataGen.Consecutive(PostInfo.UpdateCount);
            var xTicks = xs;
            var xLabels = xs.Select(x => $"{x * PostInfo.UpdateInterval / 60}").ToArray();
            
            var plot = new Plot();
            plot.Title($"{model.RName}: {model.Name} Score Averages ({model.Scope})", fontSize: ProgramConfig.TitleSize);
            
            if (top != null) plot.PlotScatter(xs, top, ProgramConfig.Color1, 2, 5, $"Top");
            if (bottom != null) plot.PlotScatter(xs, bottom, ProgramConfig.Color2, 2, 5, "Bottom");
            if (hits != null) plot.PlotScatter(xs, hits, ProgramConfig.Color3, 2, 5, "Hits");
            if (hypes != null) plot.PlotScatter(xs, hypes, ProgramConfig.Color4, 2, 5, "Hypes");
            
            plot.Legend();
            plot.XTicks(xTicks, xLabels);
            plot.XLabel("Minutes after posting");
            plot.YLabel("Score");

            ProgramConfig.StylePlot(plot);
            var image = linker.CreateImage($"images/models/{model.Scope}_{model.SubName}_{model.Name}_Scores.png");
            plot.SaveFig(image.Path);
            return image;
        }

        private static double[] GetScoreAverages(StudyItem[] top)
        {
            if (top.Length == 0) return null;
            
            var values = new double[PostInfo.UpdateCount];

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = top.Average(x => x.Scores[i]);
            }

            return values;
        }
    }
}