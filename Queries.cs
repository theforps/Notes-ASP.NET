using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics;

namespace WebNotes
{
    public class Queries
    {
        public readonly string connectionString = "Server=OPERATOR;Database=Notes;Trusted_Connection=True;MultipleActiveResultSets=True";

        public async Task querie()
        {
            string sqlExpression = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

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

                //5 запрос на формирование списка вывода (проекция)
                //sqlExpression =
                //    "SELECT * FROM Users;" +
                //    "SELECT * FROM Notes;";

                //6 запрос с упорядочением результатов по возрастанию и убыванию
                //sqlExpression =
                //    "SELECT * FROM Users ORDER BY Login Desc;" +
                //    "SELECT * FROM Notes ORDER BY Title Asc;";

                //7 запросы ко всем таблицам с условием (селекция)
                //sqlExpression =
                //    "SELECT * FROM Notes WHERE Id = 1 or Description IS NOT NULL;" +
                //    "SELECT * FROM Users WHERE Login = ConfirmPassword;";

                //8 запросы ко всем таблицам с использованием предиката IN
                //sqlExpression =
                //    "SELECT * FROM Notes WHERE UserId IN (1, 2, 3);" +
                //    "SELECT * FROM Users WHERE Password IN (12345678, 123456);";

                //9 запросы ко всем таблицам с использованием предиката BETWEEN...AND
                //sqlExpression =
                //    "SELECT * FROM Users WHERE Id BETWEEN 1 AND 5;" +
                //    "SELECT * FROM Notes WHERE Id BETWEEN 1 AND 5;";

                //10 запросы ко всем таблицам с использованием предиката LIKE
                //sqlExpression =
                //    "SELECT * FROM Notes WHERE Title Like '%test%';" +
                //    "SELECT * FROM Users WHERE Login Like '%1234%';";

                //11 запросы ко всем таблицам с использованием предиката IS [NOT] NULL
                //sqlExpression =
                //    "SELECT * FROM Notes WHERE Description IS NULL;" +
                //    "SELECT * FROM Users WHERE Login IS NOT NULL;";

                //12 запросы ко всем таблицам с использованием агрегирующей функции COUNT(*)
                //sqlExpression =
                //    "SELECT COUNT(*) FROM Notes;" +
                //    "SELECT COUNT(*) FROM Users;";

                //13 запросы ко всем таблицам с использованием агрегирующей функции COUNT(имя_поля)
                //sqlExpression =
                //    "SELECT COUNT(Title) FROM Notes;" +
                //    "SELECT COUNT(Login) FROM Users;";

                //14 запросы ко всем таблицам с использованием агрегирующей функции COUNT(distinct имя_поля)
                //sqlExpression =
                //    "SELECT COUNT(distinct Title) FROM Notes;" +
                //    "SELECT COUNT(distinct Login) FROM Users;";

                //15 запросы ко всем таблицам с использованием агрегирующей функции MAX
                //sqlExpression =
                //    "SELECT MAX(CountOfChanges) FROM Notes;";

                //16 запросы ко всем таблицам с использованием агрегирующей функции MIN
                //sqlExpression =
                //    "SELECT MIN(CountOfChanges) FROM Notes;";

                //17 запросы ко всем таблицам с использованием агрегирующей функции SUM.
                //sqlExpression =
                //    "SELECT SUM(CountOfChanges) FROM Notes;";

                //18.Запросы ко всем таблицам с использованием агрегирующей функции AVG
                //sqlExpression =
                //    "SELECT AVG(CountOfChanges) FROM Notes;";

                //19 запросы ко всем таблицам с использованием GROUP BY (по одному и по нескольким полям)
                //sqlExpression =
                //    "SELECT Title FROM Notes WHERE CountOfChanges > 0 GROUP BY Title, Id;" +
                //    "SELECT Login FROM Users WHERE Login = '12345678' GROUP BY Login;";

                //20 запросы ко всем таблицам с использованием HAVING
                //sqlExpression =
                //    "SELECT COUNT(Id), Login FROM Users GROUP BY Login HAVING COUNT(Id) > 1;" +
                //    "SELECT COUNT(Id), Title FROM Notes GROUP BY Title HAVING COUNT(Id) > 0;";

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
