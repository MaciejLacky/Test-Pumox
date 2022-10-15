using System;
using Test_Pumox.Entities;

namespace Test_Pumox.Models
{
    public class SearchCompanyDto
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public string EmployeeJobTitles { get; set; }
    }
}
