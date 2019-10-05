using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models
{
    public class StudentInformation
    {
        [Key]
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        [DataType(DataType.Date)]
        public string DOB { get; set; }
        public string Religion { get; set; }
        public string GurdiansCellPhone { get; set; }
        public string StudentAddress { get; set; }
        public string ImagePath { get; set; }

        public int ClassID { get; set; }
        public virtual ClassInformation Class { get; set; }
    }
}
