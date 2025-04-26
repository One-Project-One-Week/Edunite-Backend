namespace Edunite.DTO.Features.CourseRequest;

#region CourseRequestListModel

public class CourseRequestListModel
{
	public IEnumerable<CourseRequestModel> DataLst { get; set; }
	public PageSettingModel PageSetting { get; set; }
}

#endregion
