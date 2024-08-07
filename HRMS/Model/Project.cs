using System.ComponentModel.DataAnnotations;

namespace HRMS.Model
{
    public class Project
    {
        public long Id { get; set; }
        public int Sno { get; set; }
        public string ProjectId { get; set; } = string.Empty;

        [Required]
        [StringLength(60, ErrorMessage = "Project Name Cannot be More Than 60 Letters")]
        public string ProjectName { get; set; } = string.Empty;
        public int OpenTask { get; set; }
        public int NoOfTask { get; set; }
        public int CompletedTask { get; set; }
        public string TaskName { get; set; } = string.Empty;

        [Required]
        [StringLength(120, ErrorMessage = "Description Cannot be More Than 120 Letters")]
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; } = DateTime.Today;
        public DateTime CreatedDate { get; set; } = DateTime.Today;
        public DateTime? TaskDueDate { get; set; } = DateTime.Today;
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
    public class Members
    {
        public string Name { get; set; }
    }
    public class Searchproject
    {
        public string Projectname { get; set; } = string.Empty;
    }
}