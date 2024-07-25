using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class ClassRepository : GenericRepository<Class>
    {
        private readonly FAMSDBContext _context;

        public ClassRepository(FAMSDBContext context) : base(context)
        {
            _context = context;
        }
        public Class GetClassByTrainingCode(Func<Class, bool> predicates)
        {
            return _context.Classes
                .Include(c => c.GetFsu)
                .Include(c => c.GetLocation)
                .FirstOrDefault(predicates);
        }
    }
}
