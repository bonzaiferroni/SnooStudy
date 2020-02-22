using Bonwerk.Markdown;

namespace Bonwerk.SnooStudy
{
    public static class SubPages
    {
        public const string OverviewName = "Overview";
        
        public static void Add(FileLinker linker, SubData data)
        {
            var home = linker.GetPage("index");
            
            foreach (var subKvp in data)
            {
                var subName = subKvp.Key;
                var subPage = linker.CreatePage(subName, $"subs/{subName}.md");
                
                subPage.Root.AddText(linker.LinkPage("Home", subPage, home));

                var homeSection = home.Root.FindSection(subName);
                homeSection.Heading.Text = linker.LinkPage(subName, home, subPage);

                var section = subPage.Root.AddSection(OverviewName);
                var table = ContentHelper.GetParamsTable(subKvp.Value);
                section.AddContent(table);
                
                foreach (var studyKvp in subKvp.Value)
                {
                    var studyName = studyKvp.Key;
                    StudyPages.Add(linker, subName, studyName, studyKvp.Value);
                }
            }
        }
    }
}