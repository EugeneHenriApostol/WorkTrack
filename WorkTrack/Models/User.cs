using Microsoft.AspNetCore.Identity;

namespace WorkTrack.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public string? Department { get; set; }

        public string? ManagerId { get; set; }

        public User? Manager { get; set; }

        // Requests created by this user
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

        // Requests approved by this user
        public ICollection<LeaveRequest> ApprovedLeaveRequests { get; set; } = new List<LeaveRequest>();
    }
}
