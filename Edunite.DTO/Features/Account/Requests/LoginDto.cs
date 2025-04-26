namespace Edunite.DTO.Features.Account.Requests;

#region LoginDto

public class LoginDto : AccountBaseDTO
    {
        public required string Password { get; init; }
    }

#endregion