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
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        ///  This Method will return a list of teachers in the database
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of Teachers in the database (id, teacher first name, teacher last name, employee number).
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]

        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * from teachers where lower(teacherfname) like lower(@key) " +
                "or lower(teacherlname) like lower(@key) " +
                "or lower(concat(teacherfname,' ',teacherlname)) like lower(@key)";

            // Search Parameter
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");

            cmd.Prepare();

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            // Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                // Create a new Teacher Object

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
                // Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            // Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            // Return the final list of teachers
            return Teachers;
        }

       


        /// <summary>
        /// This Method will return a teacher based on the teacher id
        /// <example>api/ClassData/FindTeacher/1</example>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Returns a teacher based on the teacher id
        /// </returns>
        /// 

        [HttpGet]

        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * from teachers WHERE teacherid= @id";

            // Search Parameter
            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                // Create a new Teacher Object

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }


            return NewTeacher;
        }


        [HttpGet]

        // GET: api/ClassData/FindClassesForTeacher/1

        /// <summary>
        /// This Method will return a list of classes in the database based on the teacher id
        /// <example>GET api/TeacherData/FindClassesForTeacher/1</example>
        /// </summary>
        /// <param name="id">teacherid</param>
        /// <returns>A list of classes based on the teacher id</returns>
        ///

        public IEnumerable<Class> FindClassesForTeacher(int id)
        {
            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * from classes WHERE teacherid = @id";

            // Search Parameter
            cmd.Parameters.AddWithValue("@id", id);

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
        ///  Delete a teacher from the database
        /// </summary>
        /// <param name="id"></param>
        /// <example>GET: /api/TeacherData/DeleteTeacher/1</example>
        /// 

        [HttpPost]

        public void DeleteTeacher(int id)
        {
            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "DELETE from teachers WHERE teacherid = @id ";

            // remove the classes taught by the teacher

            // Search Parameter
            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        /// <summary>
        /// Add a new teacher to the database
        /// </summary>
        ///
        /// <example>POST: /api/TeacherData/AddTeacher</example>
        /// 

        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "INSERT into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) " +
                "values (@TeacherFname, @TeacherLname, @EmployeeNumber, @HireDate, @Salary)";

            // Search Parameter
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "UPDATE teachers SET teacherfname = @TeacherFname, teacherlname = @TeacherLname, employeenumber = @EmployeeNumber, hiredate = @HireDate, salary = @Salary WHERE teacherid = @id";

            // Search Parameter
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        

        
    }
}
