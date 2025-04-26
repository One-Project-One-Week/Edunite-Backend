namespace Edunite.DTO.Features.Student;

#region StudentListModel

public class StudentListModel
{
	public IEnumerable<StudentModel> DataLst { get; set; }
	public PageSettingModel PageSetting { get; set; }
}

#endregion