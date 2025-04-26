using Edunite.Domain.Features.CourseRequest;
using Edunite.DTO.Features.CourseRequest;
using Microsoft.EntityFrameworkCore;

namespace Edunite.Infrastructure.Features.CourseRequest;

public class CourseRequestRepository : ICourseRequestRepository
{
	private readonly AppDbContext _appDbContext;

	public CourseRequestRepository(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<Result<Guid>> CreateCourseRequestAsync(CreateCourseRequestModel model, CancellationToken cancellationToken)
	{
		Result<Guid> result;

		try
		{
			if(model.CourseId == null || model.UserId == null)
			{
				result = Result<Guid>.Fail("CourseId and UserId are required.");
			}

			var course = await _appDbContext.TblCourses
				.FirstOrDefaultAsync(c => c.CourseId == model.CourseId, cancellationToken);

			if(course is null)
			{
				result = Result<Guid>.NotFound("Course not found.");

			}

			var courseRequestId = Guid.NewGuid();

			var courseRequest = new TblCourseRequest
			{
				CourseRequestId = courseRequestId,
				CourseId = model.CourseId.Value,
				TeacherDetailId = model.TeacherDetailId,
				UserId = model.UserId.Value,
				Status = "Pending",
				//CreateAt = DateTime.UtcNow
			};

			_appDbContext.TblCourseRequests .Add(courseRequest);

			var existingUserIds = string.IsNullOrEmpty(course.StudentQty)
			   ? new List<Guid>()
			   : course.StudentQty
				   .Split(',', StringSplitOptions.RemoveEmptyEntries)
				   .Select(x => Guid.TryParse(x, out var g) ? g : Guid.Empty)
				   .Where(g => g != Guid.Empty)
				   .ToList();

			if (!existingUserIds.Contains(model.UserId.Value))
				existingUserIds.Add(model.UserId.Value);

			course.StudentQty = string.Join(",", existingUserIds);

			await _appDbContext.SaveChangesAsync(cancellationToken);

			result = Result<Guid>.SuccessData(courseRequestId, "Course Request created and User added to course");
		}

		catch (Exception ex)
		{
			result = Result<Guid>.Failure(ex);
		}

	result:
		return result;
	}

	public async Task<Result<CourseRequestListModel>> GetCourseRequestListAsync(int pageNo, int pageSize, CancellationToken cancellationToken)
	{
		Result<CourseRequestListModel> result;

		try
		{
			var query = _appDbContext.TblCourseRequests.OrderByDescending(x => x.CourseRequestId);
			var lst = await query
				.Paginate(pageNo, pageSize)
				.ToListAsync(cancellationToken: cancellationToken);

			var totalCount = await query.CountAsync(cancellationToken: cancellationToken);
			var pageCount = totalCount / pageSize;

			if(totalCount % pageSize > 0)
			{
				pageCount++;
			}

			var pageSettingModel = new PageSettingModel(pageNo, pageSize, pageCount, totalCount);

			var model = new CourseRequestListModel()
			{
				DataLst = lst.Select(x => new CourseRequestModel()
				{
					CourseRequestId = x.CourseRequestId,
					UserId = x.UserId,
					CourseId = x.CourseId,
					TeacherDetailId = x.TeacherDetailId,
					Status = x.Status,
				}).AsQueryable(),
				PageSetting = pageSettingModel
			};

			result = Result<CourseRequestListModel>.SuccessData(model);

		}

		catch (Exception ex)
		{
			result = Result<CourseRequestListModel>.Failure(ex);
		}

		result:
		return result;
	}

	public async Task<Result<List<CourseRequestModel>>> GetCourseRequestsByStudentIdAsync(Guid studentUserId, CancellationToken cancellationToken)
	{
		Result<List<CourseRequestModel>> result;

		try
		{
			var user = await _appDbContext.AspNetUsers
				.Include(u => u.Roles)
				.FirstOrDefaultAsync(u => u.Id == studentUserId, cancellationToken);

			if(user is null)
			{
				result = Result<List<CourseRequestModel>>.NotFound("User not Found");
			}

			var isStudent = user.Roles.Any(r => r.Name.ToLower() == "Student");

			if (!isStudent)
				return Result<List<CourseRequestModel>>.Fail("User is not a student.");

			var courseRequests = await _appDbContext.TblCourseRequests
		   .Where(cr => cr.UserId == studentUserId)
		   .Select(cr => new CourseRequestModel
		   {
			   CourseRequestId = cr.CourseRequestId,
			   UserId = cr.UserId,
			   CourseId = cr.CourseId,
			   TeacherDetailId = cr.TeacherDetailId,
			   Status = cr.Status
		   })
		   .ToListAsync(cancellationToken);

			result  = Result<List<CourseRequestModel>>.SuccessData(courseRequests);
		}

		catch (Exception ex)
		{
			result = Result<List<CourseRequestModel>>.Failure(ex);
		}

	result:
		return result;
	}

	public async Task<Result<List<CourseRequestModel>>> GetCourseRequestsByTeacherIdAsync(Guid teacherUserId, CancellationToken cancellationToken)
	{
		Result<List<CourseRequestModel>> result;

		try
		{
			var user = await _appDbContext.AspNetUsers
				.Include(u => u.Roles)
				.FirstOrDefaultAsync(u => u.Id == teacherUserId, cancellationToken);

			if (user is null)
			{
				result = Result<List<CourseRequestModel>>.NotFound("User not Found");
			}

			var isStudent = user.Roles.Any(r => r.Name.ToLower() == "Student");

			if (!isStudent)
				return Result<List<CourseRequestModel>>.Fail("User is not a student.");

			var courseRequests = await _appDbContext.TblCourseRequests
		   .Where(cr => cr.UserId == teacherUserId)
		   .Select(cr => new CourseRequestModel
		   {
			   CourseRequestId = cr.CourseRequestId,
			   UserId = cr.UserId,
			   CourseId = cr.CourseId,
			   TeacherDetailId = cr.TeacherDetailId,
			   Status = cr.Status
		   })
		   .ToListAsync(cancellationToken);

			result = Result<List<CourseRequestModel>>.SuccessData(courseRequests);
		}

		catch (Exception ex)
		{
			result = Result<List<CourseRequestModel>>.Failure(ex);
		}

	result:
		return result;
	}
}
