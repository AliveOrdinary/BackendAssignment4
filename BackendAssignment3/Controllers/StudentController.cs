using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackendAssignment3.Models;

namespace BackendAssignment3.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/List

        /// <summary>
        /// This controller will list all the students in the database
        /// <example>GET: /Student/List</example>
        /// </summary>
        /// <returns>
        /// returns a list of students
        /// </returns>
        public ActionResult List(string SearchKey)
        {
            // Instantiate the data controller
            StudentDataController controller = new StudentDataController();

            // Get the list of students
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);

            return View(Students);
        }

        // GET: Student/Show/{id}

        /// <summary>
        /// This controller will show the details of a student based on the id
        /// <example>GET: /Student/Show/1</example>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// returns details of a student
        /// </returns>

        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student SelectedStudent = controller.FindStudent(id);

            return View(SelectedStudent);
        }

        // GET: Student/DeleteConfirm/{id}

        /// <summary>
        /// This controller will show the details of a student based on the id for deletion
        /// </summary>
        /// 
        /// <param name="id"></param>

        public ActionResult DeleteConfirm(int id) {
            StudentDataController controller = new StudentDataController();
            Student SelectedStudent = controller.FindStudent(id);

            return View(SelectedStudent);
        }

        // POST: Student/Delete/{id}

        /// <summary>
        /// This controller will delete a student from the database
        /// </summary>
        /// 
        /// <param name="id"></param>
        /// 

        [HttpPost]
        public ActionResult Delete(int id)
        {
            StudentDataController controller = new StudentDataController();
            controller.DeleteStudent(id);

            return RedirectToAction("List");
        }

        // GET: Student/New

        /// <summary>
        /// This controller will show the form to create a new student
        /// </summary>
        /// <returns></returns>

        public ActionResult New()
        {
            return View();
        }

        // POST: Student/Create

        /// <summary>
        /// This controller will create a new student in the database
        /// </summary>
        /// <param name="StudentFname"></param>
        /// <param name="StudentLname"></param>
        /// <param name="StudentNumber"></param>
        /// <param name="EnrollDate"></param>
        /// <returns></returns>

        public ActionResult Create( string StudentFname, string StudentLname, string StudentNumber, DateTime EnrollDate)
        {
            Student NewStudent = new Student();
            NewStudent.StudentFname = StudentFname;
            NewStudent.StudentLname = StudentLname;
            NewStudent.StudentNumber = StudentNumber;
            NewStudent.EnrollDate = EnrollDate;

            StudentDataController controller = new StudentDataController();
            controller.AddStudent(NewStudent);

            return RedirectToAction("List");
        }
    }
}