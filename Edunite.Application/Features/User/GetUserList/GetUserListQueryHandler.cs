namespace Edunite.Application.Features.User.GetUserList;

#region GetUserListQueryHandler

public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, Result<UserListModel>>
{
	private readonly IUserNormalRepository _userNormalRepository;

	public GetUserListQueryHandler(IUserNormalRepository userNormalRepository)
	{
		_userNormalRepository = userNormalRepository;
	}

	#region Handle

	public async Task<Result<UserListModel>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
	{
		if (request.PageNo <= 0)
			return Result<UserListModel>.Fail("Invalid Page Number.");

		if (request.PageSize <= 0)
			return Result<UserListModel>.Fail("Invalid Page Size.");

		return await _userNormalRepository.GetUserListAsync(request.PageNo, request.PageSize, cancellationToken);
	}

	#endregion
}


#endregion