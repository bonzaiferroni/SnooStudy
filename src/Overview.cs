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
                
                var table = ContentHelper.GetParamsTable(scope);
                
                section.AddContent(table);
            }
        }
    }
}