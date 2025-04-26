using Edunite.Application.Features.Teacher.CreateCourseByTeacher;

public class CreateCourseByTeacherCommandHandler : IRequestHandler<CreateCourseByTeacherCommand, Unit>
{
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseRequestByTeacherRepository _courseRequestRepository;
        public CreateCourseByTeacherCommandHandler(ICourseRepository courseRepository, 
            ICourseRequestByTeacherRepository courseRequestRepository)
        {
            _courseRepository = courseRepository;
            _courseRequestRepository = courseRequestRepository;
        }

	#region Handle

	public async Task<Unit> Handle(CreateCourseByTeacherCommand request, CancellationToken cancellationToken)
    {
        var coursecreate = new CreateCourseDto
        {
            Title = request.TeacherRequest.Title,
            Description = request.TeacherRequest.Description,
            TeacherDetailId = request.TeacherRequest.TeacherDetailId,
            StartDate = request.TeacherRequest.StartDate,
            EndDate = request.TeacherRequest.EndDate,
            Schedule = request.TeacherRequest.Schedule,
            Status = request.TeacherRequest.StatusValue,
            StudentQty = null
        };
        var createdCourse = await _courseRepository.CreateCourseAsync(coursecreate);
        var courseId = createdCourse.CourseId;
        var courseRequest = new TblCourseRequest
        {
            UserId = request.TeacherRequest.UserId,
            CourseId = createdCourse.CourseId,
            TeacherDetailId = request.TeacherRequest.TeacherDetailId,
            Status = request.TeacherRequest.StatusValue
        };
        await _courseRequestRepository.CreateCourseRequestAsync(courseId, request.TeacherRequest.UserId,
            request.TeacherRequest.TeacherDetailId, request.TeacherRequest.StatusValue);
        return Unit.Value;
    }

	#endregion
}


