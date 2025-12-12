using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Models;

namespace CourseManagment.CORE.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> CreateCompanyAsync(CompanyDTO company);
        Task<Company?> GetCompanyByIdAsync(int id);
        Task<List<Company>> GetAllCompaniesAsync();
        Task UpdateCompanyAsync(CompanyDTO company);
        Task DeleteCompanyAsync(int id);
    }
}