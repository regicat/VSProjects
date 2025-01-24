using MvcTodo.Entities;

namespace MvcTodo.Services.Interfaces
{
	public interface ITodoService
	{
		IEnumerable<Todo> GetUnCompletedList();
	}
}
