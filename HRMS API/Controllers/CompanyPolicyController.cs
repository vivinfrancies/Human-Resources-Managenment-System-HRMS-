using HRMS_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Model;

namespace HRMS_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyPolicyController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public CompanyPolicyController(ModelContext dbcontext, IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// To View Leave Limit 
		[HttpGet("Leave Limit Table")]
		public async Task<ActionResult<Reimbursement>> GetLeaveLimit()
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var relist = new List<Reimbursement>();
					var sql = @"SELECT * FROM Leavepolicy";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{

						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								//
								var Companypolicy = new Reimbursement
								{
									Id = Convert.ToInt32(reader["ID"]),
									Roles = reader["Role"].ToString(),
									LeaveLimit = Convert.ToInt32(reader["LeaveLimit"])

								};
								relist.Add(Companypolicy);
							}

							// 
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

		/// T0 VIEW Expense Limit Table
		[HttpGet("Expense Limit Table")]
		public async Task<ActionResult<Reimbursement>> GetExpenseLimit()
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var relist = new List<Reimbursement>();
					var sql = @"SELECT * FROM Expensepolicy";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{

						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								//
								var Companypolicy = new Reimbursement
								{
									Id = Convert.ToInt32(reader["ID"]),
									Roles = reader["Role"].ToString(),
									InternetLimit = Convert.ToInt32(reader["InternetLimit"]),
									TravelLimit = Convert.ToInt32(reader["TravelLimit"])

								};
								relist.Add(Companypolicy);
							}

							// 
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


		// Update expense Limit 
		[HttpPut("Update Expense Limit /{Id:int}")]
		public async Task<ActionResult<Reimbursement>> UpdateExpenseLimit(int Id, int internetLimit, int travelLimit, int modifiedBy)
		{
			var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
			using (var appconnection = new SqlConnection(appdbconnectionstring))
			{
				await appconnection.OpenAsync();
				try
				{
					var sql = @"UPDATE CompanyPolicy SET CompanyPolicy.InternetLimit = @InternetLimit , CompanyPolicy.TravelLimit = @TravelLimit , CompanyPolicy.ModifiedBy = @ModifiedBy , CompanyPolicy.ModifiedDate = CONVERT(DATE , GETDATE()) WHERE CompanyPolicy.ID = @Id";
					using (var updatecmd = new SqlCommand(sql, appconnection))
					{
						updatecmd.Parameters.AddWithValue("@InternetLimit", internetLimit);
						updatecmd.Parameters.AddWithValue("@TravelLimit", travelLimit);
						updatecmd.Parameters.AddWithValue("@ModifiedBy", modifiedBy);
						updatecmd.Parameters.AddWithValue("@id", Id);
						await updatecmd.ExecuteNonQueryAsync();
					}
					return Ok("Update successful");
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}



		// Update leave limit
		[HttpPut("Update Leave Limit / {Id:int}")]
		public async Task<ActionResult<Reimbursement>> UpdateLeaveLimit(int Id, int leaveLimit, int modifiedBy)
		{
			var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
			using (var appconnection = new SqlConnection(appdbconnectionstring))
			{
				await appconnection.OpenAsync();
				try
				{
					var sql = @"UPDATE CompanyPolicy SET CompanyPolicy.LeaveLimit = @LeaveLimit  ,CompanyPolicy.ModifiedBy = @ModifiedBy , CompanyPolicy.ModifiedDate = CONVERT(DATE , GETDATE()) WHERE CompanyPolicy.ID = @Id";
					using (var updatecmd = new SqlCommand(sql, appconnection))
					{
						updatecmd.Parameters.AddWithValue("@LeaveLimit", leaveLimit);
						updatecmd.Parameters.AddWithValue("@ModifiedBy", modifiedBy);
						updatecmd.Parameters.AddWithValue("@id", Id);
						await updatecmd.ExecuteNonQueryAsync();
					}
					return Ok("Update successful");
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
	}
}