namespace Edunite.DTO.Features.Account.Requests;

#region DeactivateDto

public class DeactivateDto : AccountBaseDTO
    {
        public required string Password { get; init; }
    }

#endregion