using cw3_s19270.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3_s19270.DAL
{
    public class MockDBService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDBService()
        {
            _students = new List<Student>
            {
                new Student{IdStudent = 1, FirstName = "Jan", LastName = "Kowalski"},
                new Student { IdStudent = 1, FirstName = "Robert", LastName = "Lewandowski" },
                new Student { IdStudent = 1, FirstName = "Geralt", LastName = "Riv" }
            };
        }
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
