using System.Collections.Generic;
using Test_Pumox.Models;

namespace Test_Pumox.Services
{
    public interface ICompanyService
    {
        List<CreateCompanyDto> GetAll();
        bool Delete(long id);
        long Create(CreateCompanyDto dtoCompany);
        bool Update(long id, CreateCompanyDto dtoCompany);
        List<CreateCompanyDto> Search(SearchCompanyDto dtoCompany);
    }
}
