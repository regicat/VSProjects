using MvcTodo.Entities;

namespace MvcTodo.Services.Interfaces
{
	public interface ITodoService
	{

		IEnumerable<Todo> GetUnCompletedList();
		IEnumerable<Todo> GetAllList();

		Todo? GetById(int id);
		int Save(Todo entity);
		int Delete(int id);

	}
}
