namespace MvcTodoV9.ViewModels;

public record TodoListViewModel(string? ListMode, IEnumerable<TodoViewModel> TodoList);
