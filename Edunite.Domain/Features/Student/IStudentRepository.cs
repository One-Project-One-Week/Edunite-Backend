namespace Edunite.Domain.Features.Student;

#region IStudentRepository

public interface IStudentRepository
{
	Task<Result<StudentListModel>> GetStudentListAsync(int pageNo, int pageSize, CancellationToken cancellationToken);
	Task<Result<StudentModel>> GetStudentByIdAsync(Guid studentId, CancellationToken cancellationToken);
	Task<Result<StudentModel>> CreateStudentAsync(StudentRequestModel studentRequestModel, CancellationToken cancellationToken);
	Task<Result<StudentModel>> DeactivateStudentAsync(Guid studentId, CancellationToken cancellationToken);

}

#endregion