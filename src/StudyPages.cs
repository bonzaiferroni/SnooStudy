using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Bonwerk.Markdown;
using Bonwerk.RedditSpy;
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

            var image = CreateRSqChart(linker, data.Archive, subName, studyName);
            linkText = linker.LinkImage($"{studyName} R²", studyPage, image);
            studyPage.Root.AddText(linkText);
        }

        private static Markdown.Image CreateRSqChart(FileLinker linker, List<ArchiveItem> data, string subName, 
            string studyName)
        {
            var ys = data.Select(x => (double) (studyName == "guess" ? x.GuessRSquared : x.HunchRSquared)).ToArray();
            var xs = data.Select(x => x.Created.ToOADate()).ToArray();
            
            var plot = new Plot();
            plot.Title($"{subName} {studyName} R²", fontSize: 24);
            plot.PlotScatter(xs, ys, Color.Chartreuse, markerSize: 0, lineWidth: 2);
            plot.Style(Style.Blue1);
            plot.Axis(y1: -.05, y2: 1.05);
            var image = linker.CreateImage($"images/{subName}_{studyName}_RSq.png");
            plot.Ticks(dateTimeX: true);
            plot.YTicks(new []{0, .25, .5, .75, 1}, new []{"0", ".25", ".5", ".75", "1"});
            plot.Style(label: Color.LightBlue, tick: Color.LightBlue, grid: Color.DimGray);
            plot.SaveFig(image.Path);
            return image;
        }
    }
}