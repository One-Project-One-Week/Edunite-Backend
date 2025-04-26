using Edunite.DTO.Features.Account.Response;

namespace Edunite.Application.Features.Account.RefreshToken
{
	public class RefreshTokenCommand : IRequest<LoginResponseDto>
	{
		public string? RefreshToken { get; set; }
		public RefreshTokenCommand(string? refreshToken)
		{
			RefreshToken = refreshToken;
		}
	}
}
