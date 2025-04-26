using Edunite.DTO.Features.Course.responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Features.Course.GetCourseById
{
    public class GetStudentByIdQuery : IRequest<GetAllCourseDto>
    {
        public Guid Id { get; set; }
        public GetStudentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
