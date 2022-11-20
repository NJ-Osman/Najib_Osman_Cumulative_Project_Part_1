using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Najib_Osman_Cumulative_Project_Part_1.Models;
using MySql.Data.MySqlClient;
 using System.Security.Cryptography;

namespace Najib_Osman_Cumulative_Project_Part_1.Controllers
{
    public class StudentDataController : ApiController
    {

        private TeacherDbContext SchoolDb = new TeacherDbContext();

        //This Controller Will access the Students table of our School database.
        /// <summary>
        /// Returns a list of Students in the system
        /// Returns a list of Students name and their enrol date if the user clicks on the student name
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of Students (first names, last names, student numbers)
        /// A list of the student name, and student enrol date
        /// </returns>

        [HttpGet]
        [Route("api/StudentData/ListStudents/{SearchKey?}")]
        public IEnumerable<Student> ListStudents(string SearchKey=null)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from Students where  lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) " +
                "or lower(concat(studentfname, ' ', studentlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Student> Students = new List<Student>{};

            while(ResultSet.Read())
            {
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                DateTime StudentEnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentEnrolDate = StudentEnrolDate;

                Students.Add(NewStudent);
            }

            Conn.Close();

            return Students;

        }


        [HttpGet]
        public Student FindStudent(int id)
        {
            Student newStudent = new Student();

            MySqlConnection Conn = SchoolDb.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from Students where studentid =" + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                DateTime StudentEnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

                newStudent.StudentId = StudentId;
                newStudent.StudentFname = StudentFname;
                newStudent.StudentLname = StudentLname;
                newStudent.StudentEnrolDate = StudentEnrolDate;
            }


            return newStudent;
        }

    }
}
