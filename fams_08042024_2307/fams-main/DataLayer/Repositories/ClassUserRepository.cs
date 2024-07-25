using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class ClassUserRepository : GenericRepository<ClassUser>
    {
        public ClassUserRepository(FAMSDBContext context) : base(context) { }    
    }
}
