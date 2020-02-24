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

                    subPage.Root.AddText($"* n: {sub.Items.Length}\n* Threshold: {sub.CurrentParams.Threshold:N0}");
                    
                    ChartSections.Add(linker, sub, subPage);
                }
            }
        }
    }
}