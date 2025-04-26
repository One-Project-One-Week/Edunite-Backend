using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Edunite.Domain.Features.CourseRequest;
using Edunite.Infrastructure.Features.CourseRequest;
using Edunite.Domain.Features.Account;
using Edunite.Infrastructure.Features.Account;
using Edunite.Domain.Features.AuthProcessor;
using Edunite.Infrastructure.Features.AuthRepo;
using Edunite.Domain.Features.Course;
using Edunite.Infrastructure.Features.Course;
using Edunite.Domain.Features.SMTPRepo;
using Edunite.Infrastructure.Features.SMTPRepo;
using Edunite.Domain.Features.Subject;
using Edunite.Domain.Features.SubjectCategory;
using Edunite.Domain.Features.Teacher.TeacherDetailsOrProfile;
using Edunite.Infrastructure.Features.Teacher;
using Edunite.Domain.Features.TokenProcessors;
using Edunite.Infrastructure.Features.TokenRepo;
using Edunite.Application.Extension.FormConverter;
using Edunite.Domain.Features.Teacher.Courserequest;
using Edunite.DTO.Features.UserAuth.SMTPInfo;
using Edunite.DTO.Features.UserAuth.JWTPrincipal;
using Edunite.Domain.Features.User.UserNormal;
using Edunite.Infrastructure.Features.User.UserNormal;
using Edunite.Domain.Features.IRoleRepo;
using Edunite.Infrastructure.Features.RoleRepo;
using Edunite.Application.Extension.RoleAuth;
using Edunite.Application.Extension.PasswordHah;

namespace Edunite.Presentation.Extension;
public static class DependencyInjection
{

	#region AddDbContextService

	private static IServiceCollection AddDbContextService(this IServiceCollection services, WebApplicationBuilder builder)
	{
		builder.Services.AddDbContext<AppDbContext>(
			opt =>
			{
				opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
				opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			},
			ServiceLifetime.Transient,
			ServiceLifetime.Transient
		);

		return services;
	}

	#endregion

	#region AddRepositoryService

	private static IServiceCollection AddRepositoryService(this IServiceCollection services)
	{

		services.AddScoped<IStudentRepository, StudentRepository>()
			.AddScoped<ICourseRequestRepository, CourseRequestRepository>();
        services.AddScoped<IAccount, AccountRepository>()
        .AddScoped<IAuthTokenProcessor, JwtAuth>()
        .AddScoped<ICourseRepository, CourseRepository>()
        .AddScoped<ISMTPEmail, SMTPRepository>()
        .AddScoped<ISubjectRepository, SubjectRepository>()
        .AddScoped<ISubjectCategoryRepository, SubjectCategoryRepository>()
        .AddScoped<ITeacherRepository, TeacherRepository>()
        .AddScoped<ITeacherCertificateRepository, TeacherCertificateRepository>()
        .AddScoped<ITokenProcessor, TokenProcessor>()
        .AddScoped<IByteAndFormConverterExtension, ByteAndFormConverterExtension>()
        .AddScoped<ICourseRequestByTeacherRepository, CourseRequestByTeacherRepository>()
		.AddScoped<IUserRepository,UserRepository>()
		.AddScoped<IUserNormalRepository,UserNormalRepository>()
		.AddSingleton<IPasswordHasher, PasswordHasher>()
		.AddScoped<IRoleRepository,RoleRepository>();


        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RoleAuthorizationBehavior<,>));

        services.AddHttpContextAccessor();


		return services;
	}

	#endregion

	#region AddDependencyInjection

	public static IServiceCollection AddDependencyInjection(this IServiceCollection services, WebApplicationBuilder builder)
	{
		 services.AddDbContextService(builder)
            .AddRepositoryService()
            //.AddIdentityServices()
			.AddJwtServices(builder.Configuration);

        return services;
	}

	#endregion

	#region AddJwtServices

	//  public static IServiceCollection AddJwtServices(this IServiceCollection services, IConfiguration configuration)
	//  {
	//      services.Configure<Jwt>(configuration.GetSection(Jwt.jwtkey));

	//      var jwtOptions = configuration.GetSection(Jwt.jwtkey).Get<Jwt>() ?? throw new ArgumentException(nameof(Jwt));

	//      services.AddAuthentication(options =>
	//      {
	//          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	//          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	//          options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
	//      }).AddJwtBearer(option =>
	//      {
	//          option.TokenValidationParameters = new TokenValidationParameters
	//          {
	//              ValidateIssuer = true,
	//              ValidateAudience = true,
	//              ValidateLifetime = true,
	//              ValidateIssuerSigningKey = true,
	//              ValidIssuer = jwtOptions.Issuer,
	//              ValidAudience = jwtOptions.Audience,
	//              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
	//          };

	//          option.Events = new JwtBearerEvents
	//          {
	//              OnMessageReceived = context =>
	//              {
	//                  var token = context.Request.Cookies["access_token"];
	//                  if (!string.IsNullOrEmpty(token))
	//                  {
	//                      context.Token = token;
	//                  }
	//                  return Task.CompletedTask;
	//              }
	//          };
	//      });

	//var smtpSettings = new SmtpInfo();
	//configuration.GetSection("SmtpInfo").Bind(smtpSettings);
	//services.AddSingleton(smtpSettings);

	//return services;
	//  }


	public static IServiceCollection AddJwtServices(this IServiceCollection services, IConfiguration configuration)
	{
		// Configure JWT settings
		services.Configure<Jwt>(configuration.GetSection(Jwt.jwtkey));
		var jwtOptions = configuration.GetSection(Jwt.jwtkey).Get<Jwt>() ?? throw new ArgumentException("JWT configuration is missing.");

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtOptions.Issuer,
					ValidAudience = jwtOptions.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret!))
				};

				options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						// Read token from cookies if available
						if (context.Request.Cookies.TryGetValue("access_token", out var token) && !string.IsNullOrEmpty(token))
						{
							context.Token = token;
						}
						return Task.CompletedTask;
					}
				};
			});

		// Configure SMTP settings
		var smtpSettings = new SmtpInfo();
		configuration.GetSection("SmtpInfo").Bind(smtpSettings);
		services.AddSingleton(smtpSettings);

		return services;
	}

	#endregion


	//private static IServiceCollection AddIdentityServices(this IServiceCollection services)
	//{
	//	services.AddIdentity<Edunite.DbService.AppDbContextModels.AspNetUser, Edunite.DbService.AppDbContextModels.AspNetRole>(options =>
	//	{
	//		// You can configure Identity options here if needed
	//		options.Password.RequireDigit = false;
	//		options.Password.RequiredLength = 6;
	//		options.Password.RequireNonAlphanumeric = false;
	//		options.Password.RequireUppercase = false;
	//		options.Password.RequireLowercase = false;
	//	})
	//	.AddEntityFrameworkStores<AppDbContext>()
	//	.AddDefaultTokenProviders();

	//	return services;
	//}

}
