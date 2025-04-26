using Edunite.DbService.AppDbContextModels;

namespace Edunite.Domain.Features.Teacher.TeacherDetailsOrProfile
{
    public interface ITeacherRepository
    {
        Task<TblTeacherDetail> CreateTeacherDetailAsync(Guid userId, string statusValue, string description,
            bool isActive);
        Task DeleteTeacherDetailAsync(Guid id);
        Task<List<TblTeacherDetail>> GetAllTeacherAsync();
        Task<TblTeacherDetail?> GetTeacherByIdAsync(Guid id);
        Task<TblTeacherDetail?> GetTeacherByUserIdAsync(Guid id);
    }
}
