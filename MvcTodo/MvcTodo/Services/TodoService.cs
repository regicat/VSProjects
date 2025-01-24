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
				new()
				{
					Title = "やること１",
					LimitDate = DateTime.Now,
				},
				new ()
				{
					Title = "やること２"
				}
			};
		}
	}
}
