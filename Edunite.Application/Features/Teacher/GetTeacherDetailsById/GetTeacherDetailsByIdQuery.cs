namespace Edunite.Application.Features.Teacher.GetTeacherDetailsById;

#region GetTeacherDetailsByIdQuery

public class GetTeacherDetailsByIdQuery : IRequest<TeacherDetailDto>
    {
        public Guid UserId { get; set; }
        public GetTeacherDetailsByIdQuery(Guid UserId)
        {
            this.UserId = UserId;
        }
    }

#endregion