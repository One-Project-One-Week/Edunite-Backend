using Edunite.Domain.Features.Course;
using Edunite.DTO.Features.Course.responses;

namespace Edunite.Application.Features.Course.GetCourseById
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, GetAllCourseDto>
    {
        private readonly ICourseRepository _courseRepository;
        public GetStudentByIdQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<GetAllCourseDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var courseEntity = await _courseRepository.GetCourseByIdAsync(request.Id);

            if (courseEntity == null)
            {
                throw new Exception("Course not found.");
            }

            var courseDto = new GetAllCourseDto
            {
                Id = courseEntity.CourseId,
                Title = courseEntity.Title,
                Description = courseEntity.Description,
                StartDate = courseEntity.StartDate,
                EndDate = courseEntity.EndDate,
                TeacherDetailId = courseEntity.TeacherDetailId,
                StudentQty = courseEntity.StudentQty,
                Schedule = courseEntity.Schedule,
            };

            return courseDto;
        }
    }
}
