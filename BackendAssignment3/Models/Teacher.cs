using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendAssignment3.Models
{
    public class Teacher
    {
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string EmployeeNumber;
        public DateTime HireDate;
        public decimal Salary;
        public List<Class> Classes;

    }
}