using WorkTrack.Models;

namespace WorkTrack.Services.Interfaces
{
    public interface IApprovalService
    {
        Task<List<LeaveRequest>> GetPendingRequestsAsync(string managerId);
        Task ApproveAsync(int leaveRequestId, string managerId);
        Task RejectAsync(int leaveRequestId, string managerId);
    }
}
