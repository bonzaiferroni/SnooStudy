using System.Collections.Generic;
using Bonwerk.Markdown;

namespace Bonwerk.SnooStudy
{
    public static class ContentHelper
    {
        public static Table GetParamsTable(Dictionary<string, StudyData> studyData)
        {
            var table = new Table();
            table.AddColumns(TextAlignment.Left, "Study", "Trainer Name", "Feature Set");
            table.AddColumns(TextAlignment.Right, "n", "R²");

            foreach (var studyKvp in studyData)
            {
                var study = studyKvp.Key;
                var prams = studyKvp.Value.CurrentParams;
                var trainer = prams.TrainerName.Replace("Regression", "");
                table.AddRow(study, trainer, prams.FeatureSetName, prams.N.ToString("N0"), prams.RSquared.ToString("N2"));
            }

            return table;
        }
    }
}