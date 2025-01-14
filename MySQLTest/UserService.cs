using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLTest
{
    public class UserService
    {
        // Репозиторий
        private readonly UserRepository _userRepository;

        // Конструктор принимает объект UserRepository для взаимодействия с БД
        public UserService(UserRepository repository)
        {
            _userRepository = repository;
        }

        public void AddUser()
        {
            Console.WriteLine("Добавление пользователя");
            Console.Write("Имя пользователя: ");
            var userName = Console.ReadLine()!;
            Console.Write("Возраст пользователя: ");
            var userAge = int.Parse(Console.ReadLine()!); // Парсим число

            // Вызываем метод Add() из нашего репозитория
            var result = _userRepository.Add(new User(userName, userAge));
            if(result)
                Console.WriteLine("Пользователь добавлен успешно!");
            else
                Console.WriteLine("Пользователь НЕ добавлен. Что-то пошло не так!");
        }

        public void GetAll()
        {
            Console.WriteLine("Список всех пользователей");
            Console.WriteLine("ID\tВозраст\tИмя");
            var users = _userRepository.GetAll();
            foreach (var user in users)
                Console.WriteLine($"{user.Id}\t{user.Age}\t{user.Name}");
        }

        public void Delete()
        {
            Console.WriteLine("Удаление пользователя");
            Console.Write("Введите id пользователя: ");
            int id = int.Parse(Console.ReadLine()!);

            // Вызываем метод Delete() из нашего репозитория
            var result = _userRepository.Delete(id);
            if (result)
                Console.WriteLine("Пользователь успешно удален!");
            else
                Console.WriteLine("Пользователь НЕ удален. Что-то пошло не так!");
        }

        public void GetOne()
        {
            Console.WriteLine("Вывод конкретного пользователя");
            Console.WriteLine("Введите id пользователя: ");
            int id = int.Parse(Console.ReadLine()!);
            var user = _userRepository.Get(id);
            if(user == null)
            {
                Console.WriteLine("Пользователь с таким id не найден.");
                return;
            }
            Console.WriteLine("ID\tВозраст\tИмя");
            Console.WriteLine($"{user.Id}\t{user.Age}\t{user.Name}");
        }

        public void Update()
        {
            Console.WriteLine("Обновление пользователя");
            Console.Write("Введите id пользователя: ");
            var userId = int.Parse(Console.ReadLine()!);
            Console.Write("Имя пользователя: ");
            var userName = Console.ReadLine()!;
            Console.Write("Возраст пользователя: ");
            var userAge = int.Parse(Console.ReadLine()!);
            var result = _userRepository.Update(new User(userId, userName, userAge));
            if (result)
                Console.WriteLine("Пользователь успешно обновлен!");
            else
                Console.WriteLine("Пользователь НЕ обновлен. Что-то пошло не так!");
        }
    }
}
