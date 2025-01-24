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
				new Todo(1,"やること１","",null,false),
				new Todo(3,"やること２","",new DateTime(2015,1,31),false),
				new Todo(2,"やること３","",null,true),

			};
			mock.Setup(service => service.GetUnCompletedList())
				.Returns(entityList);
			var expectedList = entityList
				.Select(e => new TodoViewModel(e.TodoId, e.Title, e.Description, e.LimitDate, e.IsCompleted))
				.ToList();

			var controller = new TodoController(mock.Object);
			var result = controller.Index();

			Assert.That(result, Is.TypeOf(typeof(ViewResult)));
			var actual = (ViewResult)result;
			Assert.That(actual.ViewName, Is.EqualTo("List"));
			var actualModel = actual.Model;
			Assert.That(actualModel, Is.InstanceOf<IEnumerable<TodoViewModel>>());
			var actualList = (IEnumerable<TodoViewModel>)actualModel;
			Assert.That(actualList, Is.EquivalentTo(expectedList));
		}
	}
}
