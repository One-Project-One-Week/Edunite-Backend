namespace Edunite.DTO.Features.User;

#region UserListModel

public class UserListModel
{
    public IEnumerable<UserModel> DataLst { get; set; }
    public PageSettingModel PageSetting { get; set; }
}

#endregion