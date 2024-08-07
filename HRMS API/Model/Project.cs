using System.Drawing;

namespace HRMS_API.Model
{
    public class Project
    {
        public long Id { get; set; }
        public string ProjectId { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public int OpenTask { get; set; }
        public int NoOfTask { get; set; }
        public int CompletedTask { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime TaskDueDate { get; set; }
        public int ClientID { get; set; }
        public bool Status { get; set; }
        public int Role { get; set; }
        public string Roles { get; set; }
        public string Module { get; set; }
        public int EmpId { get; set; }
        public string TeamMembers { get; set; }
        public int Progress { get; set; }
        public string ClientName { get; set; }
        public bool IsActive { get; set; }
    }
}