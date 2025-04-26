
namespace Edunite.DbService.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

	#region DbSet

	public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<TblCourse> TblCourses { get; set; }

    public virtual DbSet<TblCourseRequest> TblCourseRequests { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    public virtual DbSet<TblSubject> TblSubjects { get; set; }

    public virtual DbSet<TblSubjectCategory> TblSubjectCategories { get; set; }

    public virtual DbSet<TblSubjectTeacherDetail> TblSubjectTeacherDetails { get; set; }

    public virtual DbSet<TblTeacherCertificate> TblTeacherCertificates { get; set; }

    public virtual DbSet<TblTeacherDetail> TblTeacherDetails { get; set; }

	public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }


	#endregion

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
		=> optionsBuilder.UseSqlServer("Server=.;Database=EduniteDb;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");




//	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=LAPTOP-NFU692OK\\MSSQLSERVER02;Database=EduniteDb;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True;");


  
    #region OnModelCreating

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

		#region AspNetRole

		modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasOne(e => e.User)
				.WithMany(e => e.AspNetUserRoles)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId");

            entity.HasOne(e => e.Role)
            .WithMany(e => e.AspNetUserRoles)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId");
        });

		#endregion

		#region AspNetRoleClaim

		modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

		#endregion

		#region AspNetUser

		modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.ForgetPasswordToken)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

		#endregion

		#region AspNetUserClaim

		modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

		#endregion

		#region AspNetUserLogin

		modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

		#endregion

		#region AspNetUserToken

		modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

		#endregion

		#region TblCourse

		modelBuilder.Entity<TblCourse>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A72AC68E4A");

            entity.ToTable("Tbl_Courses");

            entity.Property(e => e.CourseId).ValueGeneratedNever();
            entity.Property(e => e.Schedule).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.TeacherDetail).WithMany(p => p.TblCourses)
                .HasForeignKey(d => d.TeacherDetailId)
                .HasConstraintName("FK__Courses__Teacher__5BE2A6F2");
        });

		#endregion

		#region TblCourseRequest

		modelBuilder.Entity<TblCourseRequest>(entity =>
        {
            entity.HasKey(e => e.CourseRequestId).HasName("PK__CourseRe__D41A0DA4D7BA4025");

            entity.ToTable("Tbl_CourseRequests");

            entity.Property(e => e.CourseRequestId).ValueGeneratedNever();
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Course).WithMany(p => p.TblCourseRequests)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__CourseReq__Cours__60A75C0F");

            entity.HasOne(d => d.TeacherDetail).WithMany(p => p.TblCourseRequests)
                .HasForeignKey(d => d.TeacherDetailId)
                .HasConstraintName("FK__CourseReq__Teach__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.TblCourseRequests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CourseReq__UserI__5FB337D6");
        });

		#endregion

		#region TblStudent

		modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B997EC9B89A");

            entity.ToTable("Tbl_Students");

            entity.Property(e => e.StudentId).ValueGeneratedNever();
            entity.Property(e => e.Grade).HasMaxLength(20);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.TblStudents)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Students__UserId__4D94879B");
        });

		#endregion

		#region TblSubject

		modelBuilder.Entity<TblSubject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA3A8F7225C2F");

            entity.ToTable("Tbl_Subjects");

            entity.Property(e => e.SubjectId).ValueGeneratedNever();
            entity.Property(e => e.Grade).HasMaxLength(20);
            entity.Property(e => e.Subject).HasMaxLength(100);

            entity.HasOne(d => d.SubjectCategory).WithMany(p => p.TblSubjects)
                .HasForeignKey(d => d.SubjectCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subjects_SubjectCategories");
        });

		#endregion

		#region TblSubjectCategory

		modelBuilder.Entity<TblSubjectCategory>(entity =>
        {
            entity.HasKey(e => e.SubjectCategoryId).HasName("PK__SubjectC__4A1B6AF2B151FDC4");

            entity.ToTable("Tbl_SubjectCategories");

            entity.Property(e => e.SubjectCategoryId).ValueGeneratedNever();
            entity.Property(e => e.SubjectTypeName).HasMaxLength(50);
        });

		#endregion

		#region TblSubjectTeacherDetail

		modelBuilder.Entity<TblSubjectTeacherDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_SubjectTeacherDetails");

            entity.HasOne(d => d.Subject).WithMany()
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__SubjectTe__Subje__6FE99F9F");

            entity.HasOne(d => d.TeacherDetail).WithMany()
                .HasForeignKey(d => d.TeacherDetailId)
                .HasConstraintName("FK__SubjectTe__Teach__70DDC3D8");
        });

		#endregion

		#region TblTeacherCertificate

		modelBuilder.Entity<TblTeacherCertificate>(entity =>
        {
            entity.HasKey(e => e.TeacherCertificateId).HasName("PK__TeacherC__7FE90D624156955A");

            entity.ToTable("Tbl_TeacherCertificates");

            entity.Property(e => e.TeacherCertificateId).ValueGeneratedNever();

            entity.HasOne(d => d.TeacherDetail).WithMany(p => p.TblTeacherCertificates)
                .HasForeignKey(d => d.TeacherDetailId)
                .HasConstraintName("FK__TeacherCe__Teach__5812160E");
        });

		#endregion

		#region TblTeacherDetail

		modelBuilder.Entity<TblTeacherDetail>(entity =>
        {
            entity.HasKey(e => e.TeacherDetailId).HasName("PK__TeacherD__294BCEE08FFE2E1A");

            entity.ToTable("Tbl_TeacherDetails");

            entity.Property(e => e.TeacherDetailId).ValueGeneratedNever();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.User).WithMany(p => p.TblTeacherDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TeacherDe__UserI__5441852A");
        });

		#endregion

		OnModelCreatingPartial(modelBuilder);
    }

	#endregion

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
