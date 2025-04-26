using Edunite.DbService.AppDbContextModels;

namespace Edunite.Domain.Features.Teacher.Courserequest
{
    public interface ICourseRequestByTeacherRepository
    {
        Task<TblCourseRequest> CreateCourseRequestAsync(Guid userId, Guid courseId, Guid UserDetailId,
            string statusValue);
        Task DeleteCourseRequestAsync(Guid id);
        Task<List<TblCourseRequest>> GetAllCourseRequestsAsync();
        Task<TblCourseRequest?> GetCourseRequestByIdAsync(Guid id);
        Task<List<TblCourseRequest>> GetCourseRequestByUserIdAsync(Guid userId);
        Task<List<TblCourse>> GetAllCourseByUserIdAsync(Guid userId);
    }
}
