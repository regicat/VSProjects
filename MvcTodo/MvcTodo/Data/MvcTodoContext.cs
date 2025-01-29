using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcTodo.Entities;

namespace MvcTodo.Data
{
    public class MvcTodoContext(DbContextOptions<MvcTodoContext> options) : DbContext(options)
    {
		public virtual DbSet<MvcTodo.Entities.Todo> Todo { get; set; } = null!;

		public MvcTodoContext() : this(new DbContextOptions<MvcTodoContext>())
		{
		}
    }
}
