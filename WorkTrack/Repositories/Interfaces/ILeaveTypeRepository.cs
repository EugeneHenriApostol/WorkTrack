using WorkTrack.Models;

namespace WorkTrack.Repositories.Interfaces
{
    public interface ILeaveTypeRepository
    {
        Task<List<LeaveType>> GetAllAsync();

        Task<LeaveType?> GetByIdAsync(int id);
    }
}