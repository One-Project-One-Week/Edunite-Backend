namespace Edunite.DTO.Features.Teacher.Request;

#region CreateTeacherDetailDto

public class CreateTeacherDetailDto
    {
        public Guid UserId { get; set; }
        public Guid SunjectId { get; set; }
        public Byte[] Certificate { get; set; }
    }


#endregion