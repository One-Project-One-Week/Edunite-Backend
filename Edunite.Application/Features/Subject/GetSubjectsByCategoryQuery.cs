namespace Edunite.Application.Features.Subject;

#region GetSubjectsByCategoryQuery

public class GetSubjectsByCategoryQuery : IRequest<List<GetSubjectsBycategory>>
{
    public Guid categoryId {  get; set; }
    public GetSubjectsByCategoryQuery(Guid categoryId)
    {
        this.categoryId = categoryId;
    }
}

#endregion
