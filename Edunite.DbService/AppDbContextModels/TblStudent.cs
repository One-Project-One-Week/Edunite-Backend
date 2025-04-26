namespace Edunite.DbService.AppDbContextModels;

#region TblStudent

public partial class TblStudent
{
    public Guid StudentId { get; set; }

    public Guid? UserId { get; set; }

    public string? Grade { get; set; }

    public string? Interests { get; set; }

    public string? EnrollCourses { get; set; }

    public bool? IsActive { get; set; }

    public virtual AspNetUser? User { get; set; }
}

#endregion
