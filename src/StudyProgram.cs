using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bonwerk.Archiving;
using Bonwerk.LearningML.Auto;
using Bonwerk.Markdown;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    class StudyProgram
    {
        public const string DocsPath = "../../../docs";
        public const string SourcePath = "C:/Users/Luke/Desktop/AppData/Sputnik/RedditSpy";
        
        private static void Main(string[] args)
        {
            CreatePages();
        }

        private static void CreatePages()
        {
            var scopes = LoadData();

            var linker = new FileLinker(DocsPath);
            linker.LoadPage("index.md");
            
            SubPages.Add(linker, scopes);
            Overview.Add(linker, scopes);

            linker.SaveAll();
        }

        private static ScopeData[] LoadData()
        {
            var scopes = new List<ScopeData>();
            
            var scopeNames = new[] {ProphetStrings.Guess, ProphetStrings.Hunch};
            var subs = SpyProcess.Subs;
            //var subs = new[] {"politics"};

            var encoder = new PostEncoder();

            foreach (var subName in subs)
            {
                var archive = new PostArchive($"{subName}.archive", $"{SourcePath}/data");
                var items = archive.GetItems().OrderBy(x => x.Created).ToList();
                
                foreach (var scopeName in scopeNames)
                {
                    var scope = scopes.FirstOrDefault(x => x.Name == scopeName);
                    if (scope == null)
                    {
                        scope = new ScopeData(scopeName);
                        scopes.Add(scope);
                    }

                    var generalParams = Memorizer.Load<ModelParams>($"{subName}.{scopeName}.params",
                        $"{SourcePath}/models");
                    if (generalParams.Threshold == 0) 
                        generalParams.Threshold = (int) AutoProgram.FindThreshold(items, x => x.OutcomeScore);
                    var studyItems = items.Select(x => new StudyItem(scopeName, x, encoder.EncodeLogged(x), generalParams.Threshold)).ToArray();
                    
                    var sub = new SubData(subName, scopeName, generalParams, studyItems);
                    scope.Subreddits.Add(sub);
                    
                    // add general model
                    sub.Models.Add(new ModelData("General", subName, scopeName, studyItems, generalParams));

                    var groups = items
                        .GroupBy(x => scopeName == ProphetStrings.Guess ? x.GuessFeatures : x.HunchFeatures).Reverse();

                    const int modelCountMinimum = 100;
                    
                    foreach (var grouping in groups)
                    {
                        if (grouping.Count() < modelCountMinimum) continue;
                        var groupedItems = grouping.ToArray();

                        var first = groupedItems[0];

                        var modelName = scopeName == ProphetStrings.Guess ? first.GuessFeatures : first.HunchFeatures;
                        var rsq = scopeName == ProphetStrings.Guess ? first.GuessRSquared : first.HunchRSquared;
                        var trainerName = scopeName == ProphetStrings.Guess ? first.GuessTrainer : first.HunchTrainer;
                        var n = scopeName == ProphetStrings.Guess ? first.GuessN : first.HunchN;

                        var modelParams = new ModelParams()
                        {
                            N = n,
                            Threshold = generalParams.Threshold,
                            RSquared = rsq,
                            TrainerName = trainerName,
                            FeatureSetName = modelName,
                        };

                        var groupedStudyItems =
                            groupedItems.Select(x => new StudyItem(scopeName, x, encoder.EncodeLogged(x), 
                                generalParams.Threshold)).ToArray();
                        
                        sub.Models.Add(new ModelData(modelName, subName, scopeName, groupedStudyItems, modelParams));
                    }
                }
            }

            return scopes.ToArray();
        }
    }
}