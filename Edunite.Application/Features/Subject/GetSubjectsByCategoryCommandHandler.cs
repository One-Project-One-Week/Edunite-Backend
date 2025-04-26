namespace Edunite.Application.Features.Subject;

    public class GetSubjectsByCategoryCommandHandler : IRequestHandler<GetSubjectsByCategoryQuery, List<GetSubjectsBycategory>>
    {
        private readonly ISubjectRepository _subjectRepository;
        public GetSubjectsByCategoryCommandHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

	#region Handle

	public async Task<List<GetSubjectsBycategory>> Handle(GetSubjectsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var lst = await _subjectRepository.GetAllSubjectBySubjectCategoryId(request.categoryId);

				var result = lst.Select(subject => new GetSubjectsBycategory
				{
					Subjectid = subject.SubjectId,
					Grade = subject.Grade,
					Subject = subject.Subject
				}).ToList();
				return result;
			}

		}

	#endregion
