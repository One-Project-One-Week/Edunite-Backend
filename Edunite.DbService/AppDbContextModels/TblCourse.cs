namespace Edunite.DbService.AppDbContextModels;

#region TblCourse

public partial class TblCourse
{
    public Guid CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public Guid? TeacherDetailId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? Duration { get; set; }

    public string? Schedule { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public string? Status { get; set; }

    public string? StudentQty { get; set; }

    public virtual ICollection<TblCourseRequest> TblCourseRequests { get; set; } = new List<TblCourseRequest>();

    public virtual TblTeacherDetail? TeacherDetail { get; set; }
}

#endregion
