namespace MvcTodo.ViewModels;

public record TodoListViewModel(string? ListMode, IEnumerable<TodoViewModel> TodoList);
