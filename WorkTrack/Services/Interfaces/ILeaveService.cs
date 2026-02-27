using WorkTrack.Models;
using WorkTrack.Repositories.Interfaces;

namespace WorkTrack.Services.Interfaces
{
    public interface ILeaveService
    {
        Task SubmitLeaveRequestsAsync(LeaveRequest request);
        Task CancelLeaveRequestAsync(int requestId, string userId);
        Task<List<LeaveRequest>> GetUserLeaveRequestAsync(string userId);
    }
}
