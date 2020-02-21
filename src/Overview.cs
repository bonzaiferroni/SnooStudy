using System.Collections.Generic;
using Bonwerk.Archiving;
using Bonwerk.Markdown;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    public static class Overview
    {
        public static void Add(Document home, SubData data)
        {
            var overview = home.Root.FindSection("Overview");
            overview.Children.Clear();

            foreach (var subKvp in data)
            {
                var subName = subKvp.Key;
                var section = overview.AddSection(subName);
                
                var table = ContentHelper.GetParamsTable(subKvp.Value);
                
                section.AddContent(table);
            }
        }
    }
}