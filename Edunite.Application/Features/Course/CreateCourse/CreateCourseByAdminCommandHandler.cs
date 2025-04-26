using Edunite.Domain.Features.Course;
namespace Edunite.Application.Features.Course.CreateCourse
{
    public class CreateCourseByAdminCommandHandler : IRequestHandler<CreateCourseByAdminCommand, Unit>
    {
        private readonly ICourseRepository _courseRepository;
        public CreateCourseByAdminCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Unit> Handle(CreateCourseByAdminCommand request, CancellationToken cancellationToken)
        {
            await _courseRepository.CreateCourseAsync(request.CreateCourseDto);
            return Unit.Value;
        }
    }
}
