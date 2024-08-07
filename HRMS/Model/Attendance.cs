namespace HRMS.Model
{
    public class AttendanceModel
    {
        public int Id { get; set; }
        public int Sno { get; set; }
        public int EmpId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? date { get; set; }
        public string PunchIn { get; set; }
        public string PunchOut { get; set; }
        public string WorkingHours { get; set; }
        public bool IsActive { get; set; }
    }
    public class AttendanceTotalPresent
    {
        public int TotalPresent { get; set; }
    }
}