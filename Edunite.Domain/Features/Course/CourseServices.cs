using Edunite.Domain.Features.Teacher.Courserequest;
using Edunite.DTO.Features.Course.responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Domain.Features.Course
{
    public class CourseServices
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseRequestByTeacherRepository _courseRequestRepository;
        public CourseServices(ICourseRepository courseRepository, ICourseRequestByTeacherRepository courseRequestRepository)
        {
            _courseRepository = courseRepository;
            _courseRequestRepository = courseRequestRepository;
        }
        
    }
}
