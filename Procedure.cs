using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics;

namespace WebNotes
{
    public class Procedure
    {
        public static string connectionString = "Server=THEFORPS;Database=Notes;TrustServerCertificate=True;Trusted_Connection=True;";

        public async Task procedure()
        {
            string sqlExpressionFirst = "";
            string sqlExpressionSecond = "";
            string sqlExpressionThird = "";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                sqlExpressionFirst = @"
                    CREATE PROCEDURE [dbo].[SelectAllNotes]
                    AS 
                    BEGIN
                        SELECT Users.Id, Title, CreatedDate 
                        FROM Users JOIN Notes ON Users.Id = Notes.UserId 
                    END";

                sqlExpressionSecond = @"
                    CREATE PROCEDURE [dbo].[SelectNotesThanksYear]
	                    @year int
                    AS
                    BEGIN
                        SELECT Id, Title, CreatedDate FROM Notes
	                    WHERE @year <= YEAR(CreatedDate)
                    END";

                sqlExpressionThird = @"
                    CREATE PROCEDURE [dbo].[SelectCountNotesThanksLogin]
	                    @login varchar(30)
                    AS
                    BEGIN
                        SELECT Login, COUNT(*) AS CountOfNotes FROM Users
	                    JOIN Notes ON Users.Id = Notes.UserId
	                    WHERE Login LIKE '%' + @login + '%'
	                    GROUP BY Login
                    END";
                
                SqlCommand command = new SqlCommand(sqlExpressionFirst, connection);
                await command.ExecuteNonQueryAsync();
                
                command.CommandText = sqlExpressionSecond;
                await command.ExecuteNonQueryAsync();
                
                command.CommandText = sqlExpressionThird;
                await command.ExecuteNonQueryAsync();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                 
                SqlCommand command = new SqlCommand("SelectAllNotes", connection);
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        Trace.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}");
 
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            DateTime date = reader.GetDateTime(2);
                            Trace.WriteLine($"{id} \t{title} \t{date}");
                        }
                        Trace.WriteLine("");
                    }
                }
                
                command = new SqlCommand("SelectNotesThanksYear", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@year",
                    Value = 2023
                };
                command.Parameters.Add(nameParam);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        Trace.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}");
 
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            DateTime date = reader.GetDateTime(2);
                            Trace.WriteLine($"{id} \t{title} \t{date}");
                        }
                        Trace.WriteLine("");
                    }
                }
                
                command = new SqlCommand("SelectCountNotesThanksLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                nameParam = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = "admin"
                };
                command.Parameters.Add(nameParam);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        Trace.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}");
 
                        while (reader.Read())
                        {
                            string login = reader.GetString(0);
                            int count = reader.GetInt32(1);
                            Trace.WriteLine($"{login} \t{count}");
                        }
                        Trace.WriteLine("");
                    }
                }
            }
        }
    }
}
