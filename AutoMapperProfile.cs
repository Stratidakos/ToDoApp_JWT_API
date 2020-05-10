using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_JSONWebToken_API.DTOs.TaskTodo;
using ToDoApp_JSONWebToken_API.Models;

namespace ToDoApp_JSONWebToken_API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Map creations
            CreateMap<TaskTodo, GetTaskTodoDTO>();
            CreateMap<AddTaskTodoDTO, TaskTodo>();
        }
    }
}
