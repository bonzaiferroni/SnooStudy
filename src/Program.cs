using System.IO;
using Bonwerk.Markdown;

namespace Bonwerk.SnooStudy
{
    class Program
    {
        public const string DocsPath = "../../../docs";
        
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

            var overview = document.FindSection("Overview");
            overview.Children.Clear();

            var table = new Table();
            table.AddColumn("Procedural");
            table.AddColumn("Table", TextAlignment.Center);
            table.AddColumn("Generation", TextAlignment.Right);
            table.AddRow("This", "seems", "to");
            table.AddRow("work", "just", "fine", "cool");
            overview.AddContent(table);
            
            var writer = new Writer();
            File.WriteAllText(path, writer.Write(document));
        }
    }
}