using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using ToDoApp_JSONWebToken_API.DTOs.TaskTodo;
using ToDoApp_JSONWebToken_API.Models;
using ToDoApp_JSONWebToken_API.Services;

namespace ToDoApp_JSONWebToken_API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TaskTodoController : ControllerBase
    {
        private readonly ITaskTodoService _taskTodoService;

        public TaskTodoController(ITaskTodoService taskTodoService)
        {
            _taskTodoService = taskTodoService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _taskTodoService.GetAllTasksTodo());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _taskTodoService.GetTaskTodoById(id));
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AddTaskTodo(AddTaskTodoDTO newTaskTodo)
        {
            return Ok(await _taskTodoService.AddTaskTodo(newTaskTodo));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskTodo(UpdateTaskTodoDTO updatedTaskTodo)
        {
            ServiceResponse<GetTaskTodoDTO> response = await _taskTodoService.UpdateTaskTodo(updatedTaskTodo);
            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            ServiceResponse<List<GetTaskTodoDTO>> response = await _taskTodoService.DeleteTaskTodo(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}