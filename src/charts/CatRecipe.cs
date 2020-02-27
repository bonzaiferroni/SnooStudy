using System;

namespace Bonwerk.SnooStudy
{
    public class CatRecipe
    {
        public CatRecipe(string title, SortType sort, Func<StudyItem, object> getter)
        {
            Title = title;
            Sort = sort;
            Getter = getter;
        }

        public string Title { get; }
        public SortType Sort { get; }
        public Func<StudyItem, object> Getter { get; }

        public enum SortType
        {
            Natural,
            Average,
            Count
        }
    }
}