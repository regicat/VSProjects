using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MvcTodo.Controllers;
using MvcTodo.Entities;
using MvcTodo.Services.Interfaces;
using MvcTodo.ViewModels;
using NUnit.Framework.Legacy;

namespace MvcTodoTest.Controllers
{
	[TestFixture]
	public class TodoControllerTest
	{
		[Test]
		public void IndexTest()
		{
			var mock = new Mock<ITodoService>();
			var entityList = new List<Todo>()
			{
				new()
				{
					Title = "やること１",
					LimitDate = new DateTime(2025, 1, 28),
					
				},
				new ()
				{
					Title = "やること２"
				},
				new ()
				{
					Title = "やること３",
					LimitDate = new DateTime(2025,1,31),
					IsCompleted = true,
				}
			};
			mock.Setup(service => service.GetUnCompletedList())
				.Returns(entityList);
			var expectedList = entityList.Select(e => new TodoViewModel()
			{
				Title = e.Title,
				LimitDate = e.LimitDate,
				IsCompleted = e.IsCompleted,
			}).ToList();

			var controller = new TodoController(mock.Object);
			var result = controller.Index();

			Assert.That(result, Is.TypeOf(typeof(ViewResult)));
			var actual = (ViewResult)result;
			Assert.That(actual.ViewName, Is.EqualTo("List"));
			var actualModel = actual.Model;
			Assert.That(actualModel, Is.InstanceOf<IEnumerable<TodoViewModel>>());
			var actualList = (IEnumerable<TodoViewModel>)actualModel;
			expectedList[1].Title = "aaa";
			Assert.That(actualList, Is.EquivalentTo(expectedList));
		}
	}
}
