using System;
using System.Collections.Generic;
using System.IO;
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

            var home = ParseHelper.ParsePath($"{DocsPath}/index.md");
            Overview.Add(home, data);
            SubPages.Add(home, data);
            
            home.Save();
        }

        private static SubData LoadData()
        {
            var subData = new SubData();
            
            var studies = new[] {"hunch", "guess"};

            foreach (var sub in SpyProcess.Subs)
            {
                subData[sub] = new Dictionary<string, StudyData>();
                foreach (var study in studies)
                {
                    var prams = Memorizer.Load<ModelParams>($"{sub}.{study}.params", $"{DataPath}/models");
                    var archive = new PostArchive($"{sub}.archive", $"{DataPath}/data");
                    subData[sub][study] = new StudyData(archive.GetItems(), prams);
                }
            }

            return subData;
        }
    }
}