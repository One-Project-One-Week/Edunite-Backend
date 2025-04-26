namespace Edunite.Application.Features.User.GetUserById;

#region GetUserByIdQuery

public class GetUserByIdQuery : IRequest<Result<UserModel>>
{
    public Guid Id { get; set; }
    public GetUserByIdQuery(Guid userId)
    {
        Id = userId;
    }
}

#endregion
