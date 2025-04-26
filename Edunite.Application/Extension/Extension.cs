using Edunite.Application.Extension.RoleAuth;

namespace Edunite.Application.Extension;

public static class Extension
{

	#region AddMediatRService

	public static IServiceCollection AddMediatRService(this IServiceCollection services)
	{
		return services.AddMediatR(cf =>
		cf.RegisterServicesFromAssembly(typeof(Extension).Assembly));


    }

    #endregion
}
