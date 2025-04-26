using Edunite.DbService.AppDbContextModels;
using Edunite.DTO.Features.Course.responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Features.Course.GetAllCourse
{
    public class GetAllCourseQuery : IRequest<List<GetAllCourseDto>>, IBaseRequest
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public GetAllCourseQuery(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
