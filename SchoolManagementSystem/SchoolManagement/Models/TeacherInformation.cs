using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models
{
    public class TeacherInformation
    {
        [Key]
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string TeachersCellPhone { get; set; }
        public string Religion { get; set; }
        public string TeacherAddress { get; set; }
        public string ImagePath { get; set; }

        public int ClassID { get; set; }
        public virtual ClassInformation Class { get; set; }

    }
}
