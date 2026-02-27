using WorkTrack.Models;
using WorkTrack.Models.Enum;
using WorkTrack.Repositories.Interfaces;
using WorkTrack.Services.Interfaces;

namespace WorkTrack.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveService(ILeaveRequestRepository leaveRequestRepository,
                            ILeaveBalanceRepository leaveBalanceRepository,
                            ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveBalanceRepository = leaveBalanceRepository;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task SubmitLeaveRequestsAsync(LeaveRequest request)
        {
            var year = DateTime.UtcNow.Year;

            var balance = await _leaveBalanceRepository.GetByUserAndTypeAsync(request.UserId, request.LeaveTypeId, year);

            if (balance == null)
            {
                var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

                balance = new LeaveBalance
                {
                    UserId = request.UserId,
                    LeaveTypeId = request.LeaveTypeId,
                    TotalDays = leaveType!.DefaultDays,
                    UsedDays = 0,
                    Year = year
                };

                await _leaveBalanceRepository.AddAsync(balance);
            }

            if (balance.RemainingDays < balance.TotalDays)
            {
                throw new Exception("Not enough balance");
            }

            request.Status = LeaveStatus.Pending;
            request.CreatedAt = DateTime.UtcNow;

            await _leaveRequestRepository.AddAsync(request);
            
            await _leaveBalanceRepository.SaveChangesAsync();
            await _leaveRequestRepository.SaveChangesAsync();
        }

        public async Task CancelLeaveRequestAsync(int requestId, string userId)
        {
            var request = await _leaveRequestRepository.GetByIdAsync(requestId);

            if (request == null)
            {
                throw new Exception("Leave request not found");
            }

            if (request.UserId !=  userId)
            {
                throw new Exception("You can only cancel your own leave request");
            }

            if (request.Status != LeaveStatus.Pending)
            {
                throw new Exception("Only pending leave requests can be cancelled");
            }

            request.Status = LeaveStatus.Cancelled;
            request.CancelledAt = DateTime.UtcNow;

            await _leaveRequestRepository.UpdateAsync(request);
            await _leaveRequestRepository.SaveChangesAsync();
        }

        public async Task<List<LeaveRequest>> GetUserLeaveRequestAsync(string userId)
        {
            return await _leaveRequestRepository.GetByUserIdAsync(userId);
        }
    }
}
