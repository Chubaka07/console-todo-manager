using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace console_todo_manager
{
    internal static class FileService
    {
        private const string FilePath = "Tasks.json";

        public static bool IsFileExists()
        {
            return File.Exists(FilePath);
        }


        // Загрузка данных в файл
        public static void SaveTodosToFile(List<ToDoItem> todos)
        {
            try
            {
                string json = JsonSerializer.Serialize(todos);
                File.WriteAllText(FilePath, json);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Данные сохранены!");
                Console.ResetColor();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        // Выгрузка данных из файла
        public static List<ToDoItem> LoadTodosFromFile()
        {
            try
            {
                string json = File.ReadAllText(FilePath);

                var todos = JsonSerializer.Deserialize<List<ToDoItem>>(json);

                return todos ?? new List<ToDoItem>();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
                Thread.Sleep(30000);
                return new List<ToDoItem>();
            }
        }


    }
}
