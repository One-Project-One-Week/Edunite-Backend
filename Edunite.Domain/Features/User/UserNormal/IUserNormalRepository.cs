using Edunite.DTO.Features.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Domain.Features.User.UserNormal
{
	public interface IUserNormalRepository
	{
		 Task<Result<UserListModel>> GetUserListAsync(int pageNo, int pageSize, CancellationToken cs);
		 Task<Result<UserModel>> GetUserByIdAsync(Guid id, CancellationToken cs);
	}
}
