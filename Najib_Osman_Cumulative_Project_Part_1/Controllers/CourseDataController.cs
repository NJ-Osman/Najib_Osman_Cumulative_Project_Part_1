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
    public class CourseDataController : ApiController
    {
        private TeacherDbContext SchoolDb = new TeacherDbContext();

        //This Controller Will access the Courses table of our School database.
        /// <summary>
        /// Returns a list of Courses in the system
        /// Returns course start date and finish date if user clicks on the course name
        /// </summary>
        /// <example>GET api/CourseData/ListCourses</example>
        /// <returns>
        /// A list of Courses (Course Names)
        /// List of course name, course startdate and finish date
        /// </returns>

        [HttpGet]
        [Route("api/CourseData/listCourses/{SearchKey?}")]
        public IEnumerable<Course> ListCourses(string SearchKey = null)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from Classes where  lower(classid) like lower(@key) or lower(classname) like lower(@key) " +
                "or lower(concat(classid, ' ', classname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Course> Courses = new List<Course> { };

            while (ResultSet.Read())
            {
                int CourseId = Convert.ToInt32(ResultSet["classid"]);
                string CourseName = ResultSet["classname"].ToString();
                DateTime StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                DateTime FinishDate = Convert.ToDateTime(ResultSet["finishdate"]);

                Course newCourse = new Course();
                newCourse.CourseId = CourseId;
                newCourse.CourseName = CourseName;
                newCourse.StartDate = StartDate;
                newCourse.FinishDate = FinishDate;
                Courses.Add(newCourse);
            }

            Conn.Close();

            return Courses;
        }

        [HttpGet]
        public Course FindCourse(int id)
        {
            Course newCourse = new Course();

            MySqlConnection Conn = SchoolDb.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from Classes where classid =" + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int CourseId = Convert.ToInt32(ResultSet["classid"]);
                string CourseName = ResultSet["classname"].ToString();
                DateTime StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                DateTime FinishDate = Convert.ToDateTime(ResultSet["finishdate"]);

                newCourse.CourseId = CourseId;
                newCourse.CourseName = CourseName;
                newCourse.StartDate = StartDate;
                newCourse.FinishDate = FinishDate;
            }


            return newCourse;

        }
    }
}
