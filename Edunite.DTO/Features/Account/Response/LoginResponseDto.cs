namespace Edunite.DTO.Features.Account.Response;

#region LoginResponseDto

public class LoginResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public DateTime ExpireAt { get; set; }
    }

#endregion