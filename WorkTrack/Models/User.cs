using Microsoft.AspNetCore.Identity;

namespace WorkTrack.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public string? Department { get; set; }

        public string? ManagerId { get; set; }

        public User? Manager { get; set; }

        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
    }
}
