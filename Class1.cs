using System.Data.SqlClient;

namespace Test_2
{
    class Class1
    {
        static void Main()
        {
            // Параметры соединения с базой данных
            string connectionString = "Data Source=your_server_name;Initial Catalog=your_database_name;Integrated Security=True";

            // SQL-запрос для выборки всех пар "Имя продукта – Имя категории"
            string query = @"
            SELECT p.ProductName, c.CategoryName
            FROM Products p
            LEFT JOIN ProductCategories pc ON p.ProductID = pc.ProductID
            LEFT JOIN Categories c ON pc.CategoryID = c.CategoryID";

            // Создание и открытие подключения к базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Создание SQL-команды
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Выполнение команды и чтение результатов
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Проверка на наличие строк
                        if (reader.HasRows)
                        {
                            // Чтение и вывод каждой строки
                            while (reader.Read())
                            {
                                string productName = reader.IsDBNull(0) ? "No Product Name" : reader.GetString(0);
                                string categoryName = reader.IsDBNull(1) ? "No Category" : reader.GetString(1);

                                Console.WriteLine($"Product: {productName}, Category: {categoryName}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No data found.");
                        }
                    }
                }
            }
        }

    }
}
