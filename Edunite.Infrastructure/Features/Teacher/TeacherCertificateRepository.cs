using Edunite.Domain.Features.Teacher.TeacherDetailsOrProfile;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Infrastructure.Features.Teacher
{
    public class TeacherCertificateRepository : ITeacherCertificateRepository
    {
        private readonly AppDbContext _context;
        public TeacherCertificateRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TblTeacherCertificate> CreateTeacherCertificateAsync(Guid teacherDetailId, IFormFile certificate)
        {
            using var memoryStream = new MemoryStream();
            await certificate.CopyToAsync(memoryStream);
            var imageByte = memoryStream.ToArray();
            var NewTeacherCertificate = new TblTeacherCertificate
            {
                TeacherDetailId = teacherDetailId,
                Certificate = imageByte
            };
            await _context.AddAsync(NewTeacherCertificate);
            await _context.SaveChangesAsync();
            return NewTeacherCertificate;
        }
        public async Task DeleteTeacherCertificateAsync(Guid id)
        {
            await _context.TblTeacherCertificates.
                Where(x => x.TeacherCertificateId == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
        public async Task<List<TblTeacherCertificate>> GetAllTeacherCertificatesAsync()
        {
            List<TblTeacherCertificate> teacherCertificates = await _context.TblTeacherCertificates.ToListAsync();
            return teacherCertificates;
        }
        public async Task<TblTeacherCertificate?> GetTeacherCertificateByIdAsync(Guid id)
        {
            var teachercertificate = await _context.TblTeacherCertificates
                .Where(x => x.TeacherCertificateId == id)
                .FirstOrDefaultAsync();
            return teachercertificate;
        }
        public async Task<TblTeacherCertificate?> GetTeacherCertificateByUserIdAsync(Guid id)
        {
            var teacherCertificate = await _context.TblTeacherCertificates
                .Where(x => x.TeacherDetailId == id)
                .FirstOrDefaultAsync();
            return teacherCertificate;
        }
        public async Task UpdateTeacherCertificateAsync(TblTeacherCertificate detail)
        {
            _context.TblTeacherCertificates.Update(detail);
            await _context.SaveChangesAsync();
        }

    }
}
