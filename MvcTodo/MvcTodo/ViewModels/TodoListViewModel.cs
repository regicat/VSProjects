namespace MvcTodo.ViewModels;

public record TodoListViewModel(string? ListMode, IEnumerable<TodoViewModel> TodoList)
{
	public const string ModeUncompleted = "未完";
	public const string ModeAll = "全て";
}
