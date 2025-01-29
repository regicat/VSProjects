using Microsoft.AspNetCore.Mvc;
using Moq;
using MvcTodo.Controllers;
using MvcTodo.Entities;
using MvcTodo.Services.Interfaces;
using MvcTodo.ViewModels;
using MvcTodoTest.Utilities;

namespace MvcTodoTest.Controllers
{
	[TestFixture]
	public class TodoControllerTest
	{
		[Test]
		public void IndexTest()
		{
			var mock = new Mock<ITodoService>();
			var entityList = DataUtil.CreateDummyTodoList();
			mock.Setup(service => service.GetUnCompletedList())
				.Returns(entityList);
			var expectedList = entityList
				.Select(e => new TodoViewModel(e.Id, e.Title, e.Description, e.LimitDate, e.IsCompleted))
				.ToList();

			var controller = new TodoController(mock.Object);
			var result = controller.Index();

			mock.Verify(x => x.GetUnCompletedList(), Times.Once);
			Assert.That(result, Is.TypeOf(typeof(ViewResult)));
			var actualResult = (ViewResult)result;
			Assert.That(actualResult.ViewName, Is.EqualTo("List"));
			var actual = actualResult.Model;
			Assert.That(actual, Is.InstanceOf<TodoListViewModel>());
			var (listMode, actualList) = (TodoListViewModel)actual;
			Assert.Multiple(() =>
			{
				Assert.That(listMode, Is.EqualTo(ViewConst.ModeUncompleted));
				Assert.That(actualList, Is.EquivalentTo(expectedList));
			});
		}


		[Test]
		public void AllTest()
		{
			var mock = new Mock<ITodoService>();
			var entityList = DataUtil.CreateDummyTodoList();
			mock.Setup(service => service.GetAllList())
				.Returns(entityList);
			var expectedList = entityList
				.Select(e => new TodoViewModel(e.Id, e.Title, e.Description, e.LimitDate, e.IsCompleted))
				.ToList();

			var controller = new TodoController(mock.Object);
			var result = controller.All();

			mock.Verify(x => x.GetAllList(), Times.Once);
			Assert.That(result, Is.TypeOf(typeof(ViewResult)));
			var actualResult = (ViewResult)result;
			Assert.That(actualResult.ViewName, Is.EqualTo("List"));
			var actual = actualResult.Model;
			Assert.That(actual, Is.InstanceOf<TodoListViewModel>());
			Assert.Multiple(() =>
			{
				var (listMode, actualList) = (TodoListViewModel)actual;
				Assert.That(listMode, Is.EqualTo(ViewConst.ModeAll));
				Assert.That(actualList, Is.EquivalentTo());
			});
		}

		[Test]
		public void ShowTest()
		{
			var mock = new Mock<ITodoService>();
			var todo = DataUtil.CreateDummyTodo();
			var expected = new TodoViewModel(todo.Id, todo.Title, todo.Description, todo.LimitDate, todo.IsCompleted);
			mock.Setup(m => m.GetById(It.IsAny<int>()))
				.Returns(todo)
				.Callback<int>(id =>
				{
					Assert.That(id, Is.EqualTo(1));
				});

			var controller = new TodoController(mock.Object);
			var result = controller.Show(1);
			Assert.Multiple(() =>
			{
				mock.Verify(x => x.GetById(1), Times.Once);
				Assert.That(result, Is.InstanceOf<ViewResult>());
				var actualResult = (ViewResult)result;
				Assert.That(actualResult.ViewName, Is.EqualTo("Show"));
				var actualModel = actualResult.Model;
				Assert.That(actualModel, Is.InstanceOf<TodoViewModel>());
				if (actualModel is not TodoViewModel actual) return;
				Assert.That(actual, Is.EqualTo(expected));
			});

		}

		[Test]
		public void ShowNotFoundTest()
		{
			var mock = new Mock<ITodoService>();
			mock.Setup(m => m.GetById(It.IsAny<int>()))
				.Returns((Todo?)null);

			var controller = new TodoController(mock.Object);
			var actual = controller.Show(1);
			mock.Verify(x => x.GetById(1), Times.Once);
			Assert.That(actual, Is.InstanceOf<NotFoundResult>());

		}

		[Test]
		public void ShowIdNullTest()
		{
			var mock = new Mock<ITodoService>();
			mock.Setup(m => m.GetById(It.IsAny<int>()));

			var controller = new TodoController(mock.Object);
			var actual = controller.Show(null);
			mock.Verify(x => x.GetById(1), Times.Never);
			Assert.That(actual, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public void EditTest()
		{
			var mock = new Mock<ITodoService>();
			var todo = DataUtil.CreateDummyTodo();
			var expected = new TodoViewModel(todo.Id, todo.Title, todo.Description, todo.LimitDate, todo.IsCompleted);
			mock.Setup(m => m.GetById(It.IsAny<int>()))
				.Returns(todo)
				.Callback<int>(id =>
				{
					Assert.That(id, Is.EqualTo(1));
				});

			var controller = new TodoController(mock.Object);
			var result = controller.Edit(1);
			Assert.Multiple(() =>
			{
				mock.Verify(x => x.GetById(1), Times.Once);
				Assert.That(result, Is.InstanceOf<ViewResult>());
				var actualResult = (ViewResult)result;
				Assert.That(actualResult.ViewName, Is.EqualTo("Edit"));
				var actualModel = actualResult.Model;
				Assert.That(actualModel, Is.InstanceOf<TodoViewModel>());
				if (actualModel is not TodoViewModel actual) return;
				Assert.That(actual, Is.EqualTo(expected));
			});

		}

		[Test]
		public void EditNotFoundTest()
		{
			var mock = new Mock<ITodoService>();
			mock.Setup(m => m.GetById(It.IsAny<int>()))
				.Returns((Todo?)null);

			var controller = new TodoController(mock.Object);
			var actual = controller.Edit(1);
			mock.Verify(x => x.GetById(1), Times.Once);
			Assert.That(actual, Is.InstanceOf<NotFoundResult>());

		}
		[Test]
		public void EditIdNullTest()
		{
			var mock = new Mock<ITodoService>();
			mock.Setup(m => m.GetById(It.IsAny<int>()));

			var controller = new TodoController(mock.Object);
			var actual = controller.Edit(null);
			mock.Verify(x => x.GetById(1), Times.Never);
			Assert.That(actual, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public void AddTest()
		{
			var mock = new Mock<ITodoService>();

			var controller = new TodoController(mock.Object);
			var actualResult = controller.Add();
			Assert.That(actualResult, Is.InstanceOf<ViewResult>());
			var actual = (ViewResult)actualResult;
			Assert.Multiple(() =>
			{
				Assert.That(actual.ViewName, Is.EqualTo("Edit"));
				Assert.That(actual.Model, Is.Null);
			});
		}

		[Test]
		public void DeleteTest()
		{
			var mock = new Mock<ITodoService>();
			var todo = DataUtil.CreateDummyTodo();
			mock.Setup(m => m.Delete(It.IsAny<int>()))
				.Callback<int>(id =>
				{
					Assert.That(id, Is.EqualTo(1));
				});
			var controller = new TodoController(mock.Object);
			var result = controller.Delete(1);
			Assert.Multiple(() => 
				{
					mock.Verify(x => x.Delete(1), Times.Once);
					Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
					var actualResult = (RedirectToActionResult)result;
					Assert.That(actualResult.ActionName, Is.EqualTo("Index"));
					Assert.That(actualResult.ControllerName, Is.EqualTo("Todo"));
				});
		}

		[Test]
		public void DeleteIdNullTest()
		{
			var mock = new Mock<ITodoService>();
			mock.Setup(m => m.Delete(It.IsAny<int>()))
				.Callback<int>(id =>
				{
					Assert.That(id, Is.EqualTo(1));
				});
			var controller = new TodoController(mock.Object);
			var actual = controller.Delete(null);
			mock.Verify(x => x.Delete(1), Times.Never);
			Assert.That(actual, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public void SaveTest()
		{
			var mock = new Mock<ITodoService>();
			var todo = DataUtil.CreateDummyTodo();
			var expected = new TodoViewModel(todo.Id, todo.Title, todo.Description, todo.LimitDate, todo.IsCompleted);
			mock.Setup(m => m.Save(It.IsAny<Todo>()))
				.Callback<Todo>(e =>
				{
					Assert.That(e, Is.EqualTo(todo));
				});

			var controller = new TodoController(mock.Object);
			var vm = new TodoViewModel(todo.Id, todo.Title, todo.Description, todo.LimitDate, todo.IsCompleted);
			var result = controller.Save(vm);
			Assert.Multiple(() =>
			{
				mock.Verify(x => x.Save(todo), Times.Once);
				Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
				var actualResult = (RedirectToActionResult)result;
				Assert.That(actualResult.ActionName, Is.EqualTo("Index"));
				Assert.That(actualResult.ControllerName, Is.EqualTo("Todo"));
				});
		}

		[Test]
		public void SaveValidationErrorTest()
		{
			var mock = new Mock<ITodoService>();
			mock.Setup(m => m.Save(It.IsAny<Todo>()));

			var controller = new TodoController(mock.Object);
			controller.ModelState.AddModelError("Title", "エラー");
			var todo = DataUtil.CreateDummyTodo() with { Title = null };
			var vm = new TodoViewModel(todo.Id, todo.Title, todo.Description, todo.LimitDate, todo.IsCompleted);
			var result = controller.Save(vm);
			Assert.Multiple(() =>
			{
				mock.Verify(x => x.Save(todo), Times.Never);
				Assert.That(result, Is.InstanceOf<ViewResult>());
				var actualResult = (ViewResult)result;
				Assert.That(actualResult.ViewName, Is.EqualTo("Edit"));
				Assert.That(actualResult.Model, Is.EqualTo(vm));
			});
		}

		[TestCase("on", "未完")]
		[TestCase("off", "未完")]
		[TestCase("on", "全て")]
		[TestCase("off", "全て")]
		[Test]
		public void CheckTest(string checkValue, string mode)
		{
			var mock = new Mock<ITodoService>();
			var todo = DataUtil.CreateDummyTodo();
			var expected = todo with { IsCompleted = checkValue == "on" };
			mock.Setup(m => m.GetById(It.IsAny<int>()))
				.Returns(todo)
				.Callback<int>(id =>
				{
					Assert.That(id, Is.EqualTo(1));
				});
			mock.Setup(m => m.Save(It.IsAny<Todo>()))
				.Callback<Todo>(e =>
				{
					Assert.That(e, Is.EqualTo(expected));
				});
			var controller = new TodoController(mock.Object);
			var result = controller.Check(1,checkValue, mode);
			Assert.Multiple(() =>
			{
				mock.Verify(x => x.GetById(1), Times.Once);
				mock.Verify(x => x.Save(expected), Times.Once);
				Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
				var actualResult = (RedirectToActionResult)result;
				var expectedActionName = mode == ViewConst.ModeUncompleted ? "Index" : "All";
				Assert.That(actualResult.ActionName, Is.EqualTo(expectedActionName));
				Assert.That(actualResult.ControllerName, Is.EqualTo("Todo"));
			});
		}

		[Test]
		public void CheckNotFoundTest()
		{
			var mock = new Mock<ITodoService>();
			mock.Setup(m => m.GetById(It.IsAny<int>()))
				.Returns((Todo?)null);

			var controller = new TodoController(mock.Object);
			var actual = controller.Check(1, "on", "All");
			mock.Verify(x => x.GetById(1), Times.Once);
			Assert.That(actual, Is.InstanceOf<NotFoundResult>());

		}
		[Test]
		public void CheckIdNullTest()
		{
			var mock = new Mock<ITodoService>();
			mock.Setup(m => m.GetById(It.IsAny<int>()));

			var controller = new TodoController(mock.Object);
			var actual = controller.Check(null, "on", "All");
			mock.Verify(x => x.GetById(1), Times.Never);
			Assert.That(actual, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public void CheckModeInvalidTest()
		{
			var mock = new Mock<ITodoService>();
			var todo = DataUtil.CreateDummyTodo();
			var expected = todo with { IsCompleted = true };
			mock.Setup(m => m.GetById(It.IsAny<int>()))
				.Returns(todo)
				.Callback<int>(id =>
				{
					Assert.That(id, Is.EqualTo(1));
				});
			var controller = new TodoController(mock.Object);
			var result = controller.Check(1, "on", "Ex");
			Assert.Multiple(() =>
			{
				mock.Verify(x => x.GetById(1), Times.Once);
				mock.Verify(x => x.Save(It.IsAny<Todo>()), Times.Once);
				Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
				var actualResult = (RedirectToActionResult)result;
				// リストモードが「未完」「全て」以外の時は未完とみなす
				Assert.That(actualResult.ActionName, Is.EqualTo("Index"));
				Assert.That(actualResult.ControllerName, Is.EqualTo("Todo"));
			});
		}
	}
}
