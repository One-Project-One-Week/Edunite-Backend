namespace Edunite.DTO.Features.Teacher.Response;

#region TeacherDetailDto

public class TeacherDetailDto
{
    public Guid TeacherDetailId { get; set; }

    public Guid? UserId { get; set; }

    public string? Description { get; set; }
    public Guid TeacherCertificateId { get; set; }

    public IFormFile Certificate { get; set; }

}

#endregion
