using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Mapster;

namespace CourseManagment.EF.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public CertificateService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<Certificate> GenerateCertificateAsync(string userId, int courseId)
        {
            // Check if user exists
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            // Check if certificate already exists
            var existingCertificate = await _unitOfWork.Certificates.GetOneAsync(
                c => c.UserId == userId && c.CourseId == courseId
            );

            if (existingCertificate != null)
            {
                return existingCertificate;
            }

            var certificate = new Certificate
            {
                UserId = userId,
                CourseId = courseId,
                CertificateCode = GenerateCertificateCode(),
                GeneratedAt = DateTime.UtcNow,
                DownloadUrl = $"/api/certificates/download/{Guid.NewGuid()}"
            };

            await _unitOfWork.Certificates.CreateAsync(certificate);
            await _unitOfWork.CompleteAsync();

            return certificate;
        }

        public async Task<Certificate?> GetCertificateByIdAsync(int certificateId)
        {
            return await _unitOfWork.Certificates.GetOneAsync(c => c.CertificateId == certificateId);
        }

        public async Task<List<Certificate>> GetUserCertificatesAsync(string userId)
        {
            return await _unitOfWork.Certificates.GetAsync(
                c => c.UserId == userId,
                orderBy: q => q.OrderByDescending(c => c.GeneratedAt)
            );
        }

        public async Task<Certificate?> VerifyCertificateAsync(string certificateCode)
        {
            return await _unitOfWork.Certificates.GetOneAsync(c => c.CertificateCode == certificateCode);
        }

        public async Task DeleteCertificateAsync(int id)
        {
            var certificate = await GetCertificateByIdAsync(id);
            if (certificate != null)
            {
                _unitOfWork.Certificates.Delete(certificate);
                await _unitOfWork.CompleteAsync();
            }

        }
       
        private string GenerateCertificateCode()
        {
            return $"CERT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }

        public async Task UpdateCertificateAsync(CertificateDTO certificateDTO)
        {
            var certificate = certificateDTO.Adapt<Certificate>();
            _unitOfWork.Certificates.Update(certificate);
            await _unitOfWork.CompleteAsync();
        }
    }
}