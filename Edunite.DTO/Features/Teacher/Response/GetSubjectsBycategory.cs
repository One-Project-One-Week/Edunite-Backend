namespace Edunite.DTO.Features.Teacher.Response;

#region GetSubjectsBycategory

public class GetSubjectsBycategory
{

    public Guid Subjectid { get; set; }
    public string Subject { get; set; } 
    public string? Grade { get; set; }

}

#endregion