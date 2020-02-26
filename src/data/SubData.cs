using System.Collections.Generic;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    public class SubData
    {
        public SubData(string name, string scope, ModelParams currentParams, StudyItem[] items)
        {
            Name = name;
            Scope = scope;
            CurrentParams = currentParams;
            Items = items;
        }
        
        public string Name { get; }
        public string Scope { get; }
        public ModelParams CurrentParams { get; }
        public StudyItem[] Items { get; }
        public string RName => $"r/{Name}";

        public List<ModelData> Models { get; } = new List<ModelData>();
    }
}