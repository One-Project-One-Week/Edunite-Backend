namespace Edunite.DbService.AppDbContextModels;

#region TblSubjectCategory

public partial class TblSubjectCategory
{
    public Guid SubjectCategoryId { get; set; }

    public string SubjectTypeName { get; set; } = null!;

    public virtual ICollection<TblSubject> TblSubjects { get; set; } = new List<TblSubject>();
}

#endregion