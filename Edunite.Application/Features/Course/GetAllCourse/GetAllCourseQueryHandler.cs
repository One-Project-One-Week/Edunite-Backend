using Edunite.Domain.Features.Course;
using Edunite.DTO.Features.Course.responses;

namespace Edunite.Application.Features.Course.GetAllCourse
{
    public class GetAllCourseQueryHandler : IRequestHandler<GetAllCourseQuery, List<GetAllCourseDto>>
    {
        private readonly ICourseRepository _courseRepository;
        public GetAllCourseQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<List<GetAllCourseDto>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetAllCoursesAsync();

            var courseDtos = courses.Select(course => new GetAllCourseDto
            {
                Id = course.CourseId,
                Title = course.Title,
                Description = course.Description,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                TeacherDetailId = course.TeacherDetailId,
                StudentQty = course.StudentQty,
                Schedule = course.Schedule,
                Status = course.Status,
            }).ToList();

            return courseDtos;
        }
    }
}
