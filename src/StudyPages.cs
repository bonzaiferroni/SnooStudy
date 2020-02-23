using Bonwerk.Markdown;
using ScottPlot;

namespace Bonwerk.SnooStudy
{
    public static class StudyPages
    {
        public static void Add(FileLinker linker, string subName, string studyName, StudyData data)
        {
            var home = linker.GetPage("index");
            var subPage = linker.GetPage(subName);
            
            var studyPage = linker.CreatePage($"{subName} {studyName}", $"subs/{subName}_{studyName}.md");

            var linkText = linker.LinkPage("Home", studyPage, home);
            studyPage.Root.AddText(linkText);
            linkText = linker.LinkPage("Back", studyPage, subPage);
            studyPage.Root.AddText(linkText);

            var table = home.Root.FindContent<Table>(subName);
            linkText = linker.LinkPage(studyName, home, studyPage);
            table.ReplaceValues(studyName, linkText);

            table = subPage.Root.FindContent<Table>(SubPages.OverviewName);
            linkText = linker.LinkPage(studyName, subPage, studyPage);
            table.ReplaceValues(studyName, linkText);

            var image = CreateAccuracyChart(linker, data, subName, studyName);
            linkText = linker.LinkImage($"{subName}-{studyName} R²", studyPage, image);
            studyPage.Root.AddText(linkText);
            
            image = CreateHitRatioChart(linker, data, subName, studyName);
            linkText = linker.LinkImage($"{subName}-{studyName} Hit Ratio", studyPage, image);
            studyPage.Root.AddText(linkText);
        }

        private static Image CreateAccuracyChart(FileLinker linker, StudyData data, string subName, string studyName)
        {
            var plot = new Plot();
            plot.Title($"{subName}-{studyName} Accuracy", fontSize: ProgramConfig.TitleSize);
            plot.PlotScatter(data.DailyOADate, data.DailyRSq, ProgramConfig.Color1, 2, 5, "R²");
            plot.PlotScatter(data.DailyOADate, data.DailyAccuracy, ProgramConfig.Color2, 2, 5, "Accuracy");
            plot.Axis(y1: -.05, y2: 1.05);
            plot.Ticks(dateTimeX: true);
            plot.YTicks(new []{0, .25, .5, .75, 1}, new []{"0", ".25", ".5", ".75", "1"});
            plot.Legend();
            
            ProgramConfig.StylePlot(plot);
            var image = linker.CreateImage($"images/{subName}_{studyName}_Accuracy.png");
            plot.SaveFig(image.Path);
            return image;
        }

        private static Image CreateHitRatioChart(FileLinker linker, StudyData data, string subName, string studyName)
        {
            var plot = new Plot();
            plot.Title($"{subName}-{studyName} Hit Ratio", fontSize: ProgramConfig.TitleSize);
            plot.PlotScatter(data.DailyOADate, data.DailyHitRatio, ProgramConfig.Color1, 2, 5, "Hit");
            plot.PlotScatter(data.DailyOADate, data.DailyHypeRatio, ProgramConfig.Color2, 2, 5, "Hype");
            plot.Axis(y1: -.05, y2: 1.05);
            plot.Ticks(dateTimeX: true);
            plot.YTicks(new []{0, .25, .5, .75, 1}, new []{"0", ".25", ".5", ".75", "1"});
            // plot.Legend();
            
            ProgramConfig.StylePlot(plot);
            var image = linker.CreateImage($"images/{subName}_{studyName}_HitRatio.png");
            plot.SaveFig(image.Path);
            return image;
        }
    }
}