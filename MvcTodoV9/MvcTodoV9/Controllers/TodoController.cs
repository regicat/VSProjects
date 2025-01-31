using Microsoft.AspNetCore.Mvc;
using MvcTodoV9.Data;
using MvcTodoV9.Entities;
using MvcTodoV9.ViewModels;

namespace MvcTodoV9.Controllers
{
	public class TodoController : Controller
	{
		public IActionResult Index()
		{
			var todoList = DataUtil.CreateDummyTodoList()
				.Select(todo => new TodoViewModel(todo.Id, todo.Title, todo.LimitDate, todo.IsCompleted));

			var viewModel = new TodoListViewModel(ViewConst.ModeUncompleted,todoList);

			return View("List", viewModel);
		}

		public IActionResult Add()
		{
			var viewModel = new TodoViewModel();
			return View("Edit", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Save([Bind("TodoId,Title,LimitDate,IsCompleted")] TodoViewModel? vm)
		{

			if (vm == null || !ModelState.IsValid)
			{
				return View("Edit", vm);
			}

			var todo = new Todo(vm.TodoId, vm.Title, null, vm.LimitDate, vm.IsCompleted);
			//todoService.Save(todo);
			return RedirectToAction("Index", "Todo");
		}
	}
}
