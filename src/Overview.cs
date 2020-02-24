using System.Collections.Generic;
using Bonwerk.Archiving;
using Bonwerk.Markdown;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    public static class Overview
    {
        public static void Add(FileLinker linker, ScopeData[] scopes)
        {
            var home = linker.GetPage("index");

            var overview = home.Root.FindSection("Overview");
            overview.Children.Clear();

            foreach (var scope in scopes)
            {
                var section = overview.AddSection(scope.Name);
                
                var table = GetParamsTable(linker, scope, home);
                
                section.AddContent(table);
            }
        }
        
        public static Table GetParamsTable(FileLinker linker, ScopeData scope, Document page)
        {
            var table = new Table();
            table.AddColumns(TextAlignment.Left, "Subreddit", "Trainer Name", "Feature Set");
            table.AddColumns(TextAlignment.Right, "n", "R²");

            foreach (var subreddit in scope.Subreddits)
            {
                var prams = subreddit.CurrentParams;
                var trainer = prams.TrainerName.Replace("Regression", "");
                var subredditText = linker.LinkPage(subreddit.Name, page, $"{subreddit.Scope}_{subreddit.Name}");
                table.AddRow(subredditText, trainer, prams.FeatureSetName, prams.N.ToString("N0"), prams.RSquared.ToString("N2"));
            }

            return table;
        }
    }
}