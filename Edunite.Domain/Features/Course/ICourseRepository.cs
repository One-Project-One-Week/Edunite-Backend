using Edunite.DbService.AppDbContextModels;
using Edunite.DTO.Features.Course.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Domain.Features.Course
{
    public interface ICourseRepository 
    {
        Task<TblCourse> CreateCourseAsync(CreateCourseDto ccd);
        Task DeleteCourseAsync(Guid id);
        Task<List<TblCourse>> GetAllCoursesAsync();
        Task<TblCourse?> GetCourseByIdAsync(Guid id);
        Task<List<TblCourse>> GetCourseAllByUserIdAsync(Guid id);
        Task UpdateCourseAsync(TblCourse detail);
    }
}
