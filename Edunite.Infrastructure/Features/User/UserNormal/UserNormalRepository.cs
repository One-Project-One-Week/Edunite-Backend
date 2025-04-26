using Edunite.Domain.Features.User.UserNormal;
using Edunite.DTO.Features.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Infrastructure.Features.User.UserNormal
{
	public class UserNormalRepository : IUserNormalRepository
	{

		private readonly AppDbContext _context;

		public UserNormalRepository(AppDbContext context)
		{
		    _context = context;
		}

		public async Task<Result<UserListModel>> GetUserListAsync(int pageNo, int pageSize, CancellationToken cs)
		{
		    Result<UserListModel> result;

		    try
		    {
		        var users = _context.AspNetUsers
		            .OrderByDescending(x => x.Id);

		        var lst = await users.Paginate(pageNo, pageSize)
		            .ToListAsync(cancellationToken: cs);

		        var totalCount = await users.CountAsync(cancellationToken: cs);

		        var pageCount = totalCount / pageSize;

		        if (totalCount % pageSize > 0)
		        {
		            pageCount++;
		        }

		        var pageSettingModel = new PageSettingModel(pageNo, pageSize, pageCount, totalCount);

		        var model = new UserListModel
		        {
		            DataLst = lst.Select(x => new UserModel
		            {
		                Id = x.Id,
		                Name = x.Name,
		                Email = x.Email,
		                PhoneNumber = x.PhoneNumber,
		            }),

		            PageSetting = pageSettingModel
		        };

		        result = Result<UserListModel>.SuccessData(model);
		    }
		    catch (Exception ex)
		    {
		        result = Result<UserListModel>.Failure(ex);
		    }
		    return result;
		}

		public async Task<Result<UserModel>> GetUserByIdAsync(Guid userId, CancellationToken cs)
		{
		    Result<UserModel> result;
		    try
		    {
		        var user = await _context.AspNetUsers
		            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cs);
		        if (user == null)
		        {
		            return Result<UserModel>.Fail("User not found");
		        }
		        var model = new UserModel
		        {
		            Id = user.Id,
		            Name = user.Name,
		            Email = user.Email,
		            PhoneNumber = user.PhoneNumber,
		        };
		        result = Result<UserModel>.SuccessData(model);
		    }
		    catch (Exception ex)
		    {
		        result = Result<UserModel>.Failure(ex);
		    }
		    return result;
		}
	}
}
