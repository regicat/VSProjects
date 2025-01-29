using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MvcTodo.Data;
using MvcTodo.Entities;
using MvcTodo.Services.Interfaces;
using MvcTodo.ViewModels;
using NuGet.Protocol;

namespace MvcTodo.Services
{
	public class TodoService(MvcTodoContext dbContext) : ITodoService
	{
		public int Delete(int id)
		{
			var entity = GetById(id);
			if(entity == null) return 0;
			dbContext.Todo.Remove(entity);
			return dbContext.SaveChanges();

		}

		public IEnumerable<Todo> GetAllList()
		{
			return dbContext.Todo;
		}

		public Todo? GetById(int id)
		{
			return dbContext.Todo.SingleOrDefault(d => d.Id == id);
		}

		public IEnumerable<Todo> GetUnCompletedList()
		{
			return dbContext.Todo.Where(d => !d.IsCompleted);
		}

		public int Save(Todo entity)
		{
			if (dbContext.Todo.Any(d => d.Id == entity.Id))
			{
				dbContext.Todo.Update(entity);
			}
			else
			{
				dbContext.Todo.Add(entity);
			}
			return dbContext.SaveChanges();
		}
	}
}
