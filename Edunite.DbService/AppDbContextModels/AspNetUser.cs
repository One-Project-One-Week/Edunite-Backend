namespace Edunite.DbService.AppDbContextModels;

#region AspNetUser

public partial class AspNetUser
{
    public AspNetUser(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }

    public string? ForgetPasswordToken { get; set; }

    public DateTime ForgetPasswordTokenExpiryTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>();

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<TblCourseRequest> TblCourseRequests { get; set; } = new List<TblCourseRequest>();

    public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();

    public virtual ICollection<TblTeacherDetail> TblTeacherDetails { get; set; } = new List<TblTeacherDetail>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}

#endregion
