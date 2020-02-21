using System.Collections.Generic;
using Bonwerk.RedditSpy;

namespace Bonwerk.SnooStudy
{
    public class StudyData
    {
        public StudyData(List<ArchiveItem> archive, ModelParams currentParams)
        {
            Archive = archive;
            CurrentParams = currentParams;
        }

        public List<ArchiveItem> Archive { get; }
        public ModelParams CurrentParams { get; }
    }
}