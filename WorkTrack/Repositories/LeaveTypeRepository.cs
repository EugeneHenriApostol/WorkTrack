using Microsoft.EntityFrameworkCore;
using WorkTrack.Data;
using WorkTrack.Models;
using WorkTrack.Repositories.Interfaces;

namespace WorkTrack.Repositories
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly AppDbContext _context;

        public LeaveTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaveType>> GetAllAsync()
        {
            return await _context.LeaveTypes
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task<LeaveType?> GetByIdAsync(int id)
        {
            return await _context.LeaveTypes
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}