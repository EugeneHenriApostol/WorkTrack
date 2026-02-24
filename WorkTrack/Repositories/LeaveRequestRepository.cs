using Microsoft.EntityFrameworkCore;
using WorkTrack.Data;
using WorkTrack.Models;
using WorkTrack.Models.Enum;
using WorkTrack.Repositories.Interfaces;


namespace WorkTrack.Repositories
{
    public class LeaveRequestRepository
    {
        private readonly AppDbContext _context;

        public LeaveRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LeaveRequest leaveRequest)
        {
            await _context.LeaveRequests.AddAsync(leaveRequest);
        }

        public async Task UpdateAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Remove(leaveRequest);
            await Task.CompletedTask;
        }

        public async Task<LeaveRequest?> GetByIdAsync(int id)
        {
            return await _context.LeaveRequests
                .Include(l => l.LeaveType)
                .Include(l => l.User)
                .Include(l => l.ApprovedBy)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<LeaveRequest>> GetByUserIdAsync(string userId)
        {
            return await _context.LeaveRequests
                .Include(l => l.LeaveType)
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetPendingByManagerId(string managerId)
        {
            return await _context.LeaveRequests
                .Include(l => l.User)
                .Include(l => l.LeaveType)
                .Where(l => l.User.ManagerId == managerId && 
                        l.Status == LeaveStatus.Pending)
                .ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetAllAsync()
        {
            return await _context.LeaveRequests
                .Include(l => l.User)
                .Include(l => l.LeaveType)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
