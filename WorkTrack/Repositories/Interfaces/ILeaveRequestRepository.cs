using WorkTrack.Models;

namespace WorkTrack.Repositories.Interfaces
{
    public interface ILeaveRequestRepository
    {
        Task AddAsync(LeaveRequest leaveRequest);
        Task UpdateAsync(LeaveRequest leaveRequest);
        Task DeleteAsync(LeaveRequest leaveRequest);
        Task<LeaveRequest?> GetByIdAsync(int id);
        Task<List<LeaveRequest>> GetByUserIdAsync(string userId);
        Task<List<LeaveRequest>> GetPendingByManagerId(string managerId);
        Task<List<LeaveRequest>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
