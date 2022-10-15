using System;

namespace Test_Pumox.Entities
{
    public class Employee
    {
        public Int64 Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public JobTitle JobTitle { get; set; }        
        public virtual Company Company { get; set; }
    }

    public enum JobTitle
    {
        Administrator,
        Developer,
        Architect,
        Manager
    }
}
