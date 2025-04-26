namespace Edunite.Infrastructure.Features.Student;

public class StudentRepository : IStudentRepository
{
	private readonly AppDbContext _context;

	public StudentRepository(AppDbContext context)
	{
		_context = context;
	}

	#region GetStudentListAsync

	public async Task<Result<StudentListModel>> GetStudentListAsync(int pageNo, int pageSize, CancellationToken cancellationToken)
	{
		Result<StudentListModel> result;

		try
		{
			var students = _context.TblStudents
			.Join(_context.AspNetUsers,
			 s => s.UserId,
			 u => u.Id,
			 (s, u) => new { Student = s, User = u }
			)
			.OrderByDescending(x => x.Student.StudentId);

			var lst = await students.Paginate(pageNo, pageSize)
				.ToListAsync(cancellationToken: cancellationToken);

			var totalCount = await students.CountAsync(cancellationToken: cancellationToken);

			var pageCount = totalCount / pageSize;

			if (totalCount % pageSize > 0)
			{
				pageCount++;
			}

			var pageSettingModel = new PageSettingModel(pageNo, pageSize, pageCount, totalCount);

			var model = new StudentListModel
			{
				DataLst = lst.Select(x => new StudentModel
				{
					//StudentId = x.Student.StudentId,
					Grade = x.Student.Grade,
					Interests = x.Student.Interests,
					EnrollCourses = x.Student.EnrollCourses,
					Id = x.User.Id,
					Name = x.User.Name,
					Email = x.User.Email,
					PhoneNumber = x.User.PhoneNumber,
				}),

				PageSetting = pageSettingModel
			};

			result = Result<StudentListModel>.SuccessData(model);
		}
		catch (Exception ex)
		{
			result = Result<StudentListModel>.Failure(ex);
		}
		return result;
	}

	#endregion

	#region GetStudentByIdAsync

	public async Task<Result<StudentModel>> GetStudentByIdAsync(Guid studentId, CancellationToken cancellationToken)
	{
		Result<StudentModel> result;
		try
		{
			var studentWithUser = await _context.TblStudents
				.Join(_context.AspNetUsers,
					s => s.UserId,
					u => u.Id,
					(s, u) => new { Student = s, User = u }
				)
				.Where(x => x.Student.StudentId == studentId)
				.Select(x => new StudentModel
				{
					//StudentId = x.Student.StudentId,
					Id = x.User.Id,
					Grade = x.Student.Grade,
					Interests = x.Student.Interests,
					EnrollCourses = x.Student.EnrollCourses,
					Name = x.User.Name,
					Email = x.User.Email,
					PhoneNumber = x.User.PhoneNumber
				})
				.FirstOrDefaultAsync(cancellationToken);

			if (studentWithUser == null)
			{
				result = Result<StudentModel>.NotFound("Student not found.");
			}

			result = Result<StudentModel>.SuccessData(studentWithUser!);
		}
		catch (Exception ex)
		{
			result = Result<StudentModel>.Failure(ex);
		}

		return result;
	}

	#endregion

	#region CreateStudentAsync

	public async Task<Result<StudentModel>> CreateStudentAsync(StudentRequestModel requestModel, CancellationToken cancellationToken)
	{
		Result<StudentModel> result;

		try
		{
			var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Id == requestModel.UserId, cancellationToken);
			if (user is null)
			{
				result = Result<StudentModel>.NotFound("User not found");
				goto result;
			}

			var existingStudent = await _context.TblStudents.FirstOrDefaultAsync(s => s.UserId == requestModel.UserId, cancellationToken);
			if (existingStudent is not null)
			{
				result = Result<StudentModel>.Fail("Student already exists for this user");
				goto result;
			}

			var student = new TblStudent
			{
				UserId = requestModel.UserId,
				Grade = requestModel.Grade,
				EnrollCourses = requestModel.EnrollCourses,
				IsActive = true,
			};

			_context.TblStudents.Add(student);
			await _context.SaveChangesAsync(cancellationToken);

			var studentModel = new StudentModel
			{
				Id = user.Id,
				Name = user.Name,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Grade = student.Grade,
				Interests = student.Interests,
				EnrollCourses = student.EnrollCourses
			};

			result = Result<StudentModel>.SuccessData(studentModel);
		}
		catch (Exception ex)
		{
			result = Result<StudentModel>.Failure(ex);
		}

	result:
		return result;
	}

	#endregion

	#region DeactivateStudentAsync

	public async Task<Result<StudentModel>> DeactivateStudentAsync(Guid studentId, CancellationToken cancellationToken)
	{
		Result<StudentModel> result;

		try
		{
			var student = await _context.TblStudents.FindAsync([studentId, cancellationToken], cancellationToken : cancellationToken);

			if(student is null)
			{
				result = Result<StudentModel>.NotFound();
				goto result;
			}

			student.IsActive = false;
			_context.TblStudents.Update(student);
			await _context.SaveChangesAsync(cancellationToken );

			result = Result<StudentModel>.DeleteSuccess();
		}
		catch(Exception ex)
		{
			result = Result<StudentModel>.Failure(ex);
		}

	result:
		return result;
	}

	#endregion

}
