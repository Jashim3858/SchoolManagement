using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using SchoolManagement.Models;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Mappings
{
    public class StudentMapping : Profile
    {
        public StudentMapping()
        {
            CreateMap<StudentInformation, StudentInformationVM>();
            CreateMap<StudentInformationVM, StudentInformation>().ForMember(s => s.StudentID, opt => opt.Ignore());
        }
    }
}
