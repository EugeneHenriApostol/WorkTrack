using WorkTrack.Models.Enum;

namespace WorkTrack.Models
{
    public class LeaveType
    {
        public int Id { get; set; }
        public LeaveTypeEnum Leave { get; set; }
        public int DefaultDays { get; set; }
        public bool IsActive { get; set; }
    }
}
