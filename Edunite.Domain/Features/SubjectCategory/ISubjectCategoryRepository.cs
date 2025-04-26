using Edunite.DbService.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Domain.Features.SubjectCategory
{
    public interface ISubjectCategoryRepository
    {
        Task<TblSubjectCategory> CreateSubjectCategoryAsync(string name);
        Task DeleteSubjectCategoryAsync(Guid id);
        Task<List<TblSubjectCategory>> GetAllSubjectCategoriesAsync();
        Task<TblSubjectCategory?> GetSubjectCategoryByIdAsync(Guid id);
        Task UpdateSubjectCategoryAsync(TblSubjectCategory detail);
    }
}
