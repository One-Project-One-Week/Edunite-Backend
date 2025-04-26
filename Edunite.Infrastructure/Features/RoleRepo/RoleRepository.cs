using Edunite.Domain.Features.IRoleRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Infrastructure.Features.RoleRepo
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateRoleAsync(string roleName)
        {
            var role = new AspNetRole
            {
                Id = Guid.NewGuid(),
                Name = roleName
            };

            _context.AspNetRoles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _context.AspNetRoles.AnyAsync(r => r.Name == roleName);
        }
    }
}
