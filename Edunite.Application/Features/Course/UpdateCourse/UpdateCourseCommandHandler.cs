using Edunite.Domain.Features.Course;
namespace Edunite.Application.Features.Course.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Unit>
    {
        private readonly ICourseRepository _courseRepository;
        public UpdateCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var existingCourse = await _courseRepository.GetCourseByIdAsync(request.UpdateCourse.Id);

            if (existingCourse == null)
            {
                throw new Exception("Course not found.");
            }

            existingCourse.Title = request.UpdateCourse.Title;
            existingCourse.Description = request.UpdateCourse.Description;
            existingCourse.StartDate = request.UpdateCourse.StartDate;
            existingCourse.EndDate = request.UpdateCourse.EndDate;
            existingCourse.StudentQty = request.UpdateCourse.StudentQty;
            existingCourse.Status = request.UpdateCourse.Status;
            existingCourse.Schedule = request.UpdateCourse.Schedule;
            existingCourse.TeacherDetailId = request.UpdateCourse.UserId;
            await _courseRepository.UpdateCourseAsync(existingCourse);

            return Unit.Value;
        }
    }
}
