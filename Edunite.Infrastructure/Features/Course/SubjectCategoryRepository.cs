using Edunite.Domain.Features.SubjectCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Infrastructure.Features.Course
{
    public class SubjectCategoryRepository : ISubjectCategoryRepository
    {
        private readonly AppDbContext _context;
        public SubjectCategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TblSubjectCategory> CreateSubjectCategoryAsync(string name)
        {
            var subjectCategory = new TblSubjectCategory
            {
                SubjectTypeName = name
            };
            await _context.TblSubjectCategories.AddAsync(subjectCategory);
            await _context.SaveChangesAsync();
            return subjectCategory;
        }
        public async Task DeleteSubjectCategoryAsync(Guid id)
        {
            await _context.TblSubjectCategories
                .Where(x => x.SubjectCategoryId == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
        public async Task<List<TblSubjectCategory>> GetAllSubjectCategoriesAsync()
        {
            List<TblSubjectCategory> subjectCategories = await _context.TblSubjectCategories.ToListAsync();
            return subjectCategories;
        }

        public async Task<TblSubjectCategory?> GetSubjectCategoryByIdAsync(Guid id)
        {
            var subjectCategory = await _context.TblSubjectCategories
                .Where(x => x.SubjectCategoryId == id)
                .FirstOrDefaultAsync();
            return subjectCategory;
        }

        public async Task UpdateSubjectCategoryAsync(TblSubjectCategory detail)
        {
            _context.TblSubjectCategories.Update(detail);
            await _context.SaveChangesAsync();
        }
    }
}
