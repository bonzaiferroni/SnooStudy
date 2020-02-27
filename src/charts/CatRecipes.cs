namespace Bonwerk.SnooStudy
{
    public class CatRecipes
    {
        public static CatRecipe[] Recipes { get; } = 
        {
            new CatRecipe("Day", CatRecipe.SortType.Natural, x => x.RawData.DayOfWeek),
            new CatRecipe("MediaType", CatRecipe.SortType.Count, x => x.RawData.MediaType),
            new CatRecipe("Author", CatRecipe.SortType.Count, x => x.RawData.Author),
            new CatRecipe("Domain", CatRecipe.SortType.Count, x => x.RawData.Domain),
            new CatRecipe("Flair", CatRecipe.SortType.Count, x => x.RawData.Flair),
            new CatRecipe("IsNSFW", CatRecipe.SortType.Natural, x => x.RawData.IsNSFW),
            new CatRecipe("IsOriginalContent", CatRecipe.SortType.Natural, x => x.RawData.IsOriginalContent),
            new CatRecipe("IsSticky", CatRecipe.SortType.Natural, x => x.RawData.IsSticky),
        };
    }
}