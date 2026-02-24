using Microsoft.EntityFrameworkCore;
using WorkTrack.Data;
using WorkTrack.Models;
using WorkTrack.Repositories.Interfaces;

namespace WorkTrack.Repositories
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        private readonly AppDbContext _context;

        public LeaveBalanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LeaveBalance?> GetByUserAndTypeAsync(string userId, int leaveTypeId, int year)
        {
            return await _context.LeaveBalances
                .Include(lb => lb.LeaveType)
                .FirstOrDefaultAsync(lb =>
                    lb.UserId == userId &&
                    lb.LeaveTypeId == leaveTypeId &&
                    lb.Year == year);
        }

        public async Task AddAsync(LeaveBalance balance)
        {
            await _context.LeaveBalances.AddAsync(balance);
        }

        public async Task UpdateAsync(LeaveBalance balance)
        {
            _context.LeaveBalances.Update(balance);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}