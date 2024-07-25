using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using static DataLayer.Entities.TrainingProgram;

namespace DataLayer.Repositories
{
    public class TrainingProgramRepository : GenericRepository<TrainingProgram>
    {
        private readonly FAMSDBContext _context;
        public TrainingProgramRepository(FAMSDBContext context) : base(context)
        {
            _context = context;
        }

        public List<TrainingProgram> GetTrainingProgramsByKeyword(string key)
        {
            return _context.TrainingPrograms.Where(p => p.TrainingProgramCode.ToUpper().Contains(key.ToUpper()) || p.Name.ToUpper().Contains(key.ToUpper())).ToList();
        }

        public List<TrainingProgram> GetTrainingProgramsByFilter(string? createBy, string? createDate, int? duration, Statuses? status)
        {
            return _context.TrainingPrograms.Where(p =>
        (createBy == null || p.CreateBy.ToUpper().Equals(createBy.ToUpper())) &&
        (createDate == null || (p.CreateDate == DateTime.Parse(createDate).Date)) &&
        (duration == null || p.Duration == duration) &&
        (status == null || p.Status == status))
            .ToList();
        }

        public virtual TrainingProgram GetTrainingProgrambyTrainingCode(string trainingCode)
        {
            return _context.TrainingPrograms
                .Include(tp => tp.TrainingProgramSyllabuses)
                    .ThenInclude(tps => tps.Syllabus)
                        .ThenInclude(s => s.TrainingUnits)
                            .ThenInclude(x => x.TrainingContents)
                .FirstOrDefault(tp => tp.TrainingProgramCode.ToLower() == trainingCode.ToLower());
        }
    }
}
