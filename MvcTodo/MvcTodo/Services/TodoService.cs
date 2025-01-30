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
			if (entity.Id.HasValue)
			{
				var old = GetById(entity.Id.Value);
				if (old != null)
				{
					old.Title = entity.Title;
					old.Description = entity.Description;
					old.LimitDate = entity.LimitDate;
					old.IsCompleted = entity.IsCompleted;
					dbContext.Todo.Update(old);
					return dbContext.SaveChanges();
				}
			}
			entity.Id = null;
			dbContext.Todo.Add(entity);
			return dbContext.SaveChanges();
		}
	}
}
