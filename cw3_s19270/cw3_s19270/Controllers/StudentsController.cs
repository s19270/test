using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using cw3_s19270.DAL;
using cw3_s19270.Models;
using cw3_s19270.Properties.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw3_s19270.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog = s19270; Integrated Security = True";
        private IStudentDal _dbService;

        public StudentsController(IStudentDal dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudents(string indexNumber)
        {
            var list = new List<Student>();
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from students where indexnumber = @index";

                com.Parameters.AddWithValue("index", indexNumber);
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    list.Add(st);
                }
            }

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Malewski");
            }
            return NotFound("Nie znaleziono studenta");
        }
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 2000)}";
            return Ok(student);
        }
        [HttpGet("ex2")]
        public IActionResult GetStudents2([FromServices] IStudentDal dbService)
        {
            var list = new List<Student>();
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "TestProc";
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("LastName", "Kowalski");
                var dr = com.ExecuteReader();
            }
            return NotFound();
        }
        [HttpGet("ex3")]
        public IActionResult GetStudents3()
        {
            var list = new List<Student>();
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "insert into Student (IndexNumber, FirstName, LastName) values ('s19000', 'Jan', 'Kowalski');";
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    int affectedRows = com.ExecuteNonQuery();
                    com.CommandText = "UPDATE Student, SET BirthDate = '2000-01-01' WHERE IndexNumber = 's19000';";
                    com.ExecuteNonQuery();
                    trans.Commit();
                }catch(Exception e)
                {
                    trans.Rollback();
                }
            }
            return Ok();
        }
    }
}