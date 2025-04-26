namespace Edunite.DTO.Features.Course.responses;

#region GetAllCourseDto

public class GetAllCourseDto : BaseTeacherDto
    {
        public Guid UserId { get; set; }
        public Guid? TeacherDetailId { get; set; }
        public string? Status { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? Schedule { get; set; }
        public string? StudentQty { get; set; }
    }


#endregion