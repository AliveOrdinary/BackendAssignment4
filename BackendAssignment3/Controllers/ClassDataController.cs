using BackendAssignment3.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackendAssignment3.Controllers
{
    public class ClassDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();


        /// <summary>
        /// This Method Will Return a List of Classes in the Database
        /// <example>GET api/ClassData/ListClasses</example>
        /// <returns>
        /// A list of Classes in the database (id, Class code, Class name, Class start date, Class end date, teacher id).
        /// </returns>
        /// 

        [HttpGet]
        [Route("api/ClassData/ListClasses/{SearchKey?}")]

        public IEnumerable<Class> ListClasses(string SearchKey = null)
        {
            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * from classes where lower(classname) like lower(@key) " +
                "or lower(classcode) like lower(@key) " ;

            // Search Parameter
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");

            cmd.Prepare();

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an emptylist of classes
            List<Class> Classes = new List<Class>();

            // Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                string TeacherId = ResultSet["teacherid"].ToString();
                string ClassStartDate = ResultSet["startdate"].ToString();
                string ClassFinishDate = ResultSet["finishdate"].ToString();
                string ClassName = ResultSet["classname"].ToString();

                // Create a new Teacher Object
                Class NewClass = new Class();
                NewClass.ClassName = ClassName;
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.TeacherId = TeacherId;
                NewClass.StartDate = ClassStartDate;
                NewClass.FinishDate = ClassFinishDate;

                // Add the Teacher Name to the List
                Classes.Add(NewClass);

            }

            // Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return Classes;
        }

        /// <summary>
        /// This Method will return a class in the database based on the class id
        /// <example>GET api/ClassData/FindClass/1</example>
        /// </summary>
        /// <param name="id">classid</param>
        /// <returns>A class based on the class id</returns>
        /// 

        [HttpGet]

        public Class FindClass(int id)
        {
            Class NewClass = new Class();

            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * from classes WHERE classid = @id";

            // Search Parameter
            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                string TeacherId = ResultSet["teacherid"].ToString();
                string ClassStartDate = ResultSet["startdate"].ToString();
                string ClassFinishDate = ResultSet["finishdate"].ToString();
                string ClassName = ResultSet["classname"].ToString();

                // Create a new Teacher Object

                NewClass.ClassName = ClassName;
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.TeacherId = TeacherId;
                NewClass.StartDate = ClassStartDate;
                NewClass.FinishDate = ClassFinishDate;
            }


            return NewClass;
        }

        
    }
}
