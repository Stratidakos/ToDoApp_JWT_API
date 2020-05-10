using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_JSONWebToken_API.DTOs.TaskTodo;
using ToDoApp_JSONWebToken_API.Models;

namespace ToDoApp_JSONWebToken_API.Services
{
    public interface ITaskTodoService
    {
        Task<ServiceResponse<List<GetTaskTodoDTO>>> GetAllTasksTodo();
        Task<ServiceResponse<GetTaskTodoDTO>> GetTaskTodoById(int id);
        Task<ServiceResponse<List<GetTaskTodoDTO>>> AddTaskTodo(AddTaskTodoDTO newTaskTodo);
        Task<ServiceResponse<GetTaskTodoDTO>> UpdateTaskTodo(UpdateTaskTodoDTO updatedTaskTodo);
        Task<ServiceResponse<List<GetTaskTodoDTO>>> DeleteTaskTodo(int id);
    }
}
