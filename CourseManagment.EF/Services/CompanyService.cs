using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Mapster;

namespace CourseManagment.EF.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Company> CreateCompanyAsync(CompanyDTO companyDto)
        {
            var company = companyDto.Adapt<Company>();
            company.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Companies.CreateAsync(company);
            await _unitOfWork.CompleteAsync();
            return company;
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _unitOfWork.Companies.GetOneAsync(c => c.CompanyId == id);
        }

        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            return await _unitOfWork.Companies.GetAsync();
        }

        public async Task UpdateCompanyAsync(CompanyDTO companyDTO)
        {
            var company = companyDTO.Adapt<Company>();
            _unitOfWork.Companies.Update(company);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var company = await GetCompanyByIdAsync(id);
            if (company != null)
            {
                _unitOfWork.Companies.Delete(company);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}