using Edunite.Domain.Features.Subject;

namespace Edunite.Infrastructure.Features.Course
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _context;
        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TblSubject> CreateSubjectAsync(Guid subjectCategoryId, string subjectName, string grade)
        {
            var subject = new TblSubject
            {
                SubjectCategoryId = subjectCategoryId,
                Subject = subjectName,
                Grade = grade
            };
            await _context.TblSubjects.AddAsync(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task DeleteSubjectAsync(Guid id)
        {
            await _context.TblSubjects
                .Where(x => x.SubjectId == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<List<TblSubject>> GetAllSubjectAsync()
        {
            List<TblSubject> subjects = await _context.TblSubjects.ToListAsync();
            return subjects;
        }

        public async Task<List<TblSubject>> GetAllSubjectBySubjectCategoryId(Guid id)
        {
            List<TblSubject> subjects = await _context.TblSubjects
                .Where(x => x.SubjectCategoryId == id)
                .ToListAsync();
            return subjects;
        }
        public async Task<TblSubject?> GetSubjectByIdAsync(Guid id)
        {
            var subject = await _context.TblSubjects
                .Where(x => x.SubjectId == id)
                .FirstOrDefaultAsync();
            return subject;
        }

        public async Task UpdateSubjectAsync(TblSubject detail)
        {
            _context.TblSubjects.Update(detail);
            await _context.SaveChangesAsync();
        }
    }
}
