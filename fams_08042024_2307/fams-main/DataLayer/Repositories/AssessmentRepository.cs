using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class AssessmentRepository : GenericRepository<Assessment>
    {
        public AssessmentRepository(FAMSDBContext context) : base(context) { }
    }
}
