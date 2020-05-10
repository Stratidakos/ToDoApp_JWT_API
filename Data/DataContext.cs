﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_JSONWebToken_API.Models;

namespace ToDoApp_JSONWebToken_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<TaskTodo> TasksTodo { get; set; }
        public DbSet<User> Users { get; set; }
    }
}