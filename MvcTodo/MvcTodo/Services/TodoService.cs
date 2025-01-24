using MvcTodo.Entities;
using MvcTodo.Services.Interfaces;
using MvcTodo.ViewModels;

namespace MvcTodo.Services
{
	public class TodoService : ITodoService
	{
		public IEnumerable<Todo> GetUnCompletedList()
		{
			return new List<Todo>
			{
				new Todo("やること１"),
				new Todo("やること２", new DateTime(2025,1,31)),
				new Todo(1,"やること３",null,new DateTime(2025,2,1), true),
			};
		}
	}
}
