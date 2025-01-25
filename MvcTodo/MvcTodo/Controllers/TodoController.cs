using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcTodo.Services;
using MvcTodo.Services.Interfaces;
using MvcTodo.ViewModels;

namespace MvcTodo.Controllers
{
	public class TodoController(ITodoService todoService) : Controller
	{

		private readonly ITodoService _todoService = todoService;

		public IActionResult Index()
		{
			//var entities = new TodoService().GetUnCompletedList();
			var entities = _todoService.GetUnCompletedList();
			var todoList = entities.Select(e => new TodoViewModel(
				e.TodoId,
				e.Title,
				e.Description,
				e.LimitDate,
				e.IsCompleted
			));
			return View("List", new TodoListViewModel("未完", todoList)); 
		}

		public IActionResult All()
		{
			var entities = _todoService.GetUnCompletedList();
			var todoList = entities.Select(e => new TodoViewModel(
				e.TodoId,
				e.Title,
				e.Description,
				e.LimitDate,
				e.IsCompleted
			));
			return View("List", new TodoListViewModel("全て", todoList));
		}
		public IActionResult Edit(int? id)
		{
			if (id.HasValue)
			{
				Debug.Print(id.Value.ToString());
			}

			var vm = new TodoViewModel("やること１", new DateTime(2025, 1, 31));
			return View(vm);
		}

		public IActionResult Show(int? id)
		{
			if (id.HasValue)
			{
				Debug.Print(id.Value.ToString());
			}
			var vm = new TodoViewModel("やること１", new DateTime(2025, 1, 31));

			return View("Edit",vm);
		}

		public IActionResult Delete(int? id)
		{
			if (id.HasValue)
			{
				Debug.Print(id.Value.ToString());
			}
			var vm = new TodoViewModel("やること１", new DateTime(2025, 1, 31));

			return View("Edit", vm);
		}

		public IActionResult Add()
		{
			return View("List");
		}

		public IActionResult Check(int? id, string? checkValue, string? listMode)
		{
			if (checkValue != null)
			{
				Debug.Print(checkValue);
			}
			return View("List");
		}

		[HttpPost]
		public IActionResult Save()
		{
			return View("List");
		}
	}
}
