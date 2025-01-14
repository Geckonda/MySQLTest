using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySQLTest
{
    public class User
    {
        public int Id { get; set; } // Уникальный идентификатор пользователя
        public string? Name { get; set; } // Имя пользователя
        public int Age { get; set; } // Возраст пользователя

        // Конструкторы для удобства создания объекта
        public User()
        {
            
        }
        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public User(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
