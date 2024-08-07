using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace HRMS_API.Model
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public DbSet<HRMS_API.Model.Project> TodoItems { get; set; } = null!;
        public DbSet<HRMS_API.Model.Tasks> Task { get; set; } = default!;
        public DbSet<HRMS_API.Model.Holiday> Holiday { get; set; } = default!;
        public DbSet<HRMS_API.Model.EditSalary> EditSalary { get; set; }
        public DbSet<HRMS_API.Model.Payslipdetails> Payslipdetails { get; set; }
        public DbSet<HRMS_API.Model.Ticket> Ticket { get; set; }
        public DbSet<HRMS_API.Model.Leave> Leave { get; set; }
        public DbSet<HRMS_API.Model.Reimbursement> Reimbursement { get; set; }
        public DbSet<HRMS_API.Model.Profile> Profile { get; set; }
        public DbSet<HRMS_API.Model.LoginDetails> LoginDetails { get; set; }
        public DbSet<HRMS_API.Model.Dashboard> Dashboard { get; set; }
        public DbSet<HRMS_API.Model.AttendanceModel> Attendances { get; set; }
        public DbSet<HRMS_API.Model.EmployeeDetailsTable> EmployeeDetailsTable { get; set; }
    }
}
