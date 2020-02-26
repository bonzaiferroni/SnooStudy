using System.Collections.Generic;
using Bonwerk.Markdown;

namespace Bonwerk.SnooStudy
{
    public class ScopeData
    {
        public ScopeData(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public List<SubData> Subreddits { get; } = new List<SubData>();
    }
}