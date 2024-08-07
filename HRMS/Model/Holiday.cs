namespace HRMS.Model
{
	public class Holiday
	{
		public int Id { get; set; }
		public int Sno { get; set; }
		public string HolidayName { get; set; }
		public string HolidayDate { get; set; }
		public string HolidayDay { get; set; }
		public DateTime? HolidayNewDate { get; set; } = DateTime.Today;

	}
}