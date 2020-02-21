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
            // var document = new Document();
            // document.Heading = new Heading(1, "Welcome");
            // document.Children.Add(new TextContent("Stay awhile, and listen"));
            //
            // var section = document.AddSection("Section 1");
            // section.AddParagraph("Content for section 1");
            //
            // section = document.AddSection("Section 2");
            // section.AddParagraph("Content for section 2");
            var path = $"{DocsPath}/index.md";
            var str = File.ReadAllText(path);
            var parser = new Parser();
            var document = parser.Read(str);
            
            var writer = new Writer();
            File.WriteAllText(path, writer.Write(document));
        }
    }
}