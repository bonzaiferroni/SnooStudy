using System;

namespace Bonwerk.SnooStudy
{
    public static class DistRecipes
    {
        public static Func<double, bool> ZeroOrAbove { get; } = x => x >= 0;
        
        public static DistRecipe[] Recipes { get; } = new[]
        {
            new DistRecipe("Outcome", ZeroOrAbove, x => x.Outcome),
            new DistRecipe("Predicted", ZeroOrAbove, x => Math.Clamp(x.Predicted, 0, 200000)),
            new DistRecipe("Comments", ZeroOrAbove, x => x.RawData.CommentCount),
            new DistRecipe("Self Text Length", x => x > 0, x => x.RawData.SelfTextLength),
            
            new DistRecipe("Score 12min", ZeroOrAbove, x => x.RawData.Score2),
            new DistRecipe("Score 30min", ZeroOrAbove, x => x.RawData.Score4),
            new DistRecipe("Score 42min", ZeroOrAbove, x => x.RawData.Score7),
            new DistRecipe("Score 60min", ZeroOrAbove, x => x.RawData.Score9),
        };
    }
}