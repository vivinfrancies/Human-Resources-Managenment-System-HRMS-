namespace HRMS_API.Model
{
    public class Dashboard
    {
        public int Id { get; set; }
        public int TotalEntry { get; set; }
        public string Leavedetails { get; set; }
        public int EmpLeave { get; set; }
        public int EmpTotalLeave { get; set; }
        public int TotalProject { get; set; }
        public int TotalClient { get; set; }
        public int TotalEmployee { get; set; }
        public string CompleteProject { get; set; }
        public int GenderCount { get; set; }
        public string EmpGender { get; set; }
        public string EOMName { get; set; }
        public string EOMDesignation { get; set; }
        public int Completedproject { get; set; }
        public string ProjectName { get; set; }
        public DateTime DueDate { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventTimeIn { get; set; }
        public DateTime EventTimeOut { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public int ProjectCompleteCount { get; set; }
        public int ProjectOnprocessCount { get; set; }
        public string ProjectStatus { get; set; }
        public int TodayLeaveCount { get; set; }
        public string TodayLeaveStatus { get; set; }

    }
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmpCode { get; set; }
        public string Image { get; set; }
    }
}