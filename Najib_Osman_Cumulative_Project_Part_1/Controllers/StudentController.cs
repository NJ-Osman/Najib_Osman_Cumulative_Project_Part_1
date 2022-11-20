using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Najib_Osman_Cumulative_Project_Part_1.Models;

namespace Najib_Osman_Cumulative_Project_Part_1.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        //GET: Student/List
        public ActionResult List(string SearchKey = null)
        {
            StudentDataController Controller = new StudentDataController();
            IEnumerable<Student> Students = Controller.ListStudents(SearchKey);
            return View(Students);
        }

        //Get:Student/Show
        public ActionResult Show(int id)
        {
            StudentDataController Controller = new StudentDataController();
            Student newStudent = Controller.FindStudent(id);
            return View(newStudent);
        }

    }
}