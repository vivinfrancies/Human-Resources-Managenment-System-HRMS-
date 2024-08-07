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

namespace HRMS_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeDetailsTableController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public EmployeeDetailsTableController(ModelContext dbContext, IConfiguration configuration)
		{
			_configuration = configuration;
		}
		//POST API Function//

		[HttpPost]
		public async Task<ActionResult<EmployeeDetailsTable>> PostEmployeeDetails(EmployeeDetailsTable employeeDetails)
		{
			var connectionString = _configuration.GetConnectionString("AppDbConnection");
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();

				try
				{
					var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

					if (!Directory.Exists(pathBuilt))
					{
						Directory.CreateDirectory(pathBuilt);
					}
					if (employeeDetails.FileContent3 != null)
					{
						var uniqueFileName3 = DateTime.Now.ToString("yyyyMMddHHmm") + "_" + employeeDetails.ProfileImage;
						var filePath3 = Path.Combine(pathBuilt, uniqueFileName3);
						employeeDetails.ProfileImage = uniqueFileName3;
						FileStream file3 = System.IO.File.Create(filePath3);
						file3.Write(employeeDetails.FileContent3, 0, employeeDetails.FileContent3.Length);
						file3.Close();
					}
					// Insert into EmployeeDetails
					var insertEmployeeDetailsQuery = @"
                    INSERT INTO EmployeeDetails (
                        [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], 
                        [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [Religion], [Nationality], 
                        [MaritalStatus], [ProfileImage]
                    ) VALUES (
                        @FirstName, @MiddleName, @LastName, @Email, @MobileNumber, @Address, @City, @PinCode, @State, 
                        @Country, @Role, @Designation, @Experience, @Gender, @DateOfBirth, @Religion, @Nationality, 
                        @MaritalStatus, @ProfileImage
                    );
                    SELECT SCOPE_IDENTITY();";

					using (SqlCommand cmd1 = new SqlCommand(insertEmployeeDetailsQuery, conn))
					{
						// Adding parameters for EmployeeDetails
						cmd1.Parameters.AddWithValue("@FirstName", employeeDetails.FirstName);
						cmd1.Parameters.AddWithValue("@MiddleName", employeeDetails.MiddleName);
						cmd1.Parameters.AddWithValue("@LastName", employeeDetails.LastName);
						cmd1.Parameters.AddWithValue("@Email", employeeDetails.Email);
						cmd1.Parameters.AddWithValue("@MobileNumber", employeeDetails.MobileNumber);
						cmd1.Parameters.AddWithValue("@Address", employeeDetails.Address);
						cmd1.Parameters.AddWithValue("@City", employeeDetails.City);
						cmd1.Parameters.AddWithValue("@PinCode", employeeDetails.PinCode);
						cmd1.Parameters.AddWithValue("@State", employeeDetails.State);
						cmd1.Parameters.AddWithValue("@Country", employeeDetails.Country);
						cmd1.Parameters.AddWithValue("@Role", employeeDetails.Role);
						cmd1.Parameters.AddWithValue("@Designation", employeeDetails.Designation);
						cmd1.Parameters.AddWithValue("@Experience", employeeDetails.Experience);
						cmd1.Parameters.AddWithValue("@Gender", employeeDetails.Gender);
						cmd1.Parameters.AddWithValue("@DateOfBirth", employeeDetails.DateOfBirth);
						cmd1.Parameters.AddWithValue("@Religion", employeeDetails.Religion);
						cmd1.Parameters.AddWithValue("@Nationality", employeeDetails.Nationality);
						cmd1.Parameters.AddWithValue("@MaritalStatus", employeeDetails.MaritalStatus);
						cmd1.Parameters.AddWithValue("@ProfileImage", employeeDetails.ProfileImage);



						// Execute the command and retrieve the EmpId
						var empId = Convert.ToInt32(await cmd1.ExecuteScalarAsync());

						// Insert into EmployeePersonalDetails
						var insertEmployeePersonalDetailsQuery = @"
                        INSERT INTO EmployeePersonalDetails (
                            [EmpId], [PgCollegeName], [PgCourse], [PgStart], [PgEnd], [UgCollageName], [UgCourse], 
                            [UgStart], [UgEnd], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], 
                            [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship]
                        ) VALUES (
                            @EmpId, @PgCollegeName, @PgCourse, @PgStart, @PgEnd, @UgCollageName, @UgCourse, 
                            @UgStart, @UgEnd, @HSCSchoolName, @HSCStart, @HSCEnd, @HSCCourse, @SSLCSchoolName, 
                            @SSLCStart, @SSLCEnd, @EmergencyContact, @Relationship
                        );";

						using (SqlCommand cmd2 = new SqlCommand(insertEmployeePersonalDetailsQuery, conn))
						{
							// Adding parameters for EmployeePersonalDetails
							cmd2.Parameters.AddWithValue("@EmpId", empId);
							cmd2.Parameters.AddWithValue("@PgCollegeName", employeeDetails.PgCollegeName);
							cmd2.Parameters.AddWithValue("@PgCourse", employeeDetails.PgCourse);
							cmd2.Parameters.AddWithValue("@PgStart", employeeDetails.PgStart);
							cmd2.Parameters.AddWithValue("@PgEnd", employeeDetails.PgEnd);
							cmd2.Parameters.AddWithValue("@UgCollageName", employeeDetails.UgCollageName);
							cmd2.Parameters.AddWithValue("@UgCourse", employeeDetails.UgCourse);
							cmd2.Parameters.AddWithValue("@UgStart", employeeDetails.UgStart);
							cmd2.Parameters.AddWithValue("@UgEnd", employeeDetails.UgEnd);
							cmd2.Parameters.AddWithValue("@HSCSchoolName", employeeDetails.HSCSchoolName);
							cmd2.Parameters.AddWithValue("@HSCStart", employeeDetails.HSCStart);
							cmd2.Parameters.AddWithValue("@HSCEnd", employeeDetails.HSCEnd);
							cmd2.Parameters.AddWithValue("@HSCCourse", employeeDetails.HSCCourse);
							cmd2.Parameters.AddWithValue("@SSLCSchoolName", employeeDetails.SSLCSchoolName);
							cmd2.Parameters.AddWithValue("@SSLCStart", employeeDetails.SSLCStart);
							cmd2.Parameters.AddWithValue("@SSLCEnd", employeeDetails.SSLCEnd);
							cmd2.Parameters.AddWithValue("@EmergencyContact", employeeDetails.EmergencyContact);
							cmd2.Parameters.AddWithValue("@Relationship", employeeDetails.Relationship);

							await cmd2.ExecuteNonQueryAsync();
						}

						// Insert into EmployeeAccountDetails
						var insertEmployeeAccountDetailsQuery = @"
                        INSERT INTO EmployeeAccountDetails (
                            [EmpId], [Salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount]
                        ) VALUES (
                            @EmpId, @Salary, @BankName, @AccountNumber, @IFSCCode, @PFAccount
                        );";

						using (SqlCommand cmd3 = new SqlCommand(insertEmployeeAccountDetailsQuery, conn))
						{
							// Adding parameters for EmployeeAccountDetails
							cmd3.Parameters.AddWithValue("@EmpId", empId);
							cmd3.Parameters.AddWithValue("@Salary", employeeDetails.Salary);
							cmd3.Parameters.AddWithValue("@BankName", employeeDetails.BankName);
							cmd3.Parameters.AddWithValue("@AccountNumber", employeeDetails.AccountNumber);
							cmd3.Parameters.AddWithValue("@IFSCCode", employeeDetails.IFSCCode);
							cmd3.Parameters.AddWithValue("@PFAccount", employeeDetails.PFAccount);

							await cmd3.ExecuteNonQueryAsync();
						}


						return Ok(employeeDetails);
					}
				}
				catch (Exception ex)
				{

					Console.WriteLine(ex.ToString());
					return BadRequest("An error occurred while inserting the employee details.");
				}

			}
		}
		[HttpGet]
		public async Task<ActionResult<EmployeeList>> EmployeeList()
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var EmployeeList = new List<EmployeeList>();
					var sql = @"SELECT [ID],[EmployeeCode],[FirstName],[MiddleName],[LastName],[Email],[MobileNumber],[Designation],[DesignationId],[Role],[Experience],[DateOfJoining],[DateOfBirth],[Address],[City],[Country],[State],[PinCode],[Gender],[Religion],[Nationality],[MaritalStatus],[ProfileImage],[PgCollegeName],[PgCourse],[PgStart],[PgEnd],[UgCollageName],[UgCourse],[UgStart],[UgEnd],[HSCSchoolName],[HSCCourse], [HSCStart],[HSCEnd], [SSLCSchoolName], [SSLCStart],[SSLCEnd],[EmergencyContact],[Relationship], [BankName],[salary],[AccountNumber],[IFSCCode],[PFAccount] FROM EmployeeBasicDetails WHERE  [IsActive]=1; ";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{


						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								// Populate the Employee object properties
								var Employeeview = new EmployeeList
								{
									EmpId = Convert.ToInt32(reader["ID"]),
									Employeecode = reader["EmployeeCode"].ToString(),
									Firstname1 = reader["FirstName"].ToString(),
									Middlename1 = reader["MiddleName"].ToString(),
									Lastname1 = reader["LastName"].ToString(),
									Email1 = reader["Email"].ToString(),
									Mobilenumber1 = reader["MobileNumber"].ToString(),
									Address1 = reader["Address"].ToString(),
									City1 = reader["City"].ToString(),
									Pincode1 = reader["PinCode"].ToString(),
									State1 = reader["State"].ToString(),
									Country1 = reader["Country"].ToString(),
									Role1 = reader["Role"].ToString(),
									Designation1 = reader["Designation"].ToString(),
									DesignationId = Convert.ToInt32(reader["DesignationId"]),
									Experience = Convert.ToInt32(reader["Experience"]),
									Gender1 = reader["Gender"].ToString(),
									Dateofbirth1 = Convert.ToDateTime(reader["DateOfBirth"]),
									Dateofjoining = Convert.ToDateTime(reader["DateOfJoining"]),
									Religion1 = reader["Religion"].ToString(),
									Nationality1 = reader["Nationality"].ToString(),
									Maritalstatus1 = reader["MaritalStatus"].ToString(),
									Profileimage1 = reader["ProfileImage"].ToString(),
									Pgcollegename1 = reader["PgCollegeName"].ToString(),
									Pgstartdate1 = Convert.ToDateTime(reader["PgStart"]),
									Pgenddate1 = Convert.ToDateTime(reader["PgEnd"]),
									Pgcoursename1 = reader["PgCourse"].ToString(),
									Ugcollagename1 = reader["UgCollageName"].ToString(),
									Ugstartdate1 = Convert.ToDateTime(reader["UgStart"]),
									Ugenddate1 = Convert.ToDateTime(reader["UgEnd"]),
									Ugcoursename1 = reader["UgCourse"].ToString(),
									HSCschoolname1 = reader["HSCSchoolName"].ToString(),
									HSCstartdate1 = Convert.ToDateTime(reader["HSCStart"]),
									HSCenddate1 = Convert.ToDateTime(reader["HSCEnd"]),
									HSCcoursename1 = reader["HSCCourse"].ToString(),
									SSLCschoolname1 = reader["SSLCSchoolName"].ToString(),
									SSLCstartdate1 = Convert.ToDateTime(reader["SSLCStart"]),
									SSLCenddate1 = Convert.ToDateTime(reader["SSLCEnd"]),
									Emergencycontact1 = reader["EmergencyContact"].ToString(),
									Relationship1 = reader["Relationship"].ToString(),
									Salary1 = reader["salary"].ToString(),
									Bankname1 = reader["BankName"].ToString(),
									Accountnumber1 = reader["AccountNumber"].ToString(),
									Ifsccode1 = reader["IFSCCode"].ToString(),
									Pfaccount1 = reader["PFAccount"].ToString(),
								};
								EmployeeList.Add(Employeeview);
							}
							return Ok(EmployeeList);
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

		[HttpGet("Employeeoveralldetails/{EmployeeCode}")]
		public async Task<ActionResult<EmployeeList>> Employeeoveralldetails(string EmployeeCode)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var EmployeeList = new List<EmployeeList>();
					var sql = @"SELECT [ID],[EmployeeCode],[FirstName],[MiddleName],[LastName],[Email],[MobileNumber],[Designation],[Role],[Experience],[DateOfJoining],[DateOfBirth],[Address],[City],[Country],[State],[PinCode],[Gender],[Religion],[Nationality],[MaritalStatus],[ProfileImage],[PgCollegeName],[PgCourse],[PgStart],[PgEnd],[UgCollageName],[UgCourse],[UgStart],[UgEnd],[HSCSchoolName],[HSCCourse], [HSCStart],[HSCEnd], [SSLCSchoolName], [SSLCStart],[SSLCEnd],[EmergencyContact],[Relationship], [BankName],[salary],[AccountNumber],[IFSCCode],[PFAccount] FROM EmployeeBasicDetails WHERE EmployeeCode=@EmployeeCode AND [IsActive]=1; ";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{

						selectCommand.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{

								// Populate the Employee object properties
								var Employeeview = new EmployeeList
								{
									EmpId = Convert.ToInt32(reader["ID"]),
									Employeecode = reader["EmployeeCode"].ToString(),
									Firstname1 = reader["FirstName"].ToString(),
									Middlename1 = reader["MiddleName"].ToString(),
									Lastname1 = reader["LastName"].ToString(),
									Email1 = reader["Email"].ToString(),
									Mobilenumber1 = reader["MobileNumber"].ToString(),
									Address1 = reader["Address"].ToString(),
									City1 = reader["City"].ToString(),
									Pincode1 = reader["PinCode"].ToString(),
									State1 = reader["State"].ToString(),
									Country1 = reader["Country"].ToString(),
									Role1 = reader["Role"].ToString(),
									Designation1 = reader["Designation"].ToString(),
									Experience = Convert.ToInt32(reader["Experience"]),
									Gender1 = reader["Gender"].ToString(),
									Dateofbirth1 = Convert.ToDateTime(reader["DateOfBirth"]),
									Dateofjoining = Convert.ToDateTime(reader["DateOfJoining"]),
									Religion1 = reader["Religion"].ToString(),
									Nationality1 = reader["Nationality"].ToString(),
									Maritalstatus1 = reader["MaritalStatus"].ToString(),
									Profileimage1 = reader["ProfileImage"].ToString(),
									Pgcollegename1 = reader["PgCollegeName"].ToString(),
									Pgstartdate1 = Convert.ToDateTime(reader["PgStart"]),
									Pgenddate1 = Convert.ToDateTime(reader["PgEnd"]),
									Pgcoursename1 = reader["PgCourse"].ToString(),
									Ugcollagename1 = reader["UgCollageName"].ToString(),
									Ugstartdate1 = Convert.ToDateTime(reader["UgStart"]),
									Ugenddate1 = Convert.ToDateTime(reader["UgEnd"]),
									Ugcoursename1 = reader["UgCourse"].ToString(),
									HSCschoolname1 = reader["HSCSchoolName"].ToString(),
									HSCstartdate1 = Convert.ToDateTime(reader["HSCStart"]),
									HSCenddate1 = Convert.ToDateTime(reader["HSCEnd"]),
									HSCcoursename1 = reader["HSCCourse"].ToString(),
									SSLCschoolname1 = reader["SSLCSchoolName"].ToString(),
									SSLCstartdate1 = Convert.ToDateTime(reader["SSLCStart"]),
									SSLCenddate1 = Convert.ToDateTime(reader["SSLCEnd"]),
									Emergencycontact1 = reader["EmergencyContact"].ToString(),
									Relationship1 = reader["Relationship"].ToString(),
									Salary1 = reader["salary"].ToString(),
									Bankname1 = reader["BankName"].ToString(),
									Accountnumber1 = reader["AccountNumber"].ToString(),
									Ifsccode1 = reader["IFSCCode"].ToString(),
									Pfaccount1 = reader["PFAccount"].ToString(),
								};
								EmployeeList.Add(Employeeview);
							}
							return Ok(EmployeeList);
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
		[HttpPut("{EmpId:int}")]
		public async Task<ActionResult<EmployeeList>> Put(EmployeeList EditEmployee)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();

				var sql = @"UPDATE EmployeeDetails SET FirstName=@Firstname1,LastName=@Lastname1,Email=@Email1,MobileNumber=@Mobilenumber1,Experience=@Experience,MaritalStatus=@Maritalstatus1 WHERE ID=@EmpId";


				using (var selectCommand = new SqlCommand(sql, appDbConnection))
				{

					selectCommand.Parameters.AddWithValue("@EmpId", EditEmployee.EmpId);
					selectCommand.Parameters.AddWithValue("@Firstname1", EditEmployee.Firstname1);
					selectCommand.Parameters.AddWithValue("@Lastname1", EditEmployee.Lastname1);
					selectCommand.Parameters.AddWithValue("@Email1", EditEmployee.Email1);
					selectCommand.Parameters.AddWithValue("@Mobilenumber1", EditEmployee.Mobilenumber1);
					
				    selectCommand.Parameters.AddWithValue("@Experience", EditEmployee.Experience);
				    selectCommand.Parameters.AddWithValue("@Maritalstatus1", EditEmployee.Maritalstatus1);
				




					await selectCommand.ExecuteNonQueryAsync();
					// Return the populated Employee object
					return Ok(true);
				}

			}
		}

		[HttpPut("Edit1/{EmpId:int}")]
		public async Task<ActionResult<EmployeeList>> Put1(EmployeeList EditEmployee)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();

				var sql = @"UPDATE EmployeePersonalDetails SET PgCollegeName=@Pgcollegename1,PgCourse=@Pgcoursename1,PgStart=@Pgstartdate1,PgEnd=@Pgenddate1,UgCollageName=@Ugcollagename1,UgCourse=@Ugcollagename1,UgStart=@Ugstartdate1,UgEnd=@Ugenddate1,HSCSchoolName=@HSCschoolname1,HSCCourse=@HSCcoursename1,HSCStart=@HSCstartdate1,HSCEnd=@HSCenddate1,SSLCSchoolName=@SSLCschoolname1,SSLCStart=@SSLCstartdate1,SSLCEnd=@SSLCenddate1,EmergencyContact=@Emergencycontact1,Relationship=@Relationship1 WHERE EmpId=@EmpId";


				using (var selectCommand = new SqlCommand(sql, appDbConnection))
				{

					selectCommand.Parameters.AddWithValue("@EmpId", EditEmployee.EmpId);

					selectCommand.Parameters.AddWithValue("@Pgcollegename1", EditEmployee.Pgcollegename1);
					selectCommand.Parameters.AddWithValue("@Pgcoursename1", EditEmployee.Pgcoursename1);
					selectCommand.Parameters.AddWithValue("@Pgstartdate1", EditEmployee.Pgstartdate1);
					selectCommand.Parameters.AddWithValue("@Pgenddate1", EditEmployee.Pgenddate1);
					selectCommand.Parameters.AddWithValue("@Ugcollagename1", EditEmployee.Ugcollagename1);
					selectCommand.Parameters.AddWithValue("@Ugcoursename1", EditEmployee.Ugcoursename1);
					selectCommand.Parameters.AddWithValue("@Ugstartdate1", EditEmployee.Ugstartdate1);
					selectCommand.Parameters.AddWithValue("@Ugenddate1", EditEmployee.Ugenddate1);
					selectCommand.Parameters.AddWithValue("@HSCschoolname1", EditEmployee.HSCschoolname1);
					selectCommand.Parameters.AddWithValue("@HSCstartdate1", EditEmployee.HSCstartdate1);
					selectCommand.Parameters.AddWithValue("@HSCenddate1", EditEmployee.HSCenddate1);
					selectCommand.Parameters.AddWithValue("@HSCcoursename1", EditEmployee.HSCcoursename1);
					selectCommand.Parameters.AddWithValue("@SSLCschoolname1", EditEmployee.SSLCschoolname1);
					selectCommand.Parameters.AddWithValue("@SSLCstartdate1", EditEmployee.SSLCstartdate1);
					selectCommand.Parameters.AddWithValue("@SSLCenddate1", EditEmployee.SSLCenddate1);
					selectCommand.Parameters.AddWithValue("@Emergencycontact1", EditEmployee.Emergencycontact1);
					selectCommand.Parameters.AddWithValue("@Relationship1", EditEmployee.Relationship1);




					await selectCommand.ExecuteNonQueryAsync();
					// Return the populated Employee object
					return Ok(true);
				}

			}
		}

		[HttpPut("Edit2/{EmpID:int}")]
		public async Task<ActionResult<EmployeeList>> Put2(EmployeeList EditEmployee)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();

				var sql = @"UPDATE EmployeeAccountDetails SET BankName=@Bankname1,AccountNumber=@Accountnumber1,IFSCCode=@Ifsccode1 WHERE EmpId=@EmpId";

				using (var selectCommand = new SqlCommand(sql, appDbConnection))
				{

					selectCommand.Parameters.AddWithValue("@EmpId", EditEmployee.EmpId);
					
					selectCommand.Parameters.AddWithValue("@Bankname1", EditEmployee.Bankname1);
					selectCommand.Parameters.AddWithValue("@Accountnumber1", EditEmployee.Accountnumber1);
					selectCommand.Parameters.AddWithValue("@Ifsccode1", EditEmployee.Ifsccode1);
	



					await selectCommand.ExecuteNonQueryAsync();
					// Return the populated Employee object
					return Ok(true);
				}

			}
		}
	}
}