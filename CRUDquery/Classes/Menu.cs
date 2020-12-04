using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDquery.Classes
{
    /// <summary>
    ///     Клас, що відповідає за відображення меню
    /// </summary>
    static class Menu
    {
        /// <summary>
        ///     Відображає початкове меню
        /// </summary>
        /// <param name="conn">Приймає строку підключення</param>
        public static void StartMenu(string conn)
        {
            int counter = 1;
            while (true)
            {
                ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
                while (keyInfo.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.WriteLine("________MENU________");
                    if (counter == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine("1. для Міст");
                    Console.ForegroundColor = ConsoleColor.White;
                    if (counter == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine("2. для Користувачів");
                    Console.ForegroundColor = ConsoleColor.White;
                    if (counter == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine("3. Вихід");
                    Console.ForegroundColor = ConsoleColor.White;

                    keyInfo = Console.ReadKey();
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.DownArrow:
                            {
                                if (counter < 3)
                                {
                                    counter++;
                                }
                                else
                                {
                                    counter = 1;
                                }
                                break;
                            }
                        case ConsoleKey.UpArrow:
                            {
                                if (counter > 1)
                                {
                                    counter--;
                                }
                                else
                                {
                                    counter = 3;
                                }
                                break;
                            }

                    }
                }

                switch (counter)
                {
                    case 1: { MenuForCity(conn); break; }
                    case 2: { MenuForUser(conn); break; }
                    case 3: { return; }
                }
            }

        }
        /// <summary>
        ///     Відображає меню для роботи з користувачами
        /// </summary>
        /// <param name="conn">Приймає строку підключення</param>
        public static void MenuForUser(string conn)
        {
            using (UserCRUD user = new UserCRUD(conn))
            {
                int counter = 1;
                while (true)
                {
                    ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

                    while (keyInfo.Key != ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine("_________MENU_________");
                        if (counter == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine("1. Переглянути усі записи");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (counter == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine("2. Добавити запис");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (counter == 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine("3. Редагувати запис");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (counter == 4)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine("4. Видалити запис");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (counter == 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine("5. Вийти");
                        Console.ForegroundColor = ConsoleColor.White;

                        keyInfo = Console.ReadKey();

                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.UpArrow:
                                {
                                    if (counter > 1)
                                    {
                                        counter--;
                                    }
                                    else
                                    {
                                        counter = 5;
                                    }
                                    break;
                                }
                            case ConsoleKey.DownArrow:
                                {
                                    if (counter < 5)
                                    {
                                        counter++;
                                    }
                                    else
                                    {
                                        counter = 1;
                                    }
                                    break;
                                }
                        }
                    }

                    switch (counter)
                    {
                        case 1: { user.Select(); break; }
                        case 2: { user.Create(); break; }
                        case 3: { user.Update(); break; }
                        case 4: { user.Delete(); break; }
                        case 5: { return; }

                    }
                }

            }
        }
        /// <summary>
        ///     Відображає меню для роботи з містами
        /// </summary>
        /// <param name="conn">Приймає строку підключення</param>
        public static void MenuForCity(string conn)
        {
            using (CityCRUD city = new CityCRUD(conn))
            {
                int counter = 1;
                while (true)
                {
                    ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

                    while (keyInfo.Key != ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine("_________MENU_________");
                        if (counter == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine("1. Переглянути усі записи");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (counter == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine("2. Добавити запис");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (counter == 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine("3. Редагувати запис");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (counter == 4)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine("4. Видалити запис");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (counter == 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine("5. Вийти");
                        Console.ForegroundColor = ConsoleColor.White;

                        keyInfo = Console.ReadKey();

                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.UpArrow:
                                {
                                    if (counter > 1)
                                    {
                                        counter--;
                                    }
                                    else
                                    {
                                        counter = 5;
                                    }
                                    break;
                                }
                            case ConsoleKey.DownArrow:
                                {
                                    if (counter < 5)
                                    {
                                        counter++;
                                    }
                                    else
                                    {
                                        counter = 1;
                                    }
                                    break;
                                }
                        }
                    }

                    switch (counter)
                    {
                        case 1: { city.Select(); break; }
                        case 2: { city.Create(); break; }
                        case 3: { city.Update(); break; }
                        case 4: { city.Delete(); break; }
                        case 5: { return; }

                    }
                }

            }
        }
    }
}
