using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcTodoV9.Entities;

namespace MvcTodoV9.Data
{
    public class MvcTodoV9Context : DbContext
    {
        public MvcTodoV9Context (DbContextOptions<MvcTodoV9Context> options)
            : base(options)
        {
        }

        public DbSet<MvcTodoV9.Entities.Todo> Todo { get; set; } = default!;
    }
}
