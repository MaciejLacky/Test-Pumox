using System.Collections.Generic;
using System;

namespace Test_Pumox.Entities
{
    public class Company
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public virtual List<Employee> Employees { get; set; }
    }
}
