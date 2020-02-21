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
                
                var table = new Table();
                table.AddColumns(TextAlignment.Left, "Study", "Trainer Name", "Feature Set");
                table.AddColumns(TextAlignment.Right, "n", "R²");
                
                foreach (var studyKvp in subKvp.Value)
                {
                    var study = studyKvp.Key;
                    var prams = studyKvp.Value.CurrentParams;
                    var trainer = prams.TrainerName.Replace("Regression", "");
                    table.AddRow(study, trainer, prams.FeatureSetName, prams.N.ToString("N0"), prams.RSquared.ToString("N2"));
                }
                section.AddContent(table);
            }
        }
    }
}