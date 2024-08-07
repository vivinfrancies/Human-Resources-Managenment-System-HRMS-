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
using System.Net.Sockets;

namespace HRMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IConfiguration _configuration;


        public TicketsController(ModelContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTodoItems()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var Tickets = new List<Ticket>();
                    var sql = "SELECT * FROM Ticket";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var ticket = new Ticket
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    EmpId = Convert.ToInt32(reader["EmpId"]),
                                    TicketId = reader["TicketId"].ToString(),
                                    TicketSubject = reader["TicketSubject"].ToString(),
                                    Priority = reader["Priority"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Status = reader["Status"].ToString(),
                                    RaiseDate = Convert.ToDateTime(reader["RaiseDate"]),
                                    AttachFile = reader["AttachFile"].ToString(),
                                    //CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                    //ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"]),
                                    //CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"])
                                };
                                Tickets.Add(ticket);

                            }
                        }
                    }
                    return Ok(Tickets);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }


        [HttpGet("TickerRaised")]
        public async Task<ActionResult<IEnumerable<Ticket>>> TickerRaised()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var Tickets = new List<Ticket>();
                    var sql = "SELECT * FROM TicketRaised";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var ticket = new Ticket
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    TicketId = reader["TicketId"].ToString(),
                                    EmpId = Convert.ToInt32(reader["EmpId"]),
                                    TicketSubject = reader["Subject"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    MiddleName = reader["MiddleName"].ToString(),
                                    Designation = reader["Designation"].ToString(),
                                    RaiseDate = Convert.ToDateTime(reader["RaiseDate"]),
                                    Priority = reader["Priority"].ToString(),
                                    Status = reader["Status"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    AttachFile = reader["AttachFile"].ToString(),
                                    CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"])
                                };
                                Tickets.Add(ticket);

                            }
                        }
                    }
                    return Ok(Tickets);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("TicketSubject")]
        public async Task<ActionResult<IEnumerable<TicketSubjectList>>> TicketSubject()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var ticketSubjectLists = new List<TicketSubjectList>();
                    var sql = "SELECT * FROM TicketSubject";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var ticketsubList = new TicketSubjectList
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Subject = reader["Subject"].ToString(),
                                    PriorityId = Convert.ToInt32(reader["PriorityId"]),
                                };
                                ticketSubjectLists.Add(ticketsubList);

                            }
                        }
                    }
                    return Ok(ticketSubjectLists);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpGet("Priority")]
        public async Task<ActionResult<IEnumerable<TicketPriority>>> Priority()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var ticketPriority = new List<TicketPriority>();
                    var sql = "SELECT * FROM Priority";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var ticketsubList = new TicketPriority
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Priority = reader["Priority"].ToString(),
                                };
                                ticketPriority.Add(ticketsubList);

                            }
                        }
                    }
                    return Ok(ticketPriority);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
		[HttpGet("OverALlTicketRaised/{EmpId:int}")]
		public async Task<ActionResult<IEnumerable<Ticket>>> OverALlTicketRaised(int EmpId)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var ticketSubjectLists = new List<Ticket>();

					var sql = @"SELECT * FROM TicketRaised WHERE NOT EmpId=@EmpId AND IsActive = 1";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{
						selectCommand.Parameters.AddWithValue("@EmpId", EmpId);
						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								var ticket = new Ticket
								{
									ID = Convert.ToInt32(reader["ID"]),
									EmpId = Convert.ToInt32(reader["EmpId"]),
									TicketId = reader["TicketId"].ToString(),
									TicketSubject = reader["Subject"].ToString(),
									FirstName = reader["FirstName"].ToString(),
									MiddleName = reader["MiddleName"].ToString(),
									Designation = reader["Designation"].ToString(),
									RaiseDate = Convert.ToDateTime(reader["RaiseDate"]),
									Priority = reader["Priority"].ToString(),
									Status = reader["Status"].ToString(),
									Description = reader["Description"].ToString(),
									AttachFile = reader["AttachFile"].ToString(),
									CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
								};
								ticketSubjectLists.Add(ticket);
							}
						}
					}

					if (ticketSubjectLists.Count > 0)
					{
						return Ok(ticketSubjectLists);
					}
					else
					{
						return NotFound();
					}
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
		[HttpGet("TicketHistory/{EmpId:int}")]
		public async Task<ActionResult<IEnumerable<Ticket>>> TicketHistory(int EmpId)
		{
			var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

			using (var appDbConnection = new SqlConnection(appDbConnectionString))
			{
				await appDbConnection.OpenAsync();
				try
				{
					var ticketSubjectLists = new List<Ticket>();

					var sql = @"SELECT * FROM TicketRaised WHERE NOT EmpId=@EmpId AND IsActive = 0";
					using (var selectCommand = new SqlCommand(sql, appDbConnection))
					{
						selectCommand.Parameters.AddWithValue("@EmpId", EmpId);
						using (var reader = await selectCommand.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								var ticket = new Ticket
								{
									ID = Convert.ToInt32(reader["ID"]),
									EmpId = Convert.ToInt32(reader["EmpId"]),
									TicketId = reader["TicketId"].ToString(),
									TicketSubject = reader["Subject"].ToString(),
									FirstName = reader["FirstName"].ToString(),
									MiddleName = reader["MiddleName"].ToString(),
									Designation = reader["Designation"].ToString(),
									RaiseDate = Convert.ToDateTime(reader["RaiseDate"]),
									Priority = reader["Priority"].ToString(),
									Status = reader["Status"].ToString(),
									Description = reader["Description"].ToString(),
									AttachFile = reader["AttachFile"].ToString(),
									CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
								};
								ticketSubjectLists.Add(ticket);
							}
						}
					}

					if (ticketSubjectLists.Count > 0)
					{
						return Ok(ticketSubjectLists);
					}
					else
					{
						return NotFound();
					}
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}
		[HttpGet("PersonalTicketRaised/{EmpId:int}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> PersonalTicketRaised(int EmpId)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                try
                {
                    var ticketSubjectLists = new List<Ticket>();

                    var sql = @"SELECT * FROM TicketRaised WHERE EmpId=@EmpId";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        selectCommand.Parameters.AddWithValue("@EmpId", EmpId);
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var ticket = new Ticket
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    EmpId = Convert.ToInt32(reader["EmpId"]),
                                    TicketId = reader["TicketId"].ToString(),
                                    TicketSubject = reader["Subject"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    MiddleName = reader["MiddleName"].ToString(),
                                    Designation = reader["Designation"].ToString(),
                                    RaiseDate = Convert.ToDateTime(reader["RaiseDate"]),
                                    Priority = reader["Priority"].ToString(),
                                    Status = reader["Status"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    AttachFile = reader["AttachFile"].ToString(),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                                };
                                ticketSubjectLists.Add(ticket);
                            }
                        }
                    }

                    if (ticketSubjectLists.Count > 0)
                    {
                        return Ok(ticketSubjectLists);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("TotalTicketCount")]
        public async Task<ActionResult<TicketCounts>> TotalTicketCount()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var ticketCount = new TicketCounts();
                    var sql = @"EXEC TotalTicketCount";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            // Read TotalProject
                            if (await reader.ReadAsync())
                            {
                                ticketCount.NewTicket = reader.GetInt32(reader.GetOrdinal("NewTicket"));
                            }

                            // Move to the next result set (TotalClient)
                            await reader.NextResultAsync();
                            if (await reader.ReadAsync())
                            {
                                ticketCount.PendingTicket = reader.GetInt32(reader.GetOrdinal("PendingTicket"));
                            }

                            // Move to the next result set (TotalEmployee)
                            await reader.NextResultAsync();
                            if (await reader.ReadAsync())
                            {
                                ticketCount.SolvedTicket = reader.GetInt32(reader.GetOrdinal("SolvedTicket"));
                            }

                        }
                    }
                    return Ok(ticketCount);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
        }




        [HttpPut("Approve /{ID:int}")]
        public async Task<ActionResult<Ticket>> PutToItem(int ID)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var sql = @"UPDATE Ticket SET Status = 'Approved', IsActive = 0 WHERE ID = @ID";
                    using (var updatecmd = new SqlCommand(sql, appconnection))
                    {
                        updatecmd.Parameters.AddWithValue("@ID", ID);
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
        [HttpPut("Reject /{ID:int}")]
        public async Task<ActionResult<Ticket>> Reject(int ID)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var sql = @"UPDATE Ticket SET Status = 'Declined', IsActive = 0 WHERE ID = @ID";
                    using (var updatecmd = new SqlCommand(sql, appconnection))
                    {
                        updatecmd.Parameters.AddWithValue("@ID", ID);
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

        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTodoItem(Ticket ticket)
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

                    if (ticket.FileContent3 != null)
                    {
                        var uniqueFileName3 = DateTime.Now.ToString("yyyyMMddHHmm") + "_" + ticket.AttachFile;
                        var filePath3 = Path.Combine(pathBuilt, uniqueFileName3);
                        ticket.AttachFile = uniqueFileName3;
                        FileStream file3 = System.IO.File.Create(filePath3);
                        file3.Write(ticket.FileContent3, 0, ticket.FileContent3.Length);
                        file3.Close();
                    }
                    var sql = @"INSERT INTO Ticket (EmpId, TicketSubject, Priority, Description,AttachFile, RaiseDate,CreatedDate,CreatedBy) VALUES  (@EmpId, @TicketSubjectId, @PriorityId, @Description, @AttachFile,  CONVERT(DATE, GETDATE()),CONVERT(DATE, GETDATE()),@CreatedBy)";

                    using (var insertCommand = new SqlCommand(sql, appDbConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@EmpId", ticket.EmpId);
                        insertCommand.Parameters.AddWithValue("@TicketSubjectId", ticket.TicketSubjectId);
                        insertCommand.Parameters.AddWithValue("@PriorityId", ticket.PriorityId);
                        insertCommand.Parameters.AddWithValue("@Description", ticket.Description);
                        insertCommand.Parameters.AddWithValue("@AttachFile", ticket.AttachFile);
                        insertCommand.Parameters.AddWithValue("@CreatedBy", ticket.CreatedBy);


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

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.ID == id);
        }
    }
}