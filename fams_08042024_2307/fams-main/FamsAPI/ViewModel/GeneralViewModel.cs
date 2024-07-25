using DataLayer.Entities;

namespace FamsAPI.ViewModel
{
    public class GeneralViewModel
    {
        public int Duration { get; set; }
        public virtual LearningObjective? LearningObjective { get; set; }
    }
}
