using MySql.Data.MySqlClient;

namespace MySQLTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string database = "MyMarket";
            // // Строка подключения к базе данных
            string connectionString = $"Server=localhost;Database={database};User=root;Password=;Port=3306;";

            // Создание подключения с использованием строки подключения
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Открытие соединения
                    UserRepository userRepository = new UserRepository(connection); // Создаем репозиторий пользователей
                    UserService userService = new UserService(userRepository); // Создаем сервис для работы с пользователями
                    Run(userService); // Запускаем основной цикл программы
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}"); // Обработка ошибок
                }
            }
        }
        static void Run(UserService userService)
        {
            bool exit = false;
            while (!exit)
            {
                PrintMenu(); // Вывод меню в консоль
                string? command = Console.ReadLine(); // Считываем команду пользователя
                Console.Clear(); // Очищаем консоль перед выбором команды, чтобы не засорять интерфейс
                switch (command)
                {
                    case "1":
                        userService.AddUser();
                        break;
                    case "2":
                        userService.GetAll();
                        break;
                    case "3":
                        userService.GetOne();
                        break;
                    case "4":
                        userService.Update();
                        break;
                    case "5":
                        userService.Delete();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Ошибка ввода");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("1) Добавить пользователя");
            Console.WriteLine("2) Вывести список всех пользователей");
            Console.WriteLine("3) Вывести конкретного пользователя");
            Console.WriteLine("4) Обновить пользователя");
            Console.WriteLine("5) Удалить пользователя");
            Console.WriteLine("0) Выход");
        }
    }
}
