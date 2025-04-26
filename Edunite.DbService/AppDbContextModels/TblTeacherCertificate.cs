namespace Edunite.DbService.AppDbContextModels;

#region TblTeacherCertificate

public partial class TblTeacherCertificate
{
    public Guid TeacherCertificateId { get; set; }

    public Guid? TeacherDetailId { get; set; }

    public byte[]? Certificate { get; set; }

    public virtual TblTeacherDetail? TeacherDetail { get; set; }
}

#endregion
