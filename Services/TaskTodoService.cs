using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoApp_JSONWebToken_API.Data;
using ToDoApp_JSONWebToken_API.DTOs.TaskTodo;
using ToDoApp_JSONWebToken_API.Models;

namespace ToDoApp_JSONWebToken_API.Services
{
    public class TaskTodoService : ITaskTodoService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TaskTodoService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetTaskTodoDTO>>> AddTaskTodo(AddTaskTodoDTO newTaskTodo)
        {
            ServiceResponse<List<GetTaskTodoDTO>> serviceResponse = new ServiceResponse<List<GetTaskTodoDTO>>();
            TaskTodo TaskTodo = _mapper.Map<TaskTodo>(newTaskTodo);
            TaskTodo.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            await _context.TasksTodo.AddAsync(TaskTodo);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.TasksTodo.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetTaskTodoDTO>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTaskTodoDTO>>> DeleteTaskTodo(int id)
        {
            ServiceResponse<List<GetTaskTodoDTO>> serviceResponse = new ServiceResponse<List<GetTaskTodoDTO>>();
            try
            {
                TaskTodo dbTaskTodo =
                    await _context.TasksTodo.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if (dbTaskTodo != null)
                {
                    _context.TasksTodo.Remove(dbTaskTodo);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.TasksTodo.Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetTaskTodoDTO>(c))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Task not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTaskTodoDTO>>> GetAllTasksTodo()
        {
            ServiceResponse<List<GetTaskTodoDTO>> serviceResponse = new ServiceResponse<List<GetTaskTodoDTO>>();
            List<TaskTodo> dbTasksTodo = await _context.TasksTodo.Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbTasksTodo.Select(c => _mapper.Map<GetTaskTodoDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTaskTodoDTO>> GetTaskTodoById(int id)
        {
            ServiceResponse<GetTaskTodoDTO> serviceResponse = new ServiceResponse<GetTaskTodoDTO>();
            TaskTodo dbTaskTodo = await _context.TasksTodo.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetTaskTodoDTO>(dbTaskTodo);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTaskTodoDTO>> UpdateTaskTodo(UpdateTaskTodoDTO updatedTaskTodo)
        {
            ServiceResponse<GetTaskTodoDTO> serviceResponse = new ServiceResponse<GetTaskTodoDTO>();
            try
            {
                TaskTodo dbTaskTodo  = await _context.TasksTodo.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == updatedTaskTodo.Id);
                if (dbTaskTodo.User.Id == GetUserId())
                {
                    dbTaskTodo.Title = updatedTaskTodo.Title;
                    dbTaskTodo.Description = updatedTaskTodo.Description;
                    dbTaskTodo.Status = updatedTaskTodo.Status;

                    _context.TasksTodo.Update(dbTaskTodo);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetTaskTodoDTO>(dbTaskTodo);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Task not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
