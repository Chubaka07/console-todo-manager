using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_todo_manager
{
    public class ToDoItem
    {
        public int Id { get; set; }

        public string? Task { get; set; }

        public bool IsDone { get; set; }

        public DateTime CreatedDate { get; set; }

        Random random = new Random();
        public ToDoItem(string task)
        {
            Id = random.Next(0, 1000000);
            Task = task;
            IsDone = false;
            CreatedDate = DateTime.Now;
            
        }

    }
}
