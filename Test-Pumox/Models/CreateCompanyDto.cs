using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test_Pumox.Entities;

namespace Test_Pumox.Models
{
    public class CreateCompanyDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int EstablishmentYear { get; set; }        
        public List<EmployeeDto> Employees { get; set; }
        
    }
}
