using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Models;

namespace CourseManagment.CORE.Interfaces
{
    public interface ICertificateService
    {
        Task<Certificate> GenerateCertificateAsync(string userId, int courseId);
        Task<Certificate?> GetCertificateByIdAsync(int certificateId);
        Task<List<Certificate>> GetUserCertificatesAsync(string userId);
        Task<Certificate?> VerifyCertificateAsync(string certificateCode);

        Task UpdateCertificateAsync(CertificateDTO company);
        Task DeleteCertificateAsync(int id);
    }
}