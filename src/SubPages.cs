using System;
using System.Linq;
using Bonwerk.Markdown;

namespace Bonwerk.SnooStudy
{
    public static class SubPages
    {
        public static void Add(FileLinker linker, ScopeData[] scopes)
        {
            var home = linker.GetPage("index");
            
            foreach (var scope in scopes)
            {
                foreach (var sub in scope.Subreddits)
                {
                    var subPage = linker.CreatePage(sub.RName, $"subs/{scope.Name}_{sub.Name}.md");
                    subPage.Root.AddText(linker.LinkPage("Home", subPage, home));

                    var section = subPage.Root.AddSection("Models");

                    var table = new Table();
                    table.AddColumns(TextAlignment.Left, "Model", "Trainer");
                    table.AddColumns(TextAlignment.Right, "n", "R²");
                    
                    foreach (var model in sub.Models)
                    {
                        var modelPage = linker.CreatePage($"{sub.RName}: {model.Name}",
                            $"subs/models/{scope.Name}_{sub.Name}_{model.Name}.md");
                        var modelLinkText = linker.LinkPage(model.Name, subPage, modelPage);
                        table.AddRow(modelLinkText, model.ModelParams.TrainerName, model.ModelParams.N.ToString("N0"),
                            model.ModelParams.RSquared.ToString("N2"));

                        modelPage.Root.AddText(linker.LinkPage("Home", modelPage, home));
                        modelPage.Root.AddText(linker.LinkPage($"Back ({sub.RName})", modelPage, subPage));
                        
                        ModelCharts.Add(linker, model, modelPage);
                    }
                    
                    section.AddContent(table);

                    SubCharts.Add(linker, subPage, sub);
                }
            }
        }
    }
}