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
				new Todo(1,"やること１", null,null,false),
				new Todo(2,"やること２", null, new DateTime(2025,1,31), false),
				new Todo(3,"やること３",null,new DateTime(2025,2,1), true),
			};
		}
	}
}
