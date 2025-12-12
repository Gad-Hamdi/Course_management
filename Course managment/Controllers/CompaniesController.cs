using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null) return NotFound();
            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult<Company>> CreateCompany(CompanyDTO company)
        {
            var createdCompany = await _companyService.CreateCompanyAsync(company);
            return CreatedAtAction(nameof(GetCompany), new { id = createdCompany.CompanyId }, createdCompany);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyDTO company)
        {
            var existingCompany = await _companyService.GetCompanyByIdAsync(id);
            if (existingCompany==null||id != existingCompany.CompanyId) return BadRequest();
            await _companyService.UpdateCompanyAsync(company);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return NoContent();
        }
    }
}