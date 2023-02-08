using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace WebNotes
{
    public class Queries
    {
        public readonly string connectionString = "Server=OPERATOR;Database=Notes;Trusted_Connection=True;MultipleActiveResultSets=True";

        public async Task querie()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string sqlExpression = "";

                //1 создание таблиц
                //sqlExpression =
                //    "CREATE TABLE Users (Id int NOT NULL PRIMARY KEY, Login NVARCHAR(25) NOT NULL, Password NVARCHAR(50) NOT NULL, DateOfCreate DATETIME2);" +
                //    "CREATE TABLE Notes (Id int NOT NULL PRIMARY KEY, Title NVARCHAR(250) NOT NULL, Description NVARCHAR(500), CreatedDate DATETIME2 NOT NULL, CountOfChanges int, UserId int FOREIGN KEY REFERENCES Users(Id));";

                //2 добавление записей
                //sqlExpression =
                //    "INSERT INTO Users (Login, Password, DateOfCreate) Values ('testtest', '12345678', '2023-01-30');" +
                //    "INSERT INTO Notes (Title, CreatedDate, UserId, CountOfChanges) Values ('testtest', '2023-01-30', 1, 2);";

                //3 изменение таблиц
                //sqlExpression =
                //    "UPDATE Users SET Password = '84545454545' WHERE Id = 1;" +
                //    "UPDATE Notes SET Title = 'Update Title' WHERE Id = 1;";

                //4 удаление данных
                //sqlExpression =
                //    "DELETE FROM Notes WHERE Id = 5";



                SqlCommand command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
