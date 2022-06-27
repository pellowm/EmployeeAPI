using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config, ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public IEnumerable<EmployeeInformation> Get()
        {

            var results = new List<EmployeeInformation>();

            //get employee data and contact info from database
            using (SqlConnection connection = new SqlConnection(
                 _config.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(@"SELECT 
	                                                  Info.[employee_id]
	                                                  ,[first_name]
	                                                  ,[last_name]
	                                                  ,[hire_date]
                                                      ,[phone_number]
                                                      ,[email]
                                                  FROM [EmployeeContact] as Contact
                                                  JOIN [EmployeeInformation] as Info
                                                  ON Contact.employee_id = Info.employee_id;", connection);

                SqlDataReader reader = command.ExecuteReader();
                // store results in a list
                while (reader.Read())
                {
                    EmployeeInformation employeeInformation = new EmployeeInformation()
                    {
                        employee_id = (int)reader["employee_id"],
                        first_name = reader["first_name"].ToString(),
                        last_name = reader["last_name"].ToString(),
                        hire_date = (reader["hire_date"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["hire_date"]),
                        phone_number = reader["phone_number"].ToString(),
                        email = reader["email"].ToString()
                    };
                    results.Add(employeeInformation);
                }
            }

            return results;

        }


        [HttpGet("{id}")]
        public EmployeeInformation GetById(int id)
        {
            EmployeeInformation employeeInformation = new EmployeeInformation();

            //get employee data and contact info from database
            using (SqlConnection connection = new SqlConnection(
                 _config.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(@"SELECT 
	                                                  Info.[employee_id]
	                                                  ,[first_name]
	                                                  ,[last_name]
	                                                  ,[hire_date]
                                                      ,[phone_number]
                                                      ,[email]
                                                  FROM [EmployeeContact] as Contact
                                                  JOIN [EmployeeInformation] as Info
                                                  ON Contact.employee_id = Info.employee_id
                                                    WHERE Info.employee_id = @id;", connection); //TODO how to variable/sanitize input?

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                SqlDataReader reader = command.ExecuteReader();
                // store results in a list
                if (reader.Read())
                {

                    employeeInformation.employee_id = (int)reader["employee_id"];
                    employeeInformation.first_name = reader["first_name"].ToString();
                    employeeInformation.last_name = reader["last_name"].ToString();
                    employeeInformation.hire_date = (reader["hire_date"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["hire_date"]);
                    employeeInformation.phone_number = reader["phone_number"].ToString();
                    employeeInformation.email = reader["email"].ToString();
                    
                }
            }

            return employeeInformation;

        }
    }
}