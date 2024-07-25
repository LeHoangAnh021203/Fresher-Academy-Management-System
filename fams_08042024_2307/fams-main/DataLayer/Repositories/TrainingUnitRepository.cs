using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class TrainingUnitRepository : GenericRepository<TrainingUnit>
    {
        private readonly FAMSDBContext _context;

        public TrainingUnitRepository(FAMSDBContext context) : base(context)
        {
            _context = context;
        }

        public List<TrainingUnit> GetTrainingUnitByTopicCode(string topicCode)
        {
            return _context.TrainingUnits.Where(x =>  x.TopicCode == topicCode).ToList();
            
        }
        public virtual TrainingUnit GetByKeyword(string key)
        {
            return _context.TrainingUnits
                    .Include(tc => tc.TrainingContents)
                    .ThenInclude(lo => lo.LearningObjectives)
                    .ThenInclude(so => so.SyllabusObjectives)
                    .ThenInclude(s => s.Syllabus).FirstOrDefault(s => s.TopicCode.ToLower().Contains(key.ToLower()) || s.Syllabus.TopicName.ToLower().Contains(key.ToLower()));
        }
    }
}
