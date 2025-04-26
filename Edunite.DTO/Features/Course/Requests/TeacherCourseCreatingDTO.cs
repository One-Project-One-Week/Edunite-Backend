namespace Edunite.DTO.Features.Course.Requests;

#region TeacherCourseCreatingDTO

public class TeacherCourseCreatingDTO
    {
        public Guid UserId { get; set; }
        public Guid TeacherDetailId { get; set; }
        public string StatusValue { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int Duration { get; set; }

        public string Schedule { get; set; }

        public TimeOnly StartTime { get; set; }
        

        public TimeOnly EndTime { get; set; }
        public string StudentQty { get; set; }
    }
#endregion