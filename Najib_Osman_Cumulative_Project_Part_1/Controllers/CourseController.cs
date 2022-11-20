using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Najib_Osman_Cumulative_Project_Part_1.Models;

namespace Najib_Osman_Cumulative_Project_Part_1.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        //Get:Course/List
        public ActionResult List(string SearchKey = null)
        {
            CourseDataController Controller = new CourseDataController();
            IEnumerable<Course> Courses = Controller.ListCourses(SearchKey);
            return View(Courses);
        }

        //GET:Course/Show
        public ActionResult Show(int id)
        {
            CourseDataController Controller = new CourseDataController();
            Course newCourse = Controller.FindCourse(id);
            return View(newCourse);
        }
    }
}