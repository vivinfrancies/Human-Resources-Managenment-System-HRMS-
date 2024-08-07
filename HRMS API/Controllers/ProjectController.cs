using HRMS_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel;
using WebApplication1.Model;

namespace HRMS_API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProjectController(ModelContext dbcontext, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetTodoItems()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var projects = new List<Project>();
                    var sql = @"SELECT * FROM ProjectTable where IsActive = 1";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var project = new Project
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    ProjectId = reader["ProjectID"].ToString(),
                                    ProjectName = reader["ProjectName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                                    OpenTask = Convert.ToInt32(reader["OpenTask"]),
                                    CompletedTask = Convert.ToInt32(reader["CompletedTasks"]),
                                    NoOfTask = Convert.ToInt32(reader["NoOfTask"]),
                                    Progress = Convert.ToInt32(reader["Progress"]),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                    Status = Convert.ToBoolean(reader["Status"]),
                                    ClientID = Convert.ToInt32(reader["ClientID"]),
                                    ClientName = reader["ClientCompany"].ToString(),
                                    IsActive = Convert.ToBoolean(reader["IsActive"])
                                };
                                projects.Add(project);
                            }
                        }
                    }
                    return Ok(projects);
                }
                catch (Exception ex)
                {
                    return Ok(ex);
                }
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Project>> Get(int id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var sql = @"SELECT* FROM ProjectTable WHERE IsActive = 1 and ID = @id";
                    using (var SelectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        SelectCommand.Parameters.AddWithValue("@id", id);
                        using (var reader = await SelectCommand.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var Project = new Project
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    ProjectId = reader["ProjectID"].ToString(),
                                    ProjectName = reader["ProjectName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                                    OpenTask = Convert.ToInt32(reader["OpenTask"]),
                                    CompletedTask = Convert.ToInt32(reader["CompletedTasks"]),
                                    NoOfTask = Convert.ToInt32(reader["NoOfTask"]),
                                    Progress = Convert.ToInt32(reader["Progress"]),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                    Status = Convert.ToBoolean(reader["Status"]),
                                    ClientID = Convert.ToInt32(reader["ClientID"]),
                                    ClientName = reader["ClientCompany"].ToString(),
                                    IsActive = Convert.ToBoolean(reader["IsActive"])
                                };
                                return Project;
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        //TeamMembers
        [HttpGet("ProjectMembers/{id:int}/")]
        public async Task<ActionResult<Project>> TeamMembers(int id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var projects = new List<Project>();
                    var sql = @"SELECT * from TeamMember Where ProjectID = @id AND IsActive = 1;";
                    using (var SelectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        SelectCommand.Parameters.AddWithValue("@id", id);
                        using (var reader = await SelectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var project = new Project
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    TeamMembers = reader["FullName"].ToString(),
                                    Roles = reader["Role"].ToString(),
                                    Module = reader["Module"].ToString(),
                                };
                                projects.Add(project);
                            }
                            return Ok(projects);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
        }

        [HttpGet("ProjectLogin/{id:int}/")]
        public async Task<ActionResult<Project>> ProjectLogin(int id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var projects = new List<Project>();
                    var sql = @"SELECT * from TeamMember Where TeamMembersID = @id AND IsActive = 1;";
                    using (var SelectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        SelectCommand.Parameters.AddWithValue("@id", id);
                        using (var reader = await SelectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var project = new Project
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    ProjectId = reader["ProjectID"].ToString(),
                                    ProjectName = reader["ProjectName"].ToString(),
                                    TeamMembers = reader["FullName"].ToString(),
                                    Role = Convert.ToInt32(reader["ROLEID"]),
                                    Roles = reader["Role"].ToString(),
                                    Module = reader["Module"].ToString(),
                                    IsActive = Convert.ToBoolean(reader["IsActive"])
                                };
                                projects.Add(project);
                            }
                            return Ok(projects);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
        }

        [HttpPost("Project")]
        public async Task<ActionResult> PostTodoItem(Project todoDTO)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var sql = @"EXEC SaveProject 
                        @SpClientCompany = @ClientName, 
                        @SpProjectName = @ProjectName, 
                        @SpDescription = @Description, 
                        @SpDueDate = @DueDate, 
                        @SpTaskName = @TaskName, 
                        @SpTaskDueDate = @TaskDueDate, 
                        @spEmpId = @EmpId";

                    using (var insertCommand = new SqlCommand(sql, appDbConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@ClientName", todoDTO.ClientName);
                        insertCommand.Parameters.AddWithValue("@ProjectName", todoDTO.ProjectName);
                        insertCommand.Parameters.AddWithValue("@Description", todoDTO.Description);
                        insertCommand.Parameters.AddWithValue("@DueDate", todoDTO.DueDate);
                        insertCommand.Parameters.AddWithValue("@TaskName", todoDTO.TaskName);
                        insertCommand.Parameters.AddWithValue("@TaskDueDate", todoDTO.TaskDueDate);
                        insertCommand.Parameters.AddWithValue("@EmpId", todoDTO.EmpId);

                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    return Ok("true");
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
        }

        [HttpPost("ProjectMembers")]
        public async Task<ActionResult> ProjectMembers(Project todoDTO)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var sql = @"EXEC SpTeamMembers  @SpProjectName=@ProjectName ,@spTeamMembersID=@EmpId,@SpModule=@Module,@spRole=@Role";

                    using (var insertCommand = new SqlCommand(sql, appDbConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@ProjectName", todoDTO.ProjectName);
                        insertCommand.Parameters.AddWithValue("@EmpId", todoDTO.EmpId);
                        insertCommand.Parameters.AddWithValue("@Module", todoDTO.Module);
                        insertCommand.Parameters.AddWithValue("@Role", todoDTO.Role);

                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    return Ok("true");
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
        }

        [HttpPut("Client/{id:int}")]
        public async Task<ActionResult<Project>> Client(int id, string clientname)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                var sql = @"update Client set ClientCompany = @clientname where ID = @id;";
                using (var selectCommand = new SqlCommand(sql, appDbConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    selectCommand.Parameters.AddWithValue("@clientname", clientname);
                    await selectCommand.ExecuteNonQueryAsync();
                    return Ok(true);
                }
            }
        }

        [HttpPut("Project/{id:long}")]
        public async Task<ActionResult<Project>> Project(Project todoitem)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                var sql = @"UPDATE Project SET ProjectName =@ProjectName ,Description =@desp , DueDate = @duedate Where ID = @id";
                using (var selectCommand = new SqlCommand(sql, appDbConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", todoitem.Id);
                    selectCommand.Parameters.AddWithValue("@ProjectName", todoitem.ProjectName);
                    selectCommand.Parameters.AddWithValue("@duedate", todoitem.DueDate);
                    selectCommand.Parameters.AddWithValue("@desp", todoitem.Description);

                    await selectCommand.ExecuteNonQueryAsync();
                    return Ok(true);
                }
            }
        }

        [HttpPut("Delete/{id:int}")]
        public async Task<ActionResult<Project>> Delete(int id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                var sql = @"Update Project SET IsActive = 0 Where ID = @id;";
                using (var selectCommand = new SqlCommand(sql, appDbConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    await selectCommand.ExecuteNonQueryAsync();
                    return Ok(true);
                }
            }
        }

        [HttpPut("TeamMemberDelete/{id:int}")]
        public async Task<ActionResult<Project>> TeamMemberDelete(int id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                var sql = @"Update ProjectMembers SET IsActive = 0 Where ID = @id;";
                using (var selectCommand = new SqlCommand(sql, appDbConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    await selectCommand.ExecuteNonQueryAsync();
                    return Ok(true);
                }
            }
        }

        [HttpPut("TeamMemberModuleUpdate/{id:int}")]
        public async Task<ActionResult<Project>> TeamMemberModuleUpdate(int id, string module)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                var sql = @"UPDATE TeamMember Set Module = @Module where id = @id;";
                using (var selectCommand = new SqlCommand(sql, appDbConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    selectCommand.Parameters.AddWithValue("@Module", module);
                    await selectCommand.ExecuteNonQueryAsync();
                    return Ok(true);
                }
            }
        }

        [HttpPut("UpdateProjectStatus/{id:int}")]
        public async Task<ActionResult<Project>> UpdateProjectStatus(int id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                var sql = @"update project set Status = 0 Where id = 1;";
                using (var selectCommand = new SqlCommand(sql, appDbConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    await selectCommand.ExecuteNonQueryAsync();
                    return Ok(true);
                }
            }
        }
    }
}