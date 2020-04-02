using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3_s19270.Models
{
    public interface IStudentDbService
    {
        void EnrollStudent(EnrollmentsRequest request);
        void PromoteStudents(int semester, string studies);
    }
}
