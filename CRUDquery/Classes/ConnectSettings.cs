using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace CRUDquery.Classes
{
    /// <summary>
    ///     Абстрактний клас для роботи з БД (можна додавати нові таблички до БД та створювати нові класи наслідуючись від базового)
    /// </summary>
    abstract class ConnectSettings : IDisposable
    {
        protected SqlConnection sql = null;
        /// <summary>
        ///     Метод для корректного вводу числа
        /// </summary>
        /// <returns>Повертає числове значення</returns>
        protected int EnterIntValue() 
        {
            bool isBegin = true;
            int num = 0;
            while (true) 
            {
              try
            {
                if (!isBegin)
                {
                    Console.Write("Ще раз: ");
                }
                num = int.Parse(Console.ReadLine());

                return num;
            }
            catch 
            {
                isBegin = false;
            }
            }

        }
        abstract public void Select();
        abstract public void Create();
        abstract public void Update();
        abstract public void Delete();
        /// <summary>
        ///     Конструктор що встановлює зєднання з БД
        /// </summary>
        /// <param name="connStr">приймає строку підключення</param>
        public ConnectSettings(string connStr)
        {
            this.sql = new SqlConnection(connStr);
            this.sql.Open();
        }
        /// <summary>
        ///     Закриває усі підключення (можна використовувати using)
        /// </summary>
        public void Dispose()
        {
            if (this.sql != null) 
            {
               this.sql.Close();
            }
        }
    }
    /// <summary>
    ///     Клас для роботи з CRUD елементів City
    /// </summary>
    class CityCRUD : ConnectSettings 
    {
        public CityCRUD(string conn) : base(conn)
        {

        }

        /// <summary>
        ///     Метод для створення елементу
        /// </summary>
        public override void Create()
        {
            Console.Clear();
            Console.Write("Ведіть назву міста: ");
            string name = Console.ReadLine();

            string query = $"INSERT INTO [dbo].[tblCity] (Name) VALUES (N'{name}')";

            SqlCommand command = new SqlCommand(query, this.sql);
            command.ExecuteNonQuery();

            UserCRUD.cities = null;
        }

        /// <summary>
        ///     метод для видалення елемента
        /// </summary>
        public override void Delete()
        {
            Console.WriteLine("Оберіть номер міста");
            Thread.Sleep(2000);
            this.Select();
            Console.Write("Ведіть номер: ");
            int Id = 0;
            try
            {
                Id = int.Parse(Console.ReadLine());
            }
            catch 
            {
                Console.Clear();
                Console.WriteLine("Помилка вводу данних (процес анульовано!)");
                Thread.Sleep(1000);

                return;
            }

            City c = UserCRUD.cities.Where((City c) => { return c.id == Id; }).FirstOrDefault();
            if (c != null)
            {
                string query = "DELETE FROM [dbo].[tblCity] WHERE Id = " + c.id;
                SqlCommand command = new SqlCommand(query, this.sql);
                command.ExecuteNonQuery();
                UserCRUD.cities = null;
            }
            else 
            {
                Console.Clear();
                Console.WriteLine("Міста не знайдено!");
                Thread.Sleep(1500);

            }
        }

        /// <summary>
        ///     Метод який виводить меню з елементів таблички City
        /// </summary>
        public override void Select() 
        {
            int skipped = 0;
            if (UserCRUD.cities == null) 
            {
                UserCRUD.cities = UserCRUD.GetCitiesFromDB(this.sql);
            }
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            while (keyInfo.Key != ConsoleKey.Enter) 
            {
                Console.Clear();
                foreach (var item in UserCRUD.cities.Skip(skipped).Take(5)) 
                {
                    Console.WriteLine(item);
                }
                Console.SetCursorPosition(5, 5);
                Console.WriteLine("<<<< ENTER >>>>");
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key) 
                {
                    case ConsoleKey.LeftArrow: 
                        {
                            if (skipped > 0) 
                            {
                                skipped -= 5;
                            }
                            break; 
                        }
                    case ConsoleKey.RightArrow: 
                        {
                            if (skipped + 5 < UserCRUD.cities.Count) 
                            {
                                skipped += 5;
                            }
                            break; 
                        }
                }
            }
        }
        /// <summary>
        ///     Метод який редагує елементи таблиці City
        /// </summary>
        public override void Update()
        {
            Console.WriteLine("Оберіть номер міста");
            Thread.Sleep(2000);
            this.Select();
            Console.Write("Ведіть номер міста: ");
            int Id = EnterIntValue();
           
            

            City c = UserCRUD.cities.Where((City c) => { return c.id == Id; }).FirstOrDefault();
            if (c != null)
            {
                bool isBegin = true;
                string query = "UPDATE [dbo].[tblCity] SET ";
                Console.Write("Ведіть назву міста: ");
                string name = Console.ReadLine();

                if (!string.IsNullOrEmpty(name)) 
                {
                    isBegin = false;
                    query += "Name = N'" + name + "'";
                }

                query += " WHERE Id = " + c.id;
                if (!isBegin) 
                {
                SqlCommand command = new SqlCommand(query, this.sql);
                command.ExecuteNonQuery();
                UserCRUD.cities = null;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Міста не знайдено!");
                Thread.Sleep(1500);

            }
        }
    }

    /// <summary>
    /// Клас для роботи з користувачами
    /// </summary>
    class UserCRUD : ConnectSettings
    {
        /// <summary>
        ///     Поле для зберігання користувачів
        /// </summary>
        List<User> users = null;
        /// <summary>
        ///     Поле для зберігання міст
        /// </summary>
        public static List<City> cities = null;
        /// <summary>
        ///     Метод що вертає усіх користувачів з таблиці
        /// </summary>
        /// <returns>Повертає ліст користувачів</returns>
        private List<User> GetUsersFromDB() 
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM [dbo].[tblUser]";
            SqlCommand command = new SqlCommand(query, this.sql);

            using (SqlDataReader dataReader = command.ExecuteReader()) 
            {
            while (dataReader.Read()) 
            {
                User u = new User();
                u.id = int.Parse(dataReader["Id"].ToString());
                u.name = dataReader["Name"].ToString();
                u.telNumer = dataReader["telNummer"].ToString();
                u.cityId = int.Parse(dataReader["CityId"].ToString());
                users.Add(u);
            }
            }
            return users;
        }
        /// <summary>
        /// Повертає усі міста (Оскільки метод статичний він приймає параметр приєднання)
        /// </summary>
        /// <param name="sql">Приймає параметр приєднання до БД</param>
        /// <returns>Повертає ліст міст</returns>
        public static List<City> GetCitiesFromDB(SqlConnection sql) 
        {
            List<City> cities = new List<City>();
            string query = "SELECT * FROM [dbo].[tblCity]";
            SqlCommand command = new SqlCommand(query, sql);

            using (SqlDataReader data = command.ExecuteReader()) 
            {

                while (data.Read())
                {
                    City city = new City();
                    city.id = int.Parse(data["Id"].ToString());
                    city.name = data["Name"].ToString();

                    cities.Add(city);
                }
            }
            
            return cities;
        }
        public UserCRUD(string conn) : base(conn)
        {

        }
        /// <summary>
        ///     Створює нових користувачів і додає у БД
        /// </summary>
        public override void Create()
        {
            Console.Clear();
            Console.Write("Ведіть ім'я: ");
            string name = Console.ReadLine();
            Console.Write("Ведіть номер телефону: ");
            string telNummer = Console.ReadLine();



            int skipped = 0;

            if (UserCRUD.cities == null)
            {
                UserCRUD.cities = UserCRUD.GetCitiesFromDB(this.sql);
            }

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("Перегляньте міста та виберіть Id і жміть Enter:");
                foreach (var item in UserCRUD.cities.Skip(skipped).Take(5))
                {
                    Console.WriteLine(item);
                }
                Console.SetCursorPosition(15, 6);
                Console.WriteLine("<<<< Enter >>>>");
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (skipped > 0)
                            {
                                skipped -= 5;
                            }
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (skipped + 5 < UserCRUD.cities.Count)
                            {
                                skipped += 5;
                            }
                            break;
                        }
                }
            }

            City c = null;
            int cityId = 1;
            while (c == null) 
            {
              Console.Clear();
              Console.WriteLine("Ведіть Id: ");
                
              cityId = EnterIntValue();
               

              c = UserCRUD.cities.Where(c => c.id == cityId).FirstOrDefault();
            }


            string query = $"INSERT INTO [dbo].[tblUser] (Name, telNummer, CityId) VALUES (N'{name}', N'{telNummer}', N'{cityId}')";
            SqlCommand command = new SqlCommand(query, this.sql);
            command.ExecuteNonQuery();

            this.users = null;
        }
        /// <summary>
        ///     Видаляє елемент з БД
        /// </summary>
        public override void Delete()
        {
            Console.WriteLine("Виберіть номер користувача та жміть Enter: ");
            Thread.Sleep(2000);
            this.Select();
            Console.WriteLine("Ведіть Id: ");
            int Id = 0;
            
            Id = EnterIntValue();

            string query = $"DELETE FROM [dbo].[tblUser] WHERE Id = {Id}";
            SqlCommand command = new SqlCommand(query, this.sql);
            command.ExecuteNonQuery();
            this.users = null;
        }
        /// <summary>
        ///     Виводить меню усіх користувачів
        /// </summary>
        public override void Select()
        {
            int skipped = 0;

            if (this.users == null) 
            {
                this.users = this.GetUsersFromDB();
            }

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

            while (keyInfo.Key != ConsoleKey.Enter) 
            {
               Console.Clear();
               foreach (var item in this.users.Skip(skipped).Take(5)) 
               {
                   Console.WriteLine(item);
               }
                Console.SetCursorPosition(15,5);
                Console.WriteLine("<<<< Enter >>>>");
                keyInfo = Console.ReadKey();
                
                switch (keyInfo.Key) 
                {
                    case ConsoleKey.LeftArrow: 
                        {
                            if (skipped > 0) 
                            {
                                skipped -= 5;
                            }
                            break; 
                        }
                    case ConsoleKey.RightArrow: 
                        {
                            if (skipped+5 < this.users.Count) 
                            {
                                skipped += 5;
                            }
                            break; 
                        }
                }
            }
        }
        /// <summary>
        ///     Оновлює інформацію про користувача
        /// </summary>
        public override void Update()
        {
            Console.WriteLine("Виберіть номер користувача та жміть Enter: ");
            Thread.Sleep(2000);
            this.Select();
            Console.WriteLine("Ведіть Id: ");
            int Id = 0;
            
            Id = EnterIntValue();

            string query = "UPDATE [dbo].[tblUser] SET ";

            bool isBegin = true;
            Console.Write("Ведіть ім'я: ");
            string name = Console.ReadLine();

            if (!string.IsNullOrEmpty(name)) 
            {
                isBegin = false;
                query += "Name = N'" + name + "'";
            }

            Console.Write("Ведіть номер телефону: ");
            string tel = Console.ReadLine();

            if (!string.IsNullOrEmpty(tel))
            {
                if (!isBegin)
                {
                    query += ", ";
                }
                else 
                {
                    isBegin = false;
                }
                query += "telNummer = " + "N'" + tel + "'";
            }

            City city = null;
            string cityId = "1";
            while (city == null) 
            {
            Console.Write("Ведіть номер міста: ");
            cityId = Console.ReadLine();
                if (!string.IsNullOrEmpty(cityId))
                {
                    if (UserCRUD.cities == null)
                    {
                        UserCRUD.cities = UserCRUD.GetCitiesFromDB(this.sql);
                    }
                    try
                    {
                        city = UserCRUD.cities.Where(delegate (City c) { return c.id == int.Parse(cityId); }).FirstOrDefault();
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Помилка вводу данних (процес анульовано)");
                        Thread.Sleep(1000);
                        return;
                    }
                }
                else 
                {
                    break;
                }
                
            }

            if (!string.IsNullOrEmpty(cityId))
            {
                if (!isBegin)
                {
                    query += ", ";
                }
                else 
                {
                    isBegin = false;
                }
                try
                {
                    query += "CityId = " + "N'" + int.Parse(cityId) + "'";
                }
                catch 
                {
                    Console.Clear();
                    Console.WriteLine("Помилка вводу данних (процес анульовано)");
                    Thread.Sleep(1000);

                    return;
                }
            }

            query += " WHERE Id = " + Id;
            if (!isBegin) 
            {
            SqlCommand command = new SqlCommand(query, this.sql);
            command.ExecuteNonQuery();
            this.users = null;
            }

        }
    }
}
