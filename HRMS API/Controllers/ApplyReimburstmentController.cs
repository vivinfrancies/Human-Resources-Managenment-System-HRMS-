using HRMS_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Model;

namespace HRMS_API.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class ApplyReimburstmentController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public ApplyReimburstmentController(ModelContext dbcontext, IConfiguration configuration)
		{
			_configuration = configuration;
		}


		//TO UPDATE THE INPUT VALUES IN DB (APPLY REIMBURSTMENT CLAIM)
		// POST: api/TodoItems
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost("Apply Reimbursement Claim")]
		public async Task<ActionResult<Reimbursement>> PostTodoItem(Reimbursement todoItem)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();

				try
				{
					var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
					if (!Directory.Exists(pathBuilt))
					{
						Directory.CreateDirectory(pathBuilt);
					}

					if (todoItem.FileContent3 != null)
					{
						var uniqueFileName3 = DateTime.Now.ToString("yyyyMMddHHmm") + "_" + todoItem.Bill;
						var filePath3 = Path.Combine(pathBuilt, uniqueFileName3);
						todoItem.Bill = uniqueFileName3;
						FileStream file3 = System.IO.File.Create(filePath3);
						file3.Write(todoItem.FileContent3, 0, todoItem.FileContent3.Length);
						file3.Close();
					}

					var sql = @"INSERT INTO  ReimbursementClaim (EmpId,[Role] ,Expense,BillNo,BillDate,BillAmount,Bill) VALUES (@EmpId,@Role,@Expense,@BillNo,@BillDate,@BillAmount,@Bill)";
					using (var insertCommand = new SqlCommand(sql, appDbConnection))
					{
						insertCommand.Parameters.AddWithValue("@EmpId", todoItem.EmpId);
						insertCommand.Parameters.AddWithValue("@Role", todoItem.Role);
						insertCommand.Parameters.AddWithValue("@Expense", todoItem.Expense);
						insertCommand.Parameters.AddWithValue("@BillNo", todoItem.BillNo);
						insertCommand.Parameters.AddWithValue("@BillDate", todoItem.BillDate);
						insertCommand.Parameters.AddWithValue("@BillAmount", todoItem.BillAmount);
						insertCommand.Parameters.AddWithValue("@Bill", todoItem.Bill);

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

		//



		// TO GET EMPLOYEE PEROSNAL REIMBURSTMENT HISTORY (PARAMATER)

		[HttpGet("PersonalClaimHistory/{EmpCode}")]
		public async Task<ActionResult<IEnumerable<Reimbursement>>> Get(string EmpCode)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var relist = new List<Reimbursement>();
					var sql = @"EXEC PersonalHistory @EmpCode = @EmpCode";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{
						selectCommand.Parameters.AddWithValue("@EmpCode", EmpCode);
						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								// Populate the Reimbursement history object properties
								var ReimbursementHistory = new Reimbursement
								{
									Id = Convert.ToInt32(reader["ID"]),
									BillNo = reader["BillNo"].ToString(),
									BillDate = Convert.ToDateTime(reader["BillDate"]),
									Expensetypes = reader["Expensetypes"].ToString(),
									Status = reader["Status"].ToString(),
									BillAmount = Convert.ToInt32(reader["BillAmount"])
								};
								relist.Add(ReimbursementHistory);
							}

							// Return the populated Reimbursement history list
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
		// To Get ExpenseType
		[HttpGet("Expense Types")]
		public async Task<ActionResult<Reimbursement>> GetExpenseType()
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var relist = new List<Reimbursement>();
					var sql = @"SELECT * FROM Expensetype";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{

						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								//
								var Expensetyp = new Reimbursement
								{
									Id = Convert.ToInt32(reader["ID"]),
									Expensetypes = reader["Expensetypes"].ToString()
								};
								relist.Add(Expensetyp);
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
	}
}