using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLTest
{
    public class UserRepository
    {
        // Переменная для хранения объекта MySqlConnection
        private readonly MySqlConnection _connection;

        // Конструктор принимает объект MySqlConnection для работы с базой данных
        public UserRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        // Добавляет пользователя в базу данных с помощью SQL-запроса INSERT
        public bool Add(User user)
        {
            string insertQuery = "INSERT INTO Users (Name, Age) VALUES (@Name, @Age)";
            using (MySqlCommand cmd = new MySqlCommand(insertQuery, _connection))
            {
                // Привязываем параметры запроса для предотвращения SQL-инъекций
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Age", user.Age);

                // Выполняем запрос и проверяем, было ли добавлено хотя бы одно значение
                int rowsInserted = cmd.ExecuteNonQuery();
                if (rowsInserted > 0)
                    return true; // Успешное добавление
                return false; // Не удалось добавить пользователя
            }
        }

        // Удаляет пользователя по ID (DELETE)
        public bool Delete(int id)
        {
            string deleteQuery = "DELETE FROM Users WHERE Id = @Id";
            using (MySqlCommand cmd = new MySqlCommand(deleteQuery, _connection))
            {
                // Привязываем ID пользователя, который нужно удалить
                cmd.Parameters.AddWithValue("@Id", id);

                // Выполняем запрос и проверяем, были ли удалены строки
                int rowsDeleted = cmd.ExecuteNonQuery();
                if (rowsDeleted > 0)
                    return true; // Успешное удаление
                return false; // Пользователь с таким ID не найден
            }
        }

        // Обновляет данные пользователя (имя и возраст) по его ID 
        public bool Update(User user)
        {
            string updateQuery = "UPDATE Users SET Name = @Name, Age = @Age WHERE Id = @Id";
            using (MySqlCommand cmd = new MySqlCommand(updateQuery, _connection))
            {
                // Привязываем параметры для обновления данных
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Age", user.Age);
                cmd.Parameters.AddWithValue("@Id", user.Id);

                // Выполняем запрос и проверяем, были ли обновлены строки
                int rowsUpdated = cmd.ExecuteNonQuery();
                if (rowsUpdated > 0)
                    return true; // Обновление прошло успешно
                return false; // Пользователь с таким ID не найден
            }
        }

        // Получает одного пользователя из базы данных по его ID
        public User? Get(int id)
        {
            string selectQuery = "SELECT Id, Name, Age FROM Users WHERE Id = @Id";
            using (MySqlCommand cmd = new MySqlCommand(selectQuery, _connection))
            {
                // Привязываем ID для поиска конкретного пользователя
                cmd.Parameters.AddWithValue("@Id", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Создаем объект пользователя и заполняем его данными из базы
                        User user = new User
                        {
                            // Если пользователь найден, создаем объект и заполняем его данными
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            Age = reader.GetInt32("Age")
                        };
                        return user;
                    }
                }
            }
            return null; // Если пользователь с таким ID не найден
        }

        // Возвращает список всех пользователей
        public List<User> GetAll()
        {
            List<User> users = new List<User>(); // Создаем список пользователей
            string selectQuery = "SELECT id, Name, Age FROM Users";
            using (MySqlCommand cmd = new MySqlCommand(selectQuery, _connection))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                // Проходим по всем записям в результатах запроса
                while (reader.Read())
                {
                    // Создаем объект пользователя и заполняем его данными
                    User user = new()
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name"),
                        Age = reader.GetInt32("age")
                    };
                    users.Add(user); // Добавляем пользователя в список
                }
            }
            return users; // Возвращаем полный список пользователей
        }
    }
}
