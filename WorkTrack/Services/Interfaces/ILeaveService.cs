using WorkTrack.Models;
using WorkTrack.Repositories.Interfaces;

namespace WorkTrack.Services.Interfaces
{
    public interface ILeaveService
    {
        Task SubmitLeaveRequestAsync(LeaveRequest request);
        Task<List<LeaveRequest>> GetUserLeaveRequestAsync(string userId);
    }
}
