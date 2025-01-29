using Microsoft.EntityFrameworkCore;
using Moq;
using MvcTodo.Data;
using MvcTodo.Entities;
using MvcTodo.Services;
using MvcTodoTest.Utilities;

namespace MvcTodoTest.Services;

[TestFixture]
public class TodoServiceTest
{
	private Mock<MvcTodoContext> _mock;

	[Test]
	public void UpdateTest()
	{
		var entity = DataUtil.CreateDummyTodo();

		var service = new TodoService(_mock.Object);
		_mock.Setup(m => m.SaveChanges())
			.Returns(1);
		_mock.Setup(m => m.Update(It.IsAny<Todo>()))
			.Callback<Todo>((e) =>
			{
				Assert.That(e, Is.EqualTo(entity));
			});

		var result = service.Save(entity);
		_mock.Verify(x => x.Todo.Update(entity), Times.Once);
		_mock.Verify(x => x.Todo.Add(entity), Times.Never);
		_mock.Verify(x => x.SaveChanges(), Times.Once);

		Assert.That(result, Is.EqualTo(1));

	}
	/// <summary>
	/// DbContextをモック化するための準備
	/// </summary>
	[SetUp]
	public void SetUpMock()
	{
		var entities = DataUtil.CreateDummyTodoList().AsQueryable();
		var dbSetMock = new Mock<DbSet<Todo>>();
		dbSetMock.As<IQueryable<Todo>>().Setup(m => m.Provider).Returns(entities.Provider);
		dbSetMock.As<IQueryable<Todo>>().Setup(m => m.Expression).Returns(entities.Expression);
		dbSetMock.As<IQueryable<Todo>>().Setup(m => m.ElementType).Returns(entities.ElementType);
		using var enumerator = entities.GetEnumerator();
		dbSetMock.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(enumerator);

		_mock = new Mock<MvcTodoContext>();
		_mock.Setup(m => m.Todo)
			.Returns(dbSetMock.Object);
	}
}