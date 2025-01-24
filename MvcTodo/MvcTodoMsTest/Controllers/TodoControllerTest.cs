using Moq;
using MvcTodo.Controllers;
using MvcTodo.Entities;
using MvcTodo.Services.Interfaces;

namespace MvcTodoMsTest.Controllers;

[TestClass]
public class TodoControllerTest
{
	[TestMethod]
	public void IndexTest()
	{
		var mock = new Mock<ITodoService>();
		mock.Setup(service => service.GetUnCompletedList())
			.Returns(new List<Todo>()
			{
				new()
				{
					Title = "やること１",
				}
			});
		var controller = new TodoController(mock.Object);
		var result = controller.Index();
		Assert.IsNotNull(result);

	}
	
}