using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcTodo.Entities;

namespace MvcTodo.Data
{
    public class MvcTodoContext : DbContext
    {
        public MvcTodoContext (DbContextOptions<MvcTodoContext> options)
            : base(options)
        {
        }

        public DbSet<MvcTodo.Entities.Todo> Todo { get; set; } = default!;
    }
}
