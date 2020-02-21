using System;
using System.IO;
using Bonwerk.Archiving;
using Bonwerk.Markdown;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    class Program
    {
        public const string DocsPath = "../../../docs";
        public static string DataPath => $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Study";
        
        private static void Main(string[] args)
        {
            CreateHome();
        }

        private static void CreateHome()
        {
            var path = $"{DocsPath}/index.md";
            var str = File.ReadAllText(path);
            var parser = new Parser();
            var document = parser.Read(str);

            AddOverview(document);

            var writer = new Writer();
            File.WriteAllText(path, writer.Write(document));
        }

        private static void AddOverview(Section document)
        {
            var overview = document.FindSection("Overview");
            overview.Children.Clear();

            var studies = new[] {"hunch", "guess"};

            foreach (var subName in SpyProcess.Subs)
            {
                var section = overview.AddSection(subName);
                
                var table = new Table();
                table.AddColumns(TextAlignment.Left, "Study", "Trainer Name", "Feature Set");
                table.AddColumns(TextAlignment.Right, "n", "R²");
                
                foreach (var study in studies)
                {
                    var prams = Memorizer.Load<ModelParams>($"{subName}.{study}.params", $"{DataPath}/models");
                    var trainer = prams.TrainerName.Replace("Regression", "");
                    table.AddRow(study, trainer, prams.FeatureSetName, prams.N.ToString("N0"), prams.RSquared.ToString("N2"));
                }
                section.AddContent(table);
            }
        }
    }
}