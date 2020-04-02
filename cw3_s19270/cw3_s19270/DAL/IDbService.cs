using cw3_s19270.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3_s19270.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
