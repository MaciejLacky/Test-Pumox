using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using Test_Pumox.Authentication;
using Test_Pumox.Entities;
using Test_Pumox.Models;
using Test_Pumox.Services;

namespace Test_Pumox.Controllers
{
    
    [ApiController]   
    public class CompanyController : ControllerBase
    {        
        private readonly CompanyService _companyService;
      

        public CompanyController(CompanyService companyService)
        {
            _companyService = companyService;
        }
        
        [HttpGet("company")]
        [BasicAuthentication]
        public ActionResult<IEnumerable<Company>> GetAll()
        {
            //string userName =  Thread.CurrentPrincipal.Identity.Name;
            //if (string.IsNullOrEmpty(userName)) return Unauthorized("niepoprawny login lub hasło użytkownika");

            var companies = _companyService.GetAll();
            if (companies.Count == 0) return NotFound("Brak danych");
            return Ok(companies);
        }

        [HttpPost]
        [Route("company/create")]
        [BasicAuthentication]
        public ActionResult Create([FromBody] CreateCompanyDto dtoCompany)
        {
            var idCompany = _companyService.Create(dtoCompany);
            if (idCompany == 0) return BadRequest("Sprawdź poprawność wprowadzonych danych");
            return Ok($"Id : {idCompany}");
        }

        [HttpPost]
        [Route("company/search")]
        [BasicAuthentication]
        public ActionResult Search([FromBody] SearchCompanyDto dtoCompany)
        {
            var result = _companyService.Search(dtoCompany);
            if (result.Count() == 0) return NotFound("Nie znaleziono firm o wprowadzonych kryteriach");
            return Ok(result);
        }

        [HttpPut("company/update/{id}")]
        public ActionResult Update([FromRoute] long id, [FromBody] CreateCompanyDto dtoCompany)
        {
            var idCompany = _companyService.Update(id,dtoCompany);
            if (!idCompany) return BadRequest("Błąd aktualizacji. Sprawdź poprawność danych");
            return Ok("Zaktualizowano poprawnie dane");
        }

        [HttpDelete("company/delete/{id}")]
        [BasicAuthentication]
        public ActionResult Delete([FromRoute] long id)
        {
            var success = _companyService.Delete(id);
            if (!success) return NotFound("Nie znaleziono firmy o wprowadzonym id");
            return Ok("Poprawnie usunięto wpis");
            
        }
    }
}
