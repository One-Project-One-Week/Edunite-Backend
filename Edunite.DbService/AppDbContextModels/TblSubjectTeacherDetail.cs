namespace Edunite.DbService.AppDbContextModels;

#region TblSubjectTeacherDetail

public partial class TblSubjectTeacherDetail
{
    public Guid? SubjectTeacherDetailsId { get; set; }

    public Guid? SubjectId { get; set; }

    public Guid? TeacherDetailId { get; set; }

    public virtual TblSubject? Subject { get; set; }

    public virtual TblTeacherDetail? TeacherDetail { get; set; }
}

#endregion
