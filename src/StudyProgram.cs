using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bonwerk.Archiving;
using Bonwerk.Markdown;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    class StudyProgram
    {
        public const string DocsPath = "../../../docs";
        public static string DataPath => $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Study";
        
        private static void Main(string[] args)
        {
            CreatePages();
        }

        private static void CreatePages()
        {
            var scopes = LoadData();

            var linker = new FileLinker(DocsPath);
            linker.LoadPage("index.md");
            
            Overview.Add(linker, scopes);
            SubPages.Add(linker, scopes);

            linker.SaveAll();
        }

        private static ScopeData[] LoadData()
        {
            var scopes = new List<ScopeData>();
            
            var scopeNames = new[] {"guess", "hunch"};

            foreach (var subName in SpyProcess.Subs)
            {
                var archive = new PostArchive($"{subName}.archive", $"{DataPath}/data");
                var items = archive.GetItems();
                items = items.OrderBy(x => x.Created).ToList();
                
                foreach (var scopeName in scopeNames)
                {
                    var scope = scopes.FirstOrDefault(x => x.Name == scopeName);
                    if (scope == null)
                    {
                        scope = new ScopeData(scopeName);
                        scopes.Add(scope);
                    }
                    
                    var prams = Memorizer.Load<ModelParams>($"{subName}.{scopeName}.params", $"{DataPath}/models");

                    var studyItems = items.Select(x => new StudyItem(scopeName, x)).ToArray();
                    scope.Subreddits.Add(new SubData(subName, scopeName, studyItems, prams));
                }
            }

            return scopes.ToArray();
        }
    }
}