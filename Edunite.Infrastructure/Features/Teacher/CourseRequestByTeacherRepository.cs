using Edunite.Domain.Features.Teacher.Courserequest;

namespace Edunite.Infrastructure.Features.Teacher 
{
    public class CourseRequestByTeacherRepository : ICourseRequestByTeacherRepository
    {
        private readonly AppDbContext _context;
        public CourseRequestByTeacherRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TblCourseRequest> CreateCourseRequestAsync(Guid userId, Guid courseId, Guid UserDetailId,
            string statusValue)
        {
            var courserequest = new TblCourseRequest
            {
                UserId = userId,
                CourseId = courseId,
                TeacherDetailId = UserDetailId,
                Status= statusValue
            };
            await _context.AddAsync(courserequest);
            await _context.SaveChangesAsync();
            return courserequest;
        }

        public async Task DeleteCourseRequestAsync(Guid id)
        {
            await _context.TblCourseRequests
                .Where(x => x.CourseRequestId == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<List<TblCourseRequest>> GetAllCourseRequestsAsync()
        {
            List<TblCourseRequest> courseRequestList = await _context.TblCourseRequests.ToListAsync();
            return courseRequestList;
        }

        public Task<TblCourseRequest?> GetCourseRequestByIdAsync(Guid id)
        {
            var courseRequest = _context.TblCourseRequests
                .Where(x => x.CourseRequestId == id)
                .FirstOrDefaultAsync();
            return courseRequest;
        }
        public Task<List<TblCourseRequest>> GetCourseRequestByUserIdAsync(Guid userId)
        {
            var courseRequest = _context.TblCourseRequests
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return courseRequest;
        }
        public async Task<List<TblCourse>> GetAllCourseByUserIdAsync(Guid userId)
        {
            List<TblCourseRequest> courseRequest = await _context.TblCourseRequests
                .Where(x => x.UserId == userId)
                .ToListAsync();
            List<TblCourse> courseList = courseRequest
                .Select(x => x.Course)
                .ToList();
            return courseList;
        }
        public async Task<TblCourse?> GetCourseByUserIdAsync(Guid userId)
        {
            var courseRequest = await _context.TblCourseRequests
                .Where(x => x.UserId == userId)
                .Select(x => x.Course)
                .FirstOrDefaultAsync();
            return courseRequest;
        }
        public async Task UpdateCourseRequestAsync(TblCourseRequest detail)
        {
            _context.TblCourseRequests.Update(detail);
            await _context.SaveChangesAsync();
        }
    }
}
