using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_JSONWebToken_API.Models;

namespace ToDoApp_JSONWebToken_API.DTOs.TaskTodo
{
    public class AddTaskTodoDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
    }
}
