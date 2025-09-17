namespace console_todo_manager
{
    internal class Program
    {
        public static List<ToDoItem> todos = new List<ToDoItem>();

        static void Main(string[] args)
        {
            string[] menuItems = new string[]
            {
                "Показать все задачи",
                "Добавить новую задачу",
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
                                Environment.Exit(0);
                                break;

                        }
                        break;



                }
            }

        }

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

        private static void ShowAllTasks()
        {
            Console.Clear();
            Console.WriteLine("Список всех задач");
            Console.WriteLine("=================");

            if (todos.Count == 0)
            {
                Console.WriteLine("Задачи отсутствуют.");
            }
            else
            {
                for (int i = 0; i < todos.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {todos[i].Task}");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        

        private static void DrawMenu(string[] items, int row, int col, int index)
        {
            
            Console.SetCursorPosition(col, row);
            for (int i = 0; i < items.Length; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;

                }
                Console.WriteLine(items[i]);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}
