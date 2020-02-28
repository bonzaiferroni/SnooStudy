using System;

namespace Bonwerk.SnooStudy
{
    public class ChartRecipe
    {
        public ChartRecipe(string title, Func<StudyItem, bool> isValid, Func<StudyItem, double> getter)
        {
            Title = title;
            IsValid = isValid;
            Getter = getter;
        }

        public string Title { get; }
        public Func<StudyItem, bool> IsValid { get; }
        public Func<StudyItem, double> Getter { get; }
    }
}