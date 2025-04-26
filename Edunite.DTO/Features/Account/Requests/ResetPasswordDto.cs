namespace Edunite.DTO.Features.Account.Requests;

#region ResetPasswordDto

public class ResetPasswordDto : AccountBaseDTO
    {
        public required string ResetPasswordToken { get; set; }
        public required string Password { get; set; }
    }

#endregion