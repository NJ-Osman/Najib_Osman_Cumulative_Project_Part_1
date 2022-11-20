using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using Najib_Osman_Cumulative_Project_Part_1.Models;
using System.Diagnostics;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Najib_Osman_Cumulative_Project_Part_1.Controllers
{
    public class TeacherController : Controller
    {
        //GET: /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> MyTeachers = Controller.ListTeachers(SearchKey);
            return View(MyTeachers);
        }

        //GET: /Teacher/Show
        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher newTeacher = Controller.FindTeacher(id);    
            return View(newTeacher); 
        }

    }
}