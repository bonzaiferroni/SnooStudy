using System;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    public class StudyItem
    {
        public const int PopularityThreshold = 10000;
        
        public StudyItem(string study, ArchiveItem rawData)
        {
            RawData = rawData;
            Predicted = study == ProphetStrings.Hunch ? rawData.HunchScore : rawData.GuessScore;
            RSquared = study == ProphetStrings.Hunch ? rawData.HunchRSquared : rawData.GuessRSquared;
            N = study == ProphetStrings.Hunch ? rawData.HunchN : rawData.GuessN;
            Trainer = study == ProphetStrings.Hunch ? rawData.HunchTrainer : rawData.GuessTrainer;
            FeatureSet = study == ProphetStrings.Hunch ? rawData.HunchFeatures : rawData.GuessFeatures;
            IsNotable = study == ProphetStrings.Hunch ? rawData.IsHunchNotable : rawData.IsGuessNotable;
        }
        
        public int Predicted { get; }
        public float RSquared { get; }
        public int N { get; }
        public string Trainer { get; }
        public string FeatureSet { get; }
        public bool IsNotable { get; }
        public ArchiveItem RawData { get; }
        
        public DateTime Created => RawData.Created;
        public DateTime Recorded => RawData.Created + TimeSpan.FromSeconds(RawData.AgeAtOutcome);
        public int Outcome => RawData.OutcomeScore;

        public bool IsAccurate()
        {
            var moe = (float) Math.Max(Outcome, 1000) / 5;
            return Predicted > Outcome - moe && Predicted < Outcome + moe;
        }

        public bool IsPopular => Outcome > PopularityThreshold;
        public bool PredictedPopular => Predicted > PopularityThreshold;

        public bool IsHit => IsPopular && PredictedPopular;
        public bool IsHype => !IsPopular && PredictedPopular;
    }
}