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
            var data = LoadData();

            var linker = new FileLinker(DocsPath);
            linker.LoadPage("index.md");
            
            Overview.Add(linker, data);
            SubPages.Add(linker, data);

            linker.SaveAll();
        }

        private static SubData LoadData()
        {
            var subData = new SubData();
            
            var studies = new[] {"hunch", "guess"};

            foreach (var sub in SpyProcess.Subs)
            {
                subData[sub] = new Dictionary<string, StudyData>();
                var archive = new PostArchive($"{sub}.archive", $"{DataPath}/data");
                var items = archive.GetItems();
                
                foreach (var study in studies)
                {
                    var prams = Memorizer.Load<ModelParams>($"{sub}.{study}.params", $"{DataPath}/models");

                    var studyItems = items.Select(x => new StudyItem(study, x)).ToArray();
                    subData[sub][study] = new StudyData(studyItems, prams);
                }
            }

            return subData;
        }
    }
}