using Edunite.DbService.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Domain.Features.Subject
{
    public interface ISubjectRepository
    {
        Task<TblSubject> CreateSubjectAsync(Guid subjectCategoryId, string subjectName, string grade);
        Task DeleteSubjectAsync(Guid id);
        Task<List<TblSubject>> GetAllSubjectAsync();
        Task<List<TblSubject>> GetAllSubjectBySubjectCategoryId(Guid id);
        Task<TblSubject?> GetSubjectByIdAsync(Guid id);
        Task UpdateSubjectAsync(TblSubject detail);
    }
}
