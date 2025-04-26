namespace Edunite.DbService.AppDbContextModels;

#region TblSubject

public partial class TblSubject
{
    public Guid SubjectId { get; set; }

    public Guid SubjectCategoryId { get; set; }

    public string Subject { get; set; } = null!;

    public string? Grade { get; set; }

    public virtual TblSubjectCategory SubjectCategory { get; set; } = null!;
}

#endregion