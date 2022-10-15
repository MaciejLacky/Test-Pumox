using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test_Pumox.Models
{
    public class UpdateCompanyDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int EstablishmentYear { get; set; }
        [Required]
        public List<EmployeeDto> Employees { get; set; }
    }
}
