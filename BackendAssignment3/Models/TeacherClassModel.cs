using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendAssignment3.Models
{
    // Model for the teacher and class. It contains the teacher and the classes that the teacher teaches.

    public class TeacherClassModel
    {
        public Teacher Teacher { get;  set; }
        public IEnumerable<Class> Classes { get;  set; }

        
    }
}