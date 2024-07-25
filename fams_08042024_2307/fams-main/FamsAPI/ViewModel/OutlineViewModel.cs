using DataLayer.Entities;

namespace FamsAPI.ViewModel
{
    public class OutlineViewModel
    {
        public string? TrainingMaterials { get; set; }
        public virtual ICollection<TrainingUnit>? TrainingUnits { get; set; }
    }
}
