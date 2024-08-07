using MudBlazor;

namespace HRMS.Model
{
    public class Tasks1
    {
        public int Id { get; set; }
        public int Sno { get; set; }
        public int ProjectID { get; set; }
        public int EmpId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
        public int Progress { get; set; }
        public string EmpCode { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public bool projectstatus { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
    }
}