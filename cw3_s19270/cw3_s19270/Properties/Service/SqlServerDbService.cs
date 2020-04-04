using cw3_s19270.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cw3_s19270.Properties.Service
{
    public class SqlServerDbService : IStudentDbService
    {
        public void EnrollStudent(EnrollmentsRequest request)
        {
            var st = new Student();
            st.FirstName = request.FirstName;
            
            using (var con = new SqlConnection(""))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    com.CommandText = "select IdStudies from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                    }
                    int idstudies = (int)dr["IdStudies"];
                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName) VALUES(@Index, @Fname)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    com.ExecuteNonQuery();

                    tran.Commit();

                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }
            }

        }

        public void PromoteStudents(int semester, string studies)
        {
            using (var con = new SqlConnection(""))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    com.CommandText = "select IdStudies from studies where name=@name";
                    com.Parameters.AddWithValue("name", studies);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                    }
                    int idstudies = (int)dr["IdStudies"];
                    com.CommandText = "UPDATE Enrollments set Semester = @semestr where IdStudy = @id";
                    com.Parameters.AddWithValue("semestr", (semester + 1));
                    com.Parameters.AddWithValue("id", idstudies);
                    com.ExecuteNonQuery();

                    tran.Commit();

                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }
            }
        }
    }
}
