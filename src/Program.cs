using System;
using System.IO;

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
            File.WriteAllText($"{DocsPath}/index.md", "# Snoo Study \nStay a while, and listen.");
        }
    }
}