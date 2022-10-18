using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Test_Pumox.Entities;
using Test_Pumox.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Test_Pumox.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly Test_PumoxDbContext _dbContext;

        public CompanyService(Test_PumoxDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<CreateCompanyDto> GetAll()
        {
            List<CreateCompanyDto> companiesDto = new List<CreateCompanyDto>();
            var companies = _dbContext.Companies.Include(e => e.Employees).ToList();
            foreach (var company in companies) companiesDto.Add(Mapp.CompanyToDto(company));
            return companiesDto;
        }

        public bool Delete(long id)
        {
            var company = _dbContext.Companies.FirstOrDefault(x => x.Id == id);
            var employees = _dbContext.Employees.Where(x => x.Company.Id == id);
            if(company is null) return false;
            _dbContext.Employees.RemoveRange(employees);                 
            _dbContext.Companies.Remove(company);
            _dbContext.SaveChanges();
            return true;
        }

        public long Create(CreateCompanyDto dtoCompany)
        {
            var company = Mapp.CompanyFromDto(dtoCompany);
            _dbContext.Companies.Add(company);
            var succes = _dbContext.SaveChanges();
            return company.Id;            
        }
        public bool Update(long id,CreateCompanyDto dtoCompany)
        {           
            if(!_dbContext.Companies.Any(x=>x.Id == id)) return false;
            var company = Mapp.CompanyFromDto(dtoCompany);
            company.Id = id;
            if(company.Employees.Count>0)
                _dbContext.Employees.RemoveRange(_dbContext.Employees.Where(x=>x.Company.Id == id));
            _dbContext.Companies.Update(company);
            _dbContext.SaveChanges();
            return true;

        }
        public List<CreateCompanyDto> Search(SearchCompanyDto dtoCompany)
        {
            List<CreateCompanyDto> results = new List<CreateCompanyDto>();
            CreateCompanyDto result = new CreateCompanyDto();           
            List<Company> listCompanies = new List<Company>();
                       
            if (!string.IsNullOrEmpty(dtoCompany.Keyword))
            {
                var company = _dbContext.Companies.Where(x => x.Name.StartsWith(dtoCompany.Keyword)).Include(e=>e.Employees);
                if (company.Count() == 0)
                {
                    var companyByEmployee = _dbContext.Companies.Include(e => e.Employees.Where(e => e.FirstName.StartsWith(dtoCompany.Keyword) || e.LastName.StartsWith(dtoCompany.Keyword))).ToList();                   
                    listCompanies = companyByEmployee.Where(x=>x.Employees.Count() != 0).ToList();                    
                }
                else listCompanies = company.ToList();     
            }
            if (dtoCompany.EmployeeDateOfBirthFrom != null || dtoCompany.EmployeeDateOfBirthTo != null)
            {
                var dateFrom = dtoCompany.EmployeeDateOfBirthFrom is null ? new  DateTime() : dtoCompany.EmployeeDateOfBirthFrom;
                var DateTo = dtoCompany.EmployeeDateOfBirthTo is null ? DateTime.Now : dtoCompany.EmployeeDateOfBirthTo;
                List<Company> listCompanyByDate = new List<Company>();
                if (listCompanies.Count>0)
                {                  
                    foreach (var company in listCompanies)
                    {
                        if(company.Employees.Any(e=> e.DateOfBirth >= dateFrom && e.DateOfBirth <= DateTo))
                            listCompanyByDate.Add(company);
                    }
                    if (listCompanyByDate.Count == 0) return results;
                    listCompanies = listCompanyByDate;
                }
                else
                {
                    var companyByEmployeeDateDb = _dbContext.Companies.Include(e => e.Employees.Where(e => e.DateOfBirth >= dateFrom && e.DateOfBirth <= DateTo)).ToList();                  
                    listCompanies = companyByEmployeeDateDb.Where(x => x.Employees.Count() != 0).ToList();
                }                
            }
            if(!string.IsNullOrEmpty(dtoCompany.EmployeeJobTitles))
            {
                var jobTitle = Mapp.GetEnumJobTitleFromDto(dtoCompany.EmployeeJobTitles);
                List<Company> listCompanyByJobTitle = new List<Company>();
                if(listCompanies.Count > 0)
                {
                    foreach (var company in listCompanies)
                    {
                        if (company.Employees.Any(e => e.JobTitle == jobTitle ))
                            listCompanyByJobTitle.Add(company);
                    }
                    if (listCompanyByJobTitle.Count == 0) return results;
                    listCompanies = listCompanyByJobTitle;
                }
                else
                {
                    var listCompanyByJobTitleDb = _dbContext.Companies.Include(e => e.Employees.Where(e => e.JobTitle == jobTitle)).ToList();                   
                    listCompanies = listCompanyByJobTitleDb.Where(x => x.Employees.Count() != 0).ToList();
                }

            }
            if (listCompanies.Count ==0) return results;
            foreach (var company in listCompanies)
            {
                result = Mapp.CompanyToDto(company);
                results.Add(result);
            }
            
            return results;
        }        
    }
}
