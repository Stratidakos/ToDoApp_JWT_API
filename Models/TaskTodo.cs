using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp_JSONWebToken_API.Models
{
    public enum Status
    {
        New = 1,
        InProgress = 2,
        Completed = 3
    }

    public class TaskTodo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public User User { get; set; }
    }
}
