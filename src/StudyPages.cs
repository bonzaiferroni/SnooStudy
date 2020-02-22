using System.Linq;
using Bonwerk.Markdown;

namespace Bonwerk.SnooStudy
{
    public static class StudyPages
    {
        public static void Add(Document home, Document subPage, string subName, string studyName, StudyData data)
        {
            var pagePath = $"subs/{subName}_{studyName}.md";
            var document = new Document($"{subName} {studyName}", $"{StudyProgram.DocsPath}/{pagePath}");
            document.Root.AddParagraph(PathHelper.TextLink("Back", $"{subName}.md"));

            var table = home.Root.FindContent<Table>(subName);
            AddLinkToTable(studyName, table, pagePath);

            table = subPage.Root.FindContent<Table>(SubPages.OverviewName);
            AddLinkToTable(studyName, table, $"{subName}_{studyName}.md");
            
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
    }
}