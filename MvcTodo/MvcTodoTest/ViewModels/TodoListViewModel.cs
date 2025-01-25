namespace MvcTodoTest.ViewModels;

public record TodoListViewModel(string? ListMode, IEnumerable<TodoListViewModel> TodoList);