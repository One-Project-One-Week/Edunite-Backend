using Edunite.Domain.Features.Course;
using Edunite.DTO.Features.Course.Requests;
using Edunite.DTO.Features.Course.responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Infrastructure.Features.Course
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TblCourse> CreateCourseAsync(CreateCourseDto ccd)
        {
            var course = new TblCourse
            {
                Title = ccd.Title,
                Description = ccd.Description,
                TeacherDetailId = ccd.TeacherDetailId,
                StartDate = ccd.StartDate,
                EndDate = ccd.EndDate,
                Schedule = ccd.Schedule,
                Status = ccd.Status,
                StudentQty = ccd.StudentQty
            };
            _context.AddAsync(course);
            await _context.SaveChangesAsync();
            return course;
        }
        public async Task DeleteCourseAsync(Guid id)
        {
            await _context.TblCourses
                .Where(x => x.CourseId == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
        public async Task<List<TblCourse>> GetAllCoursesAsync()
        {
            List<TblCourse> courseList = await _context.TblCourses.ToListAsync();
            return courseList;
        }
        public Task<TblCourse?> GetCourseByIdAsync(Guid id)
        {
            var course = _context.TblCourses
                .Where(x => x.CourseId == id)
                .FirstOrDefaultAsync();
            return course;
        }
        public Task<List<TblCourse>> GetCourseAllByUserIdAsync(Guid id)
        {
            var course = _context.TblCourses
                .Where(x => x.TeacherDetailId == id)
                .ToListAsync();
            return course;
        }
        public async Task UpdateCourseAsync(TblCourse detail)
        {
            _context.Update(detail);
            await _context.SaveChangesAsync();
        }
    }
}
