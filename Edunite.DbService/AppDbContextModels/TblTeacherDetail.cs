namespace Edunite.DbService.AppDbContextModels;

#region TblTeacherDetail

public partial class TblTeacherDetail
{
    public Guid TeacherDetailId { get; set; }

    public Guid? UserId { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<TblCourseRequest> TblCourseRequests { get; set; } = new List<TblCourseRequest>();

    public virtual ICollection<TblCourse> TblCourses { get; set; } = new List<TblCourse>();

    public virtual ICollection<TblTeacherCertificate> TblTeacherCertificates { get; set; } = new List<TblTeacherCertificate>();

    public virtual AspNetUser? User { get; set; }
}

#endregion