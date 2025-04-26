namespace Edunite.DbService.AppDbContextModels;

#region TblCourseRequest

public partial class TblCourseRequest
{
    public Guid CourseRequestId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? CourseId { get; set; }

    public Guid? TeacherDetailId { get; set; }

    public string? Status { get; set; }

    public virtual TblCourse? Course { get; set; }

    public virtual TblTeacherDetail? TeacherDetail { get; set; }

    public virtual AspNetUser? User { get; set; }
}

#endregion