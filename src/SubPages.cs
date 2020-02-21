using Bonwerk.Markdown;

namespace Bonwerk.SnooStudy
{
    public static class SubPages
    {
        public static void Add(Document home, SubData data)
        {
            foreach (var subKvp in data)
            {
                var subName = subKvp.Key;
                var pagePath = $"subs/{subName}.md";
                var document = new Document(subName, $"{StudyProgram.DocsPath}/{pagePath}");
                document.Root.AddParagraph(PathHelper.TextLink("Back", "../index.md"));

                var homeSection = home.Root.FindSection(subName);
                homeSection.Heading.Text = PathHelper.TextLink(subName, pagePath);
                
                var table = ContentHelper.GetParamsTable(subKvp.Value);
                document.Root.Children.Add(table);
                
                foreach (var studyKvp in subKvp.Value)
                {
                    var studyName = studyKvp.Value;
                }

                document.Save();
            }
        }
    }
}