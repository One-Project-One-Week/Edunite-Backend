namespace Edunite.DTO.Features.Course.Requests;

#region CreateCourseDto

public class CreateCourseDto 
{
    public Guid TeacherDetailId { get; set; }
    public string Status { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public string Schedule { get; set; }
    public string StudentQty { get; set; }
}

#endregion