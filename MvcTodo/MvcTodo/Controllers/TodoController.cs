﻿using System.Diagnostics;
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
			var vm = entities.Select(e => new TodoViewModel
			{
				TodoId = e.TodoId,
				Title = e.Title,
				LimitDate = e.LimitDate,
				IsCompleted = e.IsCompleted,
			});
			return View("List", vm); 
		}

		public IActionResult All()
		{
			var vm = new List<TodoViewModel>
			{
				new TodoViewModel()
				{
					Title = "やること１",
					LimitDate = DateTime.Now,
				},
				new TodoViewModel()
				{
					Title = "やること２"
				}
			};
			return View("List", vm);
		}
		public IActionResult Edit(int? id)
		{
			if (id.HasValue)
			{
				Debug.Print(id.Value.ToString());
			}
			var vm = new TodoViewModel()
				{
					Title = "やること１",
					LimitDate = DateTime.Now,
				};
			return View(vm);
		}

		public IActionResult Show(int? id)
		{
			if (id.HasValue)
			{
				Debug.Print(id.Value.ToString());
			}
			var vm = new TodoViewModel()
			{
				Title = "やること１",
				LimitDate = DateTime.Now,
			};
			return View("Edit",vm);
		}

		public IActionResult Delete(int? id)
		{
			if (id.HasValue)
			{
				Debug.Print(id.Value.ToString());
			}
			var vm = new TodoViewModel()
			{
				Title = "やること１",
				LimitDate = DateTime.Now,
			};
			return View("Edit", vm);
		}

		public IActionResult Add()
		{
			return View("Index");
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
