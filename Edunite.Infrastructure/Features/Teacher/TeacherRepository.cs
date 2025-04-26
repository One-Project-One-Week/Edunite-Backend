using Edunite.Domain.Features.Teacher.TeacherDetailsOrProfile;
namespace Edunite.Infrastructure.Features.Teacher
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;
        public TeacherRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TblTeacherDetail> CreateTeacherDetailAsync(Guid userId, string statusValue, string description,
            bool isActive)
        {
            var teacherDetail = new TblTeacherDetail
            {
                UserId = userId,
                Status = statusValue,
                Description = description,
                IsActive = isActive
            };
            await _context.TblTeacherDetails.AddAsync(teacherDetail);
            await _context.SaveChangesAsync();
            return teacherDetail;
        }

        public async Task DeleteTeacherDetailAsync(Guid id)
        {
            await _context.TblTeacherDetails
                .Where(x => x.UserId == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<List<TblTeacherDetail>> GetAllTeacherAsync()
        {
            List<TblTeacherDetail> teacherDetailList = await _context.TblTeacherDetails.ToListAsync();
            return teacherDetailList;
        }

        public async Task<TblTeacherDetail?> GetTeacherByIdAsync(Guid id)
        {
            var teacherDetail = await _context.TblTeacherDetails
                .Where(x => x.TeacherDetailId == id)
                .FirstOrDefaultAsync();
            return teacherDetail;
        }

        public async Task<TblTeacherDetail?> GetTeacherByUserIdAsync(Guid id)
        {
            var teacherDetail = await _context.TblTeacherDetails
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();
            return teacherDetail;
        }

        public async Task UpdateTeacherDetailAsync(TblTeacherDetail detail)
        {
            _context.TblTeacherDetails.Update(detail);
            await _context.SaveChangesAsync();
        }
    }
}
