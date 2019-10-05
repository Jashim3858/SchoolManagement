using SchoolManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models
{
    public class ClassInformation
    {

        [Key]
        public int ClassID { get; set; }
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }
        [Display(Name = "Total Student")]
        public int NoOfStudent { get; set; }
        public ICollection<StudentInformation> Students { get; set; }
        public ICollection<TeacherInformation> Teachers { get; set; }

    }
}
