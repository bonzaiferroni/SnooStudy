using System;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    public class StudyItem
    {
        public StudyItem(string study, ArchiveItem rawData, LoggedItem encoded, int threshold)
        {
            RawData = rawData;
            EncodedData = encoded;
            Predicted = study == ProphetStrings.Hunch ? rawData.HunchScore : rawData.GuessScore;
            if (float.IsNaN(Predicted)) Predicted = -1;
            RSquared = study == ProphetStrings.Hunch ? rawData.HunchRSquared : rawData.GuessRSquared;
            N = study == ProphetStrings.Hunch ? rawData.HunchN : rawData.GuessN;
            Trainer = study == ProphetStrings.Hunch ? rawData.HunchTrainer : rawData.GuessTrainer;
            FeatureSet = study == ProphetStrings.Hunch ? rawData.HunchFeatures : rawData.GuessFeatures;
            IsNotable = study == ProphetStrings.Hunch ? rawData.IsHunchNotable : rawData.IsGuessNotable;

            Threshold = threshold;

            Scores = new[]
            {
                rawData.Score0,
                rawData.Score1,
                rawData.Score2,
                rawData.Score3,
                rawData.Score4,
                rawData.Score5,
                rawData.Score6,
                rawData.Score7,
                rawData.Score8,
                rawData.Score9,
            };
        }
        
        public int Predicted { get; }
        public float RSquared { get; }
        public int N { get; }
        public string Trainer { get; }
        public string FeatureSet { get; }
        public bool IsNotable { get; }
        public ArchiveItem RawData { get; }
        public LoggedItem EncodedData { get; }
        
        public int Threshold { get; }
        
        public DateTime Created => RawData.Created;
        public DateTime Recorded => RawData.Created + TimeSpan.FromSeconds(RawData.AgeAtOutcome);
        public int Outcome => RawData.OutcomeScore;
        
        public int[] Scores { get; }

        public bool IsAccurate()
        {
            var moe = (float) Math.Max(Outcome, 1000) / 5;
            return Predicted > Outcome - moe && Predicted < Outcome + moe;
        }

        public bool IsTop => Outcome > Threshold;
        public bool PredictedTop => Predicted > Threshold;

        public bool IsHit => IsTop && PredictedTop;
        public bool IsHype => !IsTop && PredictedTop;
    }
}