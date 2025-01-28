using MvcTodo.Entities;

namespace MvcTodo.Services.Interfaces
{
	public interface ITodoService
	{

		IEnumerable<Todo> GetUnCompletedList();
		IEnumerable<Todo> GetAllList();

		Todo? GetById(int id);
		void Save(Todo entity);
		void Delete(int id);

	}
}
