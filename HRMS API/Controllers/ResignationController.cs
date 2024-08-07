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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace HRMS_API.Controllers
{
	[ApiController]
	[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]

	public class ResignationController : ControllerBase
	{
		private readonly ModelContext _context;
		private readonly IConfiguration _configuration;

		public ResignationController(ModelContext DbContext, IConfiguration configuration)
		{
			_context = DbContext;
			_configuration = configuration;
		}

		//POST API Function//


		[HttpPost]
		public async Task<ActionResult<ResignationTable>> PostResignationTable(ResignationTable ResignationTable)
		{
			var connectionString = _configuration.GetConnectionString("AppDbConnection");
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();

				try
				{
					// Insert into EmployeeDetails
					var insertResignationTable = @"
                    INSERT INTO ResignationTable ([EmpId], [LastDateOfWorking], [ReasonForResign], [AdditionalReasonForResign]) VALUES 
                       (@EmpId, @LastDateOfWorking, @ReasonForResign,@AdditionalReasonForResign);";

					using (SqlCommand cmd1 = new SqlCommand(insertResignationTable, conn))
					{
						// Adding parameters for Resignation
						cmd1.Parameters.AddWithValue("@EmpId", ResignationTable.EmpId);
						cmd1.Parameters.AddWithValue("@LastDateOfWorking", ResignationTable.LastDateOfWorking);
						cmd1.Parameters.AddWithValue("@ReasonForResign", ResignationTable.ReasonForResign);
						cmd1.Parameters.AddWithValue("@AdditionalReasonForResign", ResignationTable.AdditionalReasonForResign);


						await cmd1.ExecuteNonQueryAsync();
					}


					return Ok(ResignationTable);
				}

				catch (Exception ex)
				{

					Console.WriteLine(ex.ToString());
					return BadRequest("An error occurred while inserting the Resignation Table.");
				}

			}



		}

		[HttpGet]
		public async Task<ActionResult<ResignationTableview>> ResignationTableview()
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var ResignationTableview = new List<ResignationTableview>();
					var sql = @"SELECT [ID],[EmployeeCode],[FirstName],[DateOfJoining],[Designation],[LastDateOfWorking],[ReasonForResign],[AdditionalReasonForResign]FROM Resignationview WHERE  [IsActive]=1; ";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{


						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								// Populate the Employee object properties

								var Resignationview = new ResignationTableview
								{
									EmpId = Convert.ToInt32(reader["ID"]),
									EmployeeCode = reader["EmployeeCode"].ToString(),
									EmployeeName = reader["FirstName"].ToString(),
									Dateofjoining = Convert.ToDateTime(reader["DateOfJoining"]),
									LastDateOfWorking = Convert.ToDateTime(reader["LastDateOfWorking"]),
									Designation = reader["Designation"].ToString(),
									ReasonForResign = reader["ReasonForResign"].ToString(),
									AdditionalReasonForResign = reader["AdditionalReasonForResign"].ToString()

								};

								ResignationTableview.Add(Resignationview);

							}
							return Ok(ResignationTableview);
						}
						// Return the populated Employee object     
					}
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}

		[HttpGet("reasonview")]
		public async Task<ActionResult<Reason>> Reason()
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var Resignationreason = new List<Reason>();
					var sql = @"SELECT [id],[problem] FROM Reasons";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{


						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								// Populate the Employee object properties

								var Resignationreasonview = new Reason
								{
									id = Convert.ToInt32(reader["id"]),
									problem = reader["problem"].ToString(),


								};

								Resignationreason.Add(Resignationreasonview);

							}
							return Ok(Resignationreason);
						}
						// Return the populated Employee object     
					}
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
		//PUT API function///

		//PUT API function///

		[HttpPut("{EmpId}")]
		public async Task<ActionResult<Delete>> Put(int EmpId)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			try
			{
				using (var appDbConnection = new SqlConnection(appDbConnectionString))
				{
					await appDbConnection.OpenAsync();

					var sqlUpdateResignation = @"EXEC updatevalues @updateisactive=@EmpId;";


					using (var commandUpdateResignation = new SqlCommand(sqlUpdateResignation, appDbConnection))

					{
						commandUpdateResignation.Parameters.AddWithValue("@EmpId", EmpId);


						await commandUpdateResignation.ExecuteNonQueryAsync();

					}
				}

				return Ok(true);
			}
			catch (SqlException ex)
			{
				// Log exception (ex) here with your preferred logging framework
				return StatusCode(500, "A database error occurred.");
			}

		}

		/*  [HttpDelete("{EmpId}")]
		  public async Task<ActionResult<Delete>> Delete(int EmpId)
		  {
			  var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			  try
			  {
				  using (var appDbConnection = new SqlConnection(appDbConnectionString))
				  {
					  await appDbConnection.OpenAsync();

					  var sqlUpdateResignation = @"EXEC deletevalue @deleteid=@EmpId;";


					  using (var commandUpdateResignation = new SqlCommand(sqlUpdateResignation, appDbConnection))

					  {
						  commandUpdateResignation.Parameters.AddWithValue("@EmpId", EmpId);


						  await commandUpdateResignation.ExecuteNonQueryAsync();

					  }
				  }

				  return Ok(true);
			  }
			  catch (SqlException ex)
			  {
				  // Log exception (ex) here with your preferred logging framework
				  return StatusCode(500, "A database error occurred.");
			  }

		  }*/


		[HttpPut("Reject/{EmpId}")]
		public async Task<ActionResult<Delete>> Put1(int EmpId)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			try
			{
				using (var appDbConnection = new SqlConnection(appDbConnectionString))
				{
					await appDbConnection.OpenAsync();

					var sqlUpdateResignation = @"EXEC updatevalues1 @updateisactive1=@EmpId;";


					using (var commandUpdateResignation = new SqlCommand(sqlUpdateResignation, appDbConnection))

					{
						commandUpdateResignation.Parameters.AddWithValue("@EmpId", EmpId);


						await commandUpdateResignation.ExecuteNonQueryAsync();

					}
				}

				return Ok(true);
			}
			catch (SqlException ex)
			{
				// Log exception (ex) here with your preferred logging framework
				return StatusCode(500, "A database error occurred.");
			}

		}

	}
}