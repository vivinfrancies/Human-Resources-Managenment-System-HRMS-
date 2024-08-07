using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRMS_API.Model;
using WebApplication1.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Text.RegularExpressions;

namespace HRMS_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LeaveController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public LeaveController(ModelContext dbContext, IConfiguration configuration)
		{
			_configuration = configuration;
		}

		#region Applying Leave
		[HttpPost]
		public async Task<ActionResult<Leave>> PostTodoItem(Leave todoDTO)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var sql = @"INSERT into LeaveDetails(EmpId,StartDate,EndDate,LeaveType,Reason,Detailedreason) 
                    VALUES (@Id,@Startdate,@Enddate,@LeaveType,@Reason,@Detailedreason)";
					using (var insertCommand = new SqlCommand(sql, appDbConnection))
					{
						insertCommand.Parameters.AddWithValue("@Id", todoDTO.Id);
						insertCommand.Parameters.AddWithValue("@Startdate", todoDTO.Startdate);
						insertCommand.Parameters.AddWithValue("@Enddate", todoDTO.Enddate);
						insertCommand.Parameters.AddWithValue("@LeaveType", todoDTO.LeaveType);
						insertCommand.Parameters.AddWithValue("@Reason", todoDTO.Reason);
						insertCommand.Parameters.AddWithValue("@Detailedreason", todoDTO.Detailedreason);
						await insertCommand.ExecuteNonQueryAsync();
					}
					return Ok("true");
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
		#endregion

		#region Particular Employee LeaveType Counts
		[HttpGet("chartviewsick/{EmployeeCode}")]
		public async Task<ActionResult<IEnumerable<Chartmodel>>> chartviewsick(string EmployeeCode)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var Leaves = new List<Chartmodel>();
					var sql = "exec LeaveDetailsType @EmployeeCode=@EmployeeCode";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{
						selectCommand.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								var project = new Chartmodel
								{
									Id = Convert.ToInt32(reader["ID"]),
									EmployeeCode = reader["EmployeeCode"].ToString(),
									Firstname = reader["Firstname"].ToString(),
									Role = reader["Role"].ToString(),
									Leavetype = Convert.ToInt32(reader["Leavetype"]),
									Leave = Convert.ToInt32(reader["TakenLeave"]),
									LeaveLimit = Convert.ToInt32(reader["LeaveLimit"]),
									LeaveTypename = reader["Leavetypename"].ToString()
								};
								Leaves.Add(project);
							}
						}
					}
					return Ok(Leaves);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
		#endregion

		#region Particular Employee Leave History
		[HttpGet("Employeelist/{EmpId}")]
		public async Task<ActionResult<ListModel>> Employeelist(int EmpId)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var leavedet = new List<ListModel>();
					var sql = @"SELECT [ID],[EmpId],[LeaveTypename],[StartDate],[EndDate],[Reason],[Status] FROM Leaveview WHERE EmpId=@EmpId";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{

						selectCommand.Parameters.AddWithValue("@EmpId", EmpId);
						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								var leavedetails = new ListModel
								{
									ID = Convert.ToInt32(reader["ID"]),
									EmpId = Convert.ToInt32(reader["EmpId"]),
									Leavetypename = reader["Leavetypename"].ToString(),
									StartDate = Convert.ToDateTime(reader["StartDate"]),
									EndDate = Convert.ToDateTime(reader["EndDate"]),
									Reason = reader["Reason"].ToString(),
									Status = reader["Status"].ToString()
								};

								leavedet.Add(leavedetails);

							}
							return Ok(leavedet);
						}
					}
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
		#endregion

		#region Update Status
		[HttpPut("Updatestatus/{ID:int}")]
		public async Task<ActionResult<CardModel>> Updatestatus(CardModel updatestatus)
		{
			var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
			using (var appconnection = new SqlConnection(appdbconnectionstring))
			{
				await appconnection.OpenAsync();
				try
				{
					var sql = @"update LeaveDetails set Status=@Status where ID=@ID and Status='Pending' ";
					using (var updatecmd = new SqlCommand(sql, appconnection))
					{
						updatecmd.Parameters.AddWithValue("@ID", updatestatus.ID);
						updatecmd.Parameters.AddWithValue("@Status", updatestatus.Status);
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
		#endregion

		#region Edit applied Leave
		[HttpPut("Editleave/{ID:int}")]
		public async Task<ActionResult<ListModel>> Editleave(ListModel editleave)
		{
			var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
			using (var appconnection = new SqlConnection(appdbconnectionstring))
			{
				await appconnection.OpenAsync();
				try
				{
					var sql = @"update LeaveDetails set Startdate=@Startdate,EndDate=@EndDate,Reason=@Reason,LeaveType=@leavetypeid where ID=@ID ";
					using (var editcmd = new SqlCommand(sql, appconnection))
					{
						editcmd.Parameters.AddWithValue("@ID", editleave.ID);
						editcmd.Parameters.AddWithValue("@StartDate", editleave.StartDate);
						editcmd.Parameters.AddWithValue("@EndDate", editleave.EndDate);
						editcmd.Parameters.AddWithValue("@Reason", editleave.Reason);
						editcmd.Parameters.AddWithValue("@leavetypeid", editleave.leavetypeid);
						await editcmd.ExecuteNonQueryAsync();
					}
					return Ok("true");
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
		#endregion

		#region leave Requests
		[HttpGet("Cards/{EmployeeCode}")]
		public async Task<ActionResult<CardModel>> Cards(string EmployeeCode)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");
			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var laeveaction = new List<CardModel>();
					var sql = @"SELECT [ID],[EmpId],[EmployeeCode],[FirstName],[LeaveTypename],[StartDate],[EndDate],[Reason],[Detailedreason],[Status],[Designation],[NoOfDays],[Role],[Wordstartdate],[WordEndDate] FROM Leaveview WHERE not EmployeeCode=@EmployeeCode AND Status = 'Pending'";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{
						selectCommand.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);

						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								var leavedetails = new CardModel
								{
									ID = Convert.ToInt32(reader["ID"]),
									EmpId = Convert.ToInt32(reader["EmpId"]),
									EmployeeCode = reader["EmployeeCode"].ToString(),
									FirstName = reader["FirstName"].ToString(),
									Leavetypename = reader["Leavetypename"].ToString(),
									StartDate = Convert.ToDateTime(reader["StartDate"]),
									EndDate = Convert.ToDateTime(reader["EndDate"]),
									Reason = reader["Reason"].ToString(),
									Detailedreason = reader["Detailedreason"].ToString(),
									Status = reader["Status"].ToString(),
									Designation = reader["Designation"].ToString(),
									NoOfDays = Convert.ToInt32(reader["NoOfDays"]),
									Role = Convert.ToInt32(reader["Role"]),
									wordstartdate = reader["Wordstartdate"].ToString(),
									wordenddate = reader["WordEndDate"].ToString()
								};
								laeveaction.Add(leavedetails);
							}
							return Ok(laeveaction);
						}
					}
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
		#endregion

		#region MonthlyLeave
		[HttpGet("MonthlyLeave")]
		public async Task<ActionResult<IEnumerable<monthlyleave>>> MonthlyLeave()
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();

				try
				{
					var monthLeave = new List<monthlyleave>();
					var sql = @"SELECT * FROM montheave";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{
						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								var monthcount = new monthlyleave
								{
									employeecode = reader["EmployeeCode"].ToString(),
									EmpId = Convert.ToInt32(reader["EmpId"]),
									monthleavecount = Convert.ToInt32(reader["MonthLeavecount"])

								};

								monthLeave.Add(monthcount);
							}
						}
					}
					return Ok(monthLeave);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
		#endregion

		#region Barchart
		[HttpGet("Barchart")]
		public async Task<ActionResult<IEnumerable<barchart>>> Barchart()
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();

				try
				{
					var dayLeave = new List<barchart>();
					var sql = @"SELECT * FROM DayCount";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{
						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								var daycount = new barchart
								{
									absent = Convert.ToInt32(reader["Absent"]),
									present = Convert.ToInt32(reader["Present"])

								};

								dayLeave.Add(daycount);
							}
						}
					}
					return Ok(dayLeave);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
		#endregion

		#region LeaveTypeDetails
		[HttpGet("LeaveTypeDetails")]
		public async Task<ActionResult<IEnumerable<LeaveTypeDetails>>> LeaveTypeDetails()
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();

				try
				{
					var monthLeave = new List<LeaveTypeDetails>();
					var sql = @"SELECT * FROM LeaveTypeDetails";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{
						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								var monthcount = new LeaveTypeDetails
								{
									Id = Convert.ToInt32(reader["ID"]),
									LeaveName = reader["Leavetypename"].ToString()

								};

								monthLeave.Add(monthcount);
							}
						}
					}
					return Ok(monthLeave);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
		#endregion

	}
}