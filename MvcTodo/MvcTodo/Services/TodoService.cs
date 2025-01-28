using Microsoft.EntityFrameworkCore;
using MvcTodo.Data;
using MvcTodo.Entities;
using MvcTodo.Services.Interfaces;
using MvcTodo.ViewModels;
using NuGet.Protocol;

namespace MvcTodo.Services
{
	public class TodoService(MvcTodoContext dbContext) : ITodoService
	{
		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Todo> GetAllList()
		{
			throw new NotImplementedException();
		}

		public Todo GetById(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Todo> GetUnCompletedList()
		{
			return dbContext.Todo.ToList();
			//return new List<Todo>
			//{
			//	new Todo(1,"やること１", null,null,false),
			//	new Todo(2,"やること２", null, new DateTime(2025,1,31), false),
			//	new Todo(3,"やること３",null,new DateTime(2025,2,1), true),
			//};
		}

		public void Save(Todo entity)
		{
			throw new NotImplementedException();
		}
	}
}
