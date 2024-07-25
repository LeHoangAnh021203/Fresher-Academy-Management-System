using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class SyllabusRepository : GenericRepository<Syllabus>
    {
        private readonly FAMSDBContext _context;
        public SyllabusRepository(FAMSDBContext context) : base(context)
        {
            _context = context;
        }
        public List<Syllabus> SearchSyllabusByKeyword(string keyword)
        {
            return _context.Syllabuses
                           .Where(s => s.TopicCode.ToLower().Contains(keyword.ToLower()) || s.TopicName.ToLower().Contains(keyword.ToLower()))
                           .Include(s => s.TrainingUnits)
                                .ThenInclude(tu => tu.TrainingContents)
                                    .ThenInclude(tc => tc.LearningObjectives)
                           .Include(s => s.SyllabusObjectives)
                                .ThenInclude(so => so.LearningObjective)
                           .ToList();
        }

        public virtual Syllabus SearchSyllabusByTopicCode(string keyword)
        {
            return _context.Syllabuses
                           .Where(s => s.TopicCode == keyword).FirstOrDefault();
        }

        public virtual List<Syllabus> SearchSyllabusTechnicalGroup(string technicalGroup)
        {
            return _context.Syllabuses.Where(s => s.TopicCode.StartsWith(technicalGroup.Substring(0, 1))).ToList();
        }

        public List<Syllabus> SearchSyllabusByCreatedDate(string CreatedDate)
        {
            DateTime createdDateTime;
            if (DateTime.TryParse(CreatedDate, out createdDateTime))
            {
                return _context.Syllabuses
                               .Where(s => s.CreatedDate.Date == createdDateTime.Date)
                               .Include(s => s.TrainingUnits)
                                    .ThenInclude(tu => tu.TrainingContents)
                                        .ThenInclude(tc => tc.LearningObjectives)
                               .Include(s => s.SyllabusObjectives)
                                    .ThenInclude(so => so.LearningObjective)
                               .ToList();
            }
            else
            {
                // Handle the case where the date string is not in a valid format
                return null;
            }
        }
        public virtual List<Syllabus> getAllSyllabus()
        {
            return _context.Syllabuses
                           .Include(s => s.TrainingUnits)
                                .ThenInclude(tu => tu.TrainingContents)
                                    .ThenInclude(tc => tc.LearningObjectives)
                           .Include(s => s.SyllabusObjectives)
                                .ThenInclude(so => so.LearningObjective)
                           .OrderByDescending(s => s.CreatedDate).ToList();
        }

        public virtual Syllabus GetByKeyword(string key)
        {
            return _context.Syllabuses
                .Include(tu => tu.TrainingUnits)
                .ThenInclude(tc => tc.TrainingContents)
                .ThenInclude(lo => lo.LearningObjectives)
                .ThenInclude(so => so.SyllabusObjectives)
                .ThenInclude(s => s.Syllabus).FirstOrDefault(s => s.TopicCode.ToLower().Contains(key.ToLower()) || s.TopicName.ToLower().Contains(key.ToLower()));
        }
        public virtual Syllabus GetSyllabusByTopicCode(string topicCode)
        {
            return _context.Syllabuses.FirstOrDefault(s => s.TopicCode == topicCode);
        }

    }
}
