using HRMS_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Model;

namespace HRMS_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReimburstmentRequest : ControllerBase
	{

		private readonly IConfiguration _configuration;

		public ReimburstmentRequest(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		//TO VIEW Others Claim Request

		[HttpGet("Others Claim Requests{EmpCode}")]
		public async Task<ActionResult<IEnumerable<Reimbursement>>> Get(string EmpCode)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var relist = new List<Reimbursement>();
					var sql = @"SELECT * FROM ReimbursementRequests WHERE NOT EmployeeCode = @EmpCode";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{
						selectCommand.Parameters.AddWithValue("@EmpCode", EmpCode);
						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								// Populate the Reimbursement Request (Others) object properties
								var ReimbursementHistory = new Reimbursement
								{
									Id = Convert.ToInt32(reader["ID"]),
									EmpCode = reader["EmployeeCode"].ToString(),
									FirstName = reader["FirstName"].ToString(),
									Roles = reader["Role"].ToString(),
									Designation = reader["Designation"].ToString(),
									Expensetypes = reader["Expensetypes"].ToString(),
									BillNo = reader["BillNo"].ToString(),
									BillDate = Convert.ToDateTime(reader["BillDate"]),
									AppliedDate = Convert.ToDateTime(reader["CreatedDate"]),
									BillAmount = Convert.ToInt32(reader["BillAmount"]),
									Status = reader["Status"].ToString(),
									Bill = reader["Bill"].ToString()
								};
								relist.Add(ReimbursementHistory);
							}

							// Return the populated Reimbursement request list
							return Ok(relist);
						}
					}
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}


		// Reimburstment Request Approve 
		[HttpPut("Approve /{Id:int}")]
		public async Task<ActionResult<Reimbursement>> PutToItem(int Id)
		{
			var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
			using (var appconnection = new SqlConnection(appdbconnectionstring))
			{
				await appconnection.OpenAsync();
				try
				{
					var sql = @"UPDATE ReimbursementAllclaim SET ReimbursementAllclaim.Status = 'Approved' WHERE ReimbursementAllclaim.ID = @id";
					using (var updatecmd = new SqlCommand(sql, appconnection))
					{
						updatecmd.Parameters.AddWithValue("@id", Id);
						await updatecmd.ExecuteNonQueryAsync();
					}
					return Ok("true");
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}


		// Reimburstment Request Reject 
		[HttpPut("Reject / {Id:int}")]
		public async Task<ActionResult<Reimbursement>> Rejectitem(int Id)
		{
			var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
			using (var appconnection = new SqlConnection(appdbconnectionstring))
			{
				await appconnection.OpenAsync();
				try
				{
					var sql = @"UPDATE ReimbursementAllclaim SET ReimbursementAllclaim.Status = 'Rejected' , IsActive = 0 WHERE ReimbursementAllclaim.ID = @id";
					using (var updatecmd = new SqlCommand(sql, appconnection))
					{
						updatecmd.Parameters.AddWithValue("@id", Id);
						await updatecmd.ExecuteNonQueryAsync();
					}
					return Ok("true");
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
	}
}