namespace Edunite.Application.Features.User.GetUserList;

#region GetUserListQuery

public class GetUserListQuery : IRequest<Result<UserListModel>>
{
	public int PageNo { get; set; }
	public int PageSize { get; set; }

	public GetUserListQuery(int pageNo, int pageSize)
	{
		PageNo = pageNo;
		PageSize = pageSize;
	}
}

#endregion
