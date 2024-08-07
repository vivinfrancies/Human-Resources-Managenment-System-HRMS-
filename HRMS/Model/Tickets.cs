namespace HRMS.Model
{
    public class Ticket
    {
        public int ID { get; set; }
        public string TicketId { get; set; }
        public int EmpId { get; set; }
        public string TicketSubject { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Designation { get; set; }
        public DateTime RaiseDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string AttachFile { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
