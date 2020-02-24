using System.Collections.Generic;
using Bonwerk.Markdown;

namespace Bonwerk.SnooStudy
{
    public static class ContentHelper
    {
        public static Table GetParamsTable(ScopeData scope)
        {
            var table = new Table();
            table.AddColumns(TextAlignment.Left, "Subreddit", "Trainer Name", "Feature Set");
            table.AddColumns(TextAlignment.Right, "n", "R²");

            foreach (var subreddit in scope.Subreddits)
            {
                var prams = subreddit.CurrentParams;
                var trainer = prams.TrainerName.Replace("Regression", "");
                table.AddRow(subreddit.Name, trainer, prams.FeatureSetName, prams.N.ToString("N0"), prams.RSquared.ToString("N2"));
            }

            return table;
        }
    }
}