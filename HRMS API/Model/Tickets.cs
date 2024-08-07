namespace HRMS_API.Model
{
    public class Ticket
    {
        public int ID { get; set; }
        public string TicketId { get; set; }
        public int EmpId { get; set; }
        public string TicketSubject { get; set; }
        public int TicketSubjectId { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Designation { get; set; }
        public DateTime RaiseDate { get; set; }
        public string Priority { get; set; }
        public int PriorityId { get; set; }

        public string Status { get; set; }
        public string Description { get; set; }
        public string AttachFile { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public byte[] FileContent3 { get; set; }
    }
	public class TicketCounts
    {
		public int NewTicket { get; set; }
		public int PendingTicket { get; set; }
		public int SolvedTicket { get; set; }
    }
    public class TicketSubjectList
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public int PriorityId { get; set; }
    }
    public class TicketPriority
    {
        public int ID { get; set; }
        public string Priority { get; set; }
    }
}
