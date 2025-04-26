using Edunite.DbService.AppDbContextModels;
using Microsoft.AspNetCore.Http;
namespace Edunite.Domain.Features.Teacher.TeacherDetailsOrProfile
{
    public interface ITeacherCertificateRepository
    {
        Task<TblTeacherCertificate> CreateTeacherCertificateAsync(Guid teacherDetailId, IFormFile certificate);
        Task DeleteTeacherCertificateAsync(Guid id);
        Task<List<TblTeacherCertificate>> GetAllTeacherCertificatesAsync();
        Task<TblTeacherCertificate?> GetTeacherCertificateByIdAsync(Guid id);
        Task<TblTeacherCertificate?> GetTeacherCertificateByUserIdAsync(Guid id);
        Task UpdateTeacherCertificateAsync(TblTeacherCertificate detail);
    }
}
