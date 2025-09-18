using System.Globalization;

namespace console_todo_manager
{
    internal class Program
    {
        public static List<ToDoItem> todos = new List<ToDoItem>();

        static void Main(string[] args)
        {
            string[] menuItems =
            {
                "Показать все задачи",
                "Добавить новую задачу",
                "Изменить статус задачи",
                "Удалить задачу",
                "Выход"
            };

            int row = Console.CursorTop;
            int col = Console.CursorLeft;
            int index = 0;
            while (true)
            {
                Console.Clear();
                DrawMenu(menuItems, row, col, index);

                ConsoleKeyInfo ck = Console.ReadKey();
                switch (ck.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (index < menuItems.Length - 1)
                            index++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (index > 0)
                            index--;
                        break;
                    case ConsoleKey.Enter:
                        switch (index)
                        {
                            case 0:
                                ShowAllTasks();
                                break;
                            case 1:
                                AddNewTask();
                                break;
                            case 2:
                                ChangeStatusOfTask();
                                break;
                            case 3: 
                                DeleteTask();
                                break;
                            case 4:
                                return;
                        }
                        break;
                }
            }

        }


        // Метод для добавления новой задачи
        private static void AddNewTask()
        {
            Console.Clear();
            Console.WriteLine("Добавление новой задачи");
            Console.WriteLine("=======================");
            Console.Write("Введите задачу: ");
            string taskText = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(taskText))
            {
                ToDoItem newTask = new ToDoItem(taskText);
                todos.Add(newTask);
                Console.WriteLine("\nЗадача успешно добавлена!");
            }
            else
            {
                Console.WriteLine("\nОшибка: задача не может быть пустой!");
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // Метод для показа всех задач
        private static void ShowAllTasks()
        {
            Console.Clear();
            Console.WriteLine("Список всех задач");
            Console.WriteLine("=================");

            if (todos.Count == 0)
                Console.WriteLine("Задачи отсутствуют.");
            else
                PrintAllTasks();

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }


        // Вывод списка задач в консоль
        private static void PrintAllTasks()
        {
            for (int i = 0; i < todos.Count; i++)
            {

                Console.WriteLine("|-----------------------------------" +
                    "-------------------------------------------------|");
                if (todos[i].IsDone)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[+] ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[-] ");
                }

                Console.ResetColor();
                Console.WriteLine($"ID:{todos[i].Id} - {todos[i].Task}");
                Console.WriteLine($"   Создано: {todos[i].CreatedDate:dd.MM.yyyy HH:mm}");
            }
        }



        // Метод для изменения статуса 
        private static void ChangeStatusOfTask()
        {
            Console.Clear();
            Console.WriteLine("Изменение статуса задачи\n");

            if (todos.Count == 0)
            {
                Console.WriteLine("Задачи отсутствуют.");
                Console.WriteLine("Нажмите любую клавишу для возврата в меню");
                Console.ReadKey();
                return;
            }

            PrintAllTasks();

            int taskId = -1;

            
            while (true)
            {
                Console.WriteLine("\nВыберите и напишите ID задачи, которой хотите изменить статус на 'Выполнено'");
                Console.WriteLine("Или введите '0' для выхода в меню");

                string input = Console.ReadLine();

                // Выход в меню
                if (input == "0")
                    return;

                if (!int.TryParse(input, out taskId)) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка: введите корректное значение ID!");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                else 
                    break; // Выходим из цикла, если ввод корректен
                
            }


            bool f = false;
            string completedTaskText = "";

            for (int i = 0; i < todos.Count; i++)
            {
                if (todos[i].Id == taskId)
                {
                    if (!todos[i].IsDone)
                    {
                        todos[i].IsDone = true;
                        f = true;
                        completedTaskText = todos[i].Task;
                    }
                    else
                    {
                        Console.WriteLine("Эта задача уже выполнена!");
                        Console.WriteLine("Нажмите любую клавишу для возврата в меню");
                        Console.ReadKey();
                        return;
                    }
                }
            }

            if (f)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nЗадача: '{completedTaskText}' выполнена!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Нет задачи с таким ID");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню");
            Console.ReadKey();
        }



        // Метод для рисовки меню
        private static void DrawMenu(string[] items, int row, int col, int index)
        {
            
            Console.SetCursorPosition(col, row);
            for (int i = 0; i < items.Length; i++)
            {
                if (i == index)
                    Console.BackgroundColor = ConsoleColor.DarkGreen;

                Console.WriteLine(items[i]);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        // Метод для удаления задачи

        private static void DeleteTask()
        {
            Console.Clear();
            Console.WriteLine("Удаление задачи");

            if (todos.Count == 0)
            {
                Console.WriteLine("Задачи отсутствуют.");
                Console.WriteLine("Нажмите любую клавишу для возврата в меню");
                Console.ReadKey();
                return;
            }

            PrintAllTasks();
            int taskId = -1;

            while (true)
            {
                Console.WriteLine("\nВыберите и напишите ID задачи, которую хотите удалить'");
                Console.WriteLine("Или введите '0' для выхода в меню");

                string input = Console.ReadLine();

                // Выход в меню
                if (input == "0")
                    return;

                if (!int.TryParse(input, out taskId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка: введите корректное значение ID!");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                else
                    break; // Выходим из цикла, если ввод корректен

            }


            bool f = false;
            string deletedTaskText = "";
            for(int i = 0; i < todos.Count; i++)
            {
                if (todos[i].Id == taskId)
                {
                    deletedTaskText = todos[i].Task;
                    todos.RemoveAt(i);
                    f = true;
                    break;
                }
            }

            if (f)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nЗадача: '{deletedTaskText}' удалена!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Нет задачи с таким ID");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}
