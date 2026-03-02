using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkTrack.Models;

namespace WorkTrack.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {}

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // relationship: LeaveRequest -> User (request creator)
            builder.Entity<LeaveRequest>()
                .HasOne(lr => lr.User)
                .WithMany(u =>  u.LeaveRequests)
                .HasForeignKey(lr => lr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // relationship: LeaveRequest -> ApprovedBy (Approver
            builder.Entity<LeaveRequest>()
                .HasOne(lr => lr.ApprovedBy)
                .WithMany(u => u.ApprovedLeaveRequests)
                .HasForeignKey(lr => lr.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
