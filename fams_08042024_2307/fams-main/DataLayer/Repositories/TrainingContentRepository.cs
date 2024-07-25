using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class TrainingContentRepository :GenericRepository<TrainingContent>
    {
        private readonly FAMSDBContext _context;
        public TrainingContentRepository(FAMSDBContext context) : base(context) 
        {
            _context = context;
        }
        public virtual List<TrainingContent> GetByUnitCode(string unitCode)
        {
            return _context.TrainingContents
                .Where(tc => tc.UnitCode == unitCode)
                .ToList();
        }
    }
}
