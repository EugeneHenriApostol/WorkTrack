using WorkTrack.Models;

namespace WorkTrack.Repositories.Interfaces
{
    public interface ILeaveBalanceRepository
    {
        Task<LeaveBalance?> GetByUserAndTypeAsync(string userId, int leaveTypeId, int year);

        Task AddAsync(LeaveBalance balance);

        Task UpdateAsync(LeaveBalance balance);

        Task SaveChangesAsync();
    }
}