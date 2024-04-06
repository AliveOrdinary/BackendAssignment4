using BackendAssignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackendAssignment3.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }



        // GET: Class/List

        /// <summary>
        /// This controller will list all the classes in the database
        /// <example>GET: /Class/List</example>
        /// </summary>
        /// <returns>
        /// Returns a list of classes
        /// </returns>
        public ActionResult List(string SearchKey)
        {
            // Instantiate the data controller
            ClassDataController controller = new ClassDataController();

            // Get the list of classes
            IEnumerable<Class> Classes = controller.ListClasses(SearchKey);

            return View(Classes);
        }


        // GET: Class/Show/{id}

        /// <summary>
        /// This controller will show the details of a class based on the id
        /// <example>GET: /Class/Show/1</example>
        /// </summary>
        /// <returns>
        /// Returns details of selected class 
        /// </returns>

        public ActionResult Show(int id)
        {
            // Instantiate the data controller
            ClassDataController controller = new ClassDataController();

            // Get the class based on the id
            Class SelectedClass = controller.FindClass(id);

            return View(SelectedClass);
        }
    }
}