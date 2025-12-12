using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificateService _certificateService;

        public CertificatesController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<Certificate>> GenerateCertificate(GenerateCertificateRequest request)
        {
            var certificate = await _certificateService.GenerateCertificateAsync(request.UserId, request.CourseId);
            return Ok(certificate);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Certificate>> GetCertificate(int id)
        {
            var certificate = await _certificateService.GetCertificateByIdAsync(id);
            if (certificate == null) return NotFound();
            return Ok(certificate);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Certificate>>> GetUserCertificates(string userId)
        {
            var certificates = await _certificateService.GetUserCertificatesAsync(userId);
            return Ok(certificates);
        }

        [HttpGet("verify/{certificateCode}")]
        public async Task<ActionResult<Certificate>> VerifyCertificate(string certificateCode)
        {
            var certificate = await _certificateService.VerifyCertificateAsync(certificateCode);
            if (certificate == null) return NotFound();
            return Ok(certificate);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCertificate(int id, CertificateDTO certificate)
        {
            var existingCertificate = await _certificateService.GetCertificateByIdAsync(id);
            if (existingCertificate == null || id != existingCertificate.CertificateId) return BadRequest();
            await _certificateService.UpdateCertificateAsync(certificate);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            await _certificateService.DeleteCertificateAsync(id);
            return NoContent();
        }

    }

    
}