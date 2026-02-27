using WorkTrack.Models;
using WorkTrack.Models.Enum;
using WorkTrack.Repositories.Interfaces;
using WorkTrack.Services.Interfaces;

namespace WorkTrack.Services
{
    public class ApprovalService : IApprovalService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepo;
        private readonly ILeaveBalanceRepository _leaveBalanceRepo;

        public ApprovalService(ILeaveRequestRepository leaveRequestRepo,
                                ILeaveBalanceRepository leaveBalanceRepo)
        {
            _leaveRequestRepo = leaveRequestRepo;
            _leaveBalanceRepo = leaveBalanceRepo;
        }

        public async Task<List<LeaveRequest>> GetPendingRequestsAsync(string managerId)
        {
            return await _leaveRequestRepo.GetPendingByManagerId(managerId);
        }

        public async Task ApproveAsync(int leaveRequestId, string managerId)
        {
            var request = await _leaveRequestRepo.GetByIdAsync(leaveRequestId);

            if (request == null)
            {
                throw new Exception("Leave request not found");
            }

            if (request.Status != LeaveStatus.Pending)
            {
                throw new Exception("Only pending requests can be approved");
            }

            var year = request.StartDate.Year;

            var balance = await _leaveBalanceRepo.GetByUserAndTypeAsync(request.UserId, request.LeaveTypeId, year);

            if (balance == null)
            {
                throw new Exception("Leave balance not found");
            }

            if (balance.RemainingDays < balance.TotalDays)
            {
                throw new Exception("Insufficient balance leave");
            }

            // update leave balance
            balance.UsedDays += request.TotalDays;

            // update leave request
            request.Status = LeaveStatus.Approved;
            request.ApprovedById = managerId;
            request.ReviewedAt = DateTime.UtcNow;

            await _leaveBalanceRepo.UpdateAsync(balance);
            await _leaveRequestRepo.UpdateAsync(request);

            await _leaveBalanceRepo.SaveChangesAsync();
            await _leaveRequestRepo.SaveChangesAsync();
        }

        public async Task RejectAsync(int leaveRequestId, string managerId)
        {
            var request = await _leaveRequestRepo.GetByIdAsync(leaveRequestId);

            if (request == null)
            {
                throw new Exception("Leave request not found");
            }

            if (request.Status != LeaveStatus.Pending)
            {
                throw new Exception("Only pending requests can be rejected");
            }

            request.Status = LeaveStatus.Rejected;
            request.ApprovedById = managerId;
            request.ReviewedAt = DateTime.UtcNow;

            await _leaveRequestRepo.UpdateAsync(request);
            await _leaveRequestRepo.SaveChangesAsync();
        }
    }
}
