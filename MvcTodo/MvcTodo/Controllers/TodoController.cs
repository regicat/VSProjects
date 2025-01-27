using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcTodo.Entities;
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
			return View("List", new TodoListViewModel(ViewConst.ModeUncompleted, todoList)); 
		}

		public IActionResult All()
		{
			var entities = _todoService.GetAllList();
			var todoList = entities.Select(e => new TodoViewModel(
				e.TodoId,
				e.Title,
				e.Description,
				e.LimitDate,
				e.IsCompleted
			));
			return View("List", new TodoListViewModel(ViewConst.ModeAll, todoList));
		}
		public IActionResult Edit(int? id)
		{
			if (!id.HasValue) { return new NotFoundResult(); }
			var todo = _todoService.GetById(id.Value);
			if (todo == null)
			{
				return new NotFoundResult();
			}
			var vm = new TodoViewModel(todo.TodoId, todo.Title, todo.Description, todo.LimitDate, todo.IsCompleted); 
			return View("Edit", vm);
		}

		public IActionResult Show(int? id)
		{
			if(!id.HasValue) { return new NotFoundResult(); }
			var todo = _todoService.GetById(id.Value);
			if (todo == null)
			{
				return new NotFoundResult();
			}
			var vm = new TodoViewModel(todo.TodoId, todo.Title, todo.Description, todo.LimitDate, todo.IsCompleted);

			return View("Show",vm);
		}

		public IActionResult Delete(int? id)
		{
			if (!id.HasValue) { return new NotFoundResult(); }
			_todoService.Delete(id.Value);
			return Redirect($"/Todo/Index");
		}

		public IActionResult Add()
		{
			return View("Edit");
		}

		public IActionResult Check(int? id, string? checkValue, string? listMode)
		{
			
			return Redirect($"/Todo/Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Save([Bind("TodoId,Title,LimitDate,IsCompleted")] TodoViewModel? vm)
		{

			if (vm == null || !ModelState.IsValid)
			{
				return View("Edit", vm);
			}

			var todo = new Todo(vm.TodoId, vm.Title, vm.Description, vm.LimitDate, vm.IsCompleted);
			todoService.Save(todo);
			return Redirect($"/Todo/Index");
		}
	}
}
