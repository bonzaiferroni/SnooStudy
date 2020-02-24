using System;

namespace Bonwerk.SnooStudy
{
    public class DistRecipe
    {
        public DistRecipe(string title, Func<double, bool> isValid, Func<StudyItem, double> getter)
        {
            Title = title;
            IsValid = isValid;
            Getter = getter;
        }

        public string Title { get; }
        public Func<double, bool> IsValid { get; }
        public Func<StudyItem, double> Getter { get; }
    }
}