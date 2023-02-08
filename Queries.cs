using Microsoft.Data.SqlClient;

namespace WebNotes
{
    public class Queries
    {
        public readonly string connectionString = "Server=OPERATOR;Database=Notes;Trusted_Connection=True;MultipleActiveResultSets=True";

        public Queries()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.OpenAsync();
                SqlCommand command = new SqlCommand();

                //
                command.CommandText = "CREATE TABLE Users (Id INT PRIMARY KEY IDENTITY, Age INT NOT NULL, Name NVARCHAR(100) NOT NULL)";





                command.Connection = connection;
                command.ExecuteNonQueryAsync();
            }
        }
    }
}
