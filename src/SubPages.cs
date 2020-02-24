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
                    var subPage = linker.CreatePage(sub.Name, $"subs/{scope.Name}_{sub.Name}.md");
                    subPage.Root.AddText(linker.LinkPage("Home", subPage, home));
                    ChartSections.Add(linker, sub, subPage);
                }
            }
        }
    }
}