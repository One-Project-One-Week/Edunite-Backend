namespace Edunite.Application.Features.User.GetUserById;

#region GetUserByIdQueryHandler

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserModel>>
{
    private readonly IUserNormalRepository _userRepository;

    public GetUserByIdQueryHandler(IUserNormalRepository userRepository)
    {
        _userRepository = userRepository;
    }

	#region Handle

	public async Task<Result<UserModel>> Handle(GetUserByIdQuery request, CancellationToken cs)
    {
        Result<UserModel> result;

        if (request.Id == Guid.Empty)
        {
            result = Result<UserModel>.Fail("Invalid user ID");
            goto result;
        }

        result = await _userRepository.GetUserByIdAsync(request.Id, cs);
    result:
        return result;
    }

	#endregion
}

#endregion