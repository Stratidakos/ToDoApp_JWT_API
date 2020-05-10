using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_JSONWebToken_API.Models;

namespace ToDoApp_JSONWebToken_API.DTOs.TaskTodo
{
    public class UpdateTaskTodoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public User User { get; set; }
    }
}
