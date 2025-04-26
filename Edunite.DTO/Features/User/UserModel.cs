namespace Edunite.DTO.Features.User;

#region UserModel

public class UserModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? PhoneNumber { get; set; }
}

#endregion