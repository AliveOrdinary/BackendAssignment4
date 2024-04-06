using BackendAssignment3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackendAssignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List

        /// <summary>
        /// This controller will list all the teachers in the database
        /// <example>GET: /Teachers/List</example>
        /// </summary>
        /// <returns>
        /// returns a list of teachers
        /// </returns>
        public ActionResult List(string SearchKey)
        {
            // Instantiate the data controller
            TeacherDataController controller = new TeacherDataController();

            // Get the list of teachers
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            return View(Teachers);
        }

        

        // GET: Teacher/Show/{id}

        /// <summary>
        /// This controller will show the details of a teacher based on the id
        /// <example>GET: /Teacher/Show/1</example>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Show(int id)
        {
            // Instantiate the data controller
            TeacherDataController controller = new TeacherDataController();


            // Get the teacher based on the id
            Teacher SelectedTeacher = controller.FindTeacher(id);

            // Get the classes taught by the teacher
           IEnumerable<Class> TeacherClasses = controller.FindClassesForTeacher(id);

            TeacherClassModel ViewModel = new TeacherClassModel
            {
                Teacher = SelectedTeacher,
                Classes = TeacherClasses
            };



            return View(ViewModel);
        }


        // GET: Teacher/DeleteConfirm/{id}

        /// <summary>
        /// Shows the details of the teacher to be deleted and asks for confirmation
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a view with the details of the teacher to be deleted</returns>

        public ActionResult DeleteConfirm(int id)
        {
            // Instantiate the data controller
            TeacherDataController controller = new TeacherDataController();


            // Get the teacher based on the id
            Teacher SelectedTeacher = controller.FindTeacher(id);

            // Get the classes taught by the teacher
            IEnumerable<Class> TeacherClasses = controller.FindClassesForTeacher(id);

            TeacherClassModel ViewModel = new TeacherClassModel
            {
                Teacher = SelectedTeacher,
                Classes = TeacherClasses
            };



            return View(ViewModel);
        }

        // POST: Teacher/Delete/{id}

        /// <summary>
        /// Deletes the teacher based on the id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Redirects to the list of teachers</returns>


        public ActionResult Delete(int id)
        {
            // Instantiate the data controller
            TeacherDataController controller = new TeacherDataController();

            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        // GET: Teacher/Add
        public ActionResult New()
        {
            return View();
        }

        // POST: Teacher/Create
        [HttpPost]

        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {

            // create a new teacher object
            Teacher NewTeacher = new Teacher();

            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            // Instantiate the data controller
            TeacherDataController controller = new TeacherDataController();

            controller.AddTeacher(NewTeacher);
            return RedirectToAction("List");
        }

    }
}