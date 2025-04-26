namespace Edunite.DTO.Features.Account.Requests;

#region RegisterDto

public class RegisterDto : AccountBaseDTO
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
    }

#endregion
