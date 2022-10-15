using Test_Pumox.Entities;
using Test_Pumox.Models;

namespace Test_Pumox
{
    public static class Mapp
    {
       
        public static Company CompanyFromDto(CreateCompanyDto dto)
        {
            Company company = new Company();
            company.Employees = new System.Collections.Generic.List<Employee>();
            company.Name = dto.Name;
            company.EstablishmentYear = dto.EstablishmentYear;
            if (dto.Employees is null) return company;
            foreach (var item in dto.Employees)
            {

                company.Employees.Add(new Employee
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DateOfBirth = item.DateOfBirth,
                    JobTitle = GetEnumJobTitleFromDto(item.JobTitle)
                });
            }
            return company;
        }

        public static JobTitle GetEnumJobTitleFromDto(string dto)
        {
            JobTitle jobTitle = new JobTitle();
            switch (dto)
            {
                case "Administrator":
                     jobTitle = JobTitle.Administrator;
                    break;
                case "Developer":
                    jobTitle = JobTitle.Developer;
                    break;
                case "Architect":
                    jobTitle = JobTitle.Architect;
                    break;
                case "Manager":
                    jobTitle = JobTitle.Manager;
                    break;               
            }
            return jobTitle;
        }
        public static CreateCompanyDto CompanyToDto(Company dto)
        {
            CreateCompanyDto company = new CreateCompanyDto();
            company.Employees = new System.Collections.Generic.List<EmployeeDto>();
            company.Name = dto.Name;
            company.EstablishmentYear = dto.EstablishmentYear;
            if (dto.Employees is null) return company;
            foreach (var item in dto.Employees)
            {

                company.Employees.Add(new EmployeeDto
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DateOfBirth = item.DateOfBirth,
                    JobTitle = GetEnumJobTitleToDto(item.JobTitle)
                });
            }
            return company;
        }

        public static string GetEnumJobTitleToDto(JobTitle dto)
        {
            string jobTitle = "";
            switch (dto)
            {
                case JobTitle.Administrator:
                    jobTitle = "Administrator";
                    break;
                case JobTitle.Developer:
                    jobTitle = "Developer";
                    break;
                case JobTitle.Architect:
                    jobTitle = "Architect";
                    break;
                case JobTitle.Manager:
                    jobTitle = "Manager";
                    break;
            }
            return jobTitle;
        }
    }
}
