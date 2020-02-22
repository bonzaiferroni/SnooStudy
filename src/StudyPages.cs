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
        public static void Add(Document home, Document subPage, string subName, string studyName, StudyData data)
        {
            var pagePath = $"subs/{subName}_{studyName}.md";
            var document = new Document($"{subName} {studyName}", $"{StudyProgram.DocsPath}/{pagePath}");
            document.Root.AddText(PathHelper.TextLink("Back", $"{subName}.md"));

            var table = home.Root.FindContent<Table>(subName);
            AddLinkToTable(studyName, table, pagePath);

            table = subPage.Root.FindContent<Table>(SubPages.OverviewName);
            AddLinkToTable(studyName, table, $"{subName}_{studyName}.md");

            var fileName = $"{subName}_{studyName}_RSq.png";
            GetRSqChart(data.Archive, subName, studyName, fileName);
            document.Root.AddText(PathHelper.Image("R²", $"../images/{fileName}"));
            
            document.Save();
        }

        private static void AddLinkToTable(string studyName, Table table, string pagePath)
        {
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                for (var j = 0; j < row.Count; j++)
                {
                    var value = row[j];
                    if (value != studyName) continue;
                    table.Rows[i][j] = PathHelper.TextLink(studyName, pagePath);
                }
            }
        }

        private static void GetRSqChart(List<ArchiveItem> data, string subName, string studyName, string fileName)
        {
            var ys = data.Select(x => (double) (studyName == "guess" ? x.GuessRSquared : x.HunchRSquared)).ToArray();
            var xs = data.Select(x => x.Created.ToOADate()).ToArray();
            
            var plot = new Plot();
            plot.Title($"{subName} {studyName} R²", fontSize: 24);
            plot.PlotScatter(xs, ys, Color.Chartreuse, markerSize: 0, lineWidth: 2);
            plot.Style(Style.Blue1);
            plot.Axis(y1: -.05, y2: 1.05);
            var docsPath = $"{StudyProgram.DocsPath}/images/{fileName}";
            plot.Ticks(dateTimeX: true);
            plot.YTicks(new []{0, .25, .5, .75, 1}, new []{"0", ".25", ".5", ".75", "1"});
            plot.Style(label: Color.LightBlue, tick: Color.LightBlue, grid: Color.DimGray);
            plot.SaveFig(docsPath);
        }
    }
}