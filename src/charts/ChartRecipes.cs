using System;

namespace Bonwerk.SnooStudy
{
    public static class ChartRecipes
    {
        public static Func<StudyItem, bool> Always { get; } = x => true;
        
        public static ChartRecipe[] Correlation { get; } = new[]
        {
            new ChartRecipe("Predicted", x => x.Predicted >= 0, x => Math.Clamp(x.Predicted, 0, 200000)),
            new ChartRecipe("Comments", Always, x => x.RawData.CommentCount),
            new ChartRecipe("Self Text Length", x => x.RawData.SelfTextLength > 0, x => x.RawData.SelfTextLength),
            new ChartRecipe("Gild Score", Always, x => x.RawData.GildScore),
            new ChartRecipe("Subscribers", x => x.RawData.Subscribers > 0, x => x.RawData.Subscribers),
            new ChartRecipe("Crossposts", x => x.RawData.CrosspostCount >= 0, x => x.RawData.CrosspostCount),
            
            new ChartRecipe("Buzz 12min", Always, x => x.EncodedData.Buzz2),
            new ChartRecipe("Buzz 30min", Always, x => x.EncodedData.Buzz4),
            new ChartRecipe("Buzz 42min", Always, x => x.EncodedData.Buzz7),
            new ChartRecipe("Buzz 60min", Always, x => x.EncodedData.Buzz9),
            
            new ChartRecipe("Slump", Always, x => x.EncodedData.Slump),
            new ChartRecipe("Users0to9", x => x.EncodedData.Users0 > 0, x => x.EncodedData.Users0to9),
            new ChartRecipe("ActivityAvg0", x => x.RawData.Activity0 >= 0, x => x.EncodedData.ActivityAvg0),
            new ChartRecipe("ActivityAvg1", x => x.RawData.Activity0 >= 0, x => x.EncodedData.ActivityAvg1),
            new ChartRecipe("Pos9", x => x.EncodedData.Pos9 > 0, x => x.EncodedData.Pos9),
            new ChartRecipe("Users9", x => x.EncodedData.Users9 > 0, x => x.EncodedData.Users9),
        };
        
        public static ChartRecipe[] Distributions { get; } = new[]
        {
            new ChartRecipe("Outcome", Always, x => x.Outcome),
            new ChartRecipe("Predicted", x => x.Predicted >= 0, x => Math.Clamp(x.Predicted, 0, 200000)),
            new ChartRecipe("Comments", Always, x => x.RawData.CommentCount),
            new ChartRecipe("Self Text Length", x => x.RawData.SelfTextLength > 0, x => x.RawData.SelfTextLength),
            
            new ChartRecipe("Score 12min", Always, x => x.RawData.Score2),
            new ChartRecipe("Score 30min", Always, x => x.RawData.Score4),
            new ChartRecipe("Score 42min", Always, x => x.RawData.Score7),
            new ChartRecipe("Score 60min", Always, x => x.RawData.Score9),
        };
    }
}