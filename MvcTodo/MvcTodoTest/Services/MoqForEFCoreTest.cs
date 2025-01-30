using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using MvcTodo.Data;
using MvcTodo.Entities;
using MvcTodo.Services;
using MvcTodoTest.Utilities;
namespace MvcTodoTest.Services;

[TestFixture]
public class MoqForEFCoreTest
{
	private Mock<MvcTodoContext> _mock;

	[Test]
	public void GetByIdTest()
	{
		var expected = DataUtil.CreateDummyTodoList().Single(d => d.Id == 1);
		var service = new TodoService(_mock.Object);
		var actual = service.GetById(1);
		Assert.That(actual, Is.Not.Null);
		Assert.That(actual, Is.EqualTo(expected).UsingPropertiesComparer());
	}
	[Test]
	public void GetByIdNotFoundTest()
	{
		var service = new TodoService(_mock.Object);
		var actual = service.GetById(10);
		Assert.That(actual, Is.Null);
	}

	[Test]
	public void DeleteTest()
	{
		var expected = DataUtil.CreateDummyTodoList().Single(d => d.Id == 1);
		_mock.Setup(x => x.Remove(It.IsAny<Todo>()))
			.Callback<Todo>(e =>
			{
				Assert.That(e, Is.EqualTo(expected).UsingPropertiesComparer());
			});
		var service = new TodoService(_mock.Object);
		var actual = service.Delete(1);
		_mock.Verify(x => x.Todo.Remove(It.IsAny<Todo>()));
		Assert.That(actual, Is.EqualTo(1));
	}

	[Test]
	public void DeleteNotFoundTest()
	{
		var service = new TodoService(_mock.Object);
		Assert.That(() =>
		{
			var actual = service.Delete(10);
			Assert.That(actual, Is.EqualTo(0));
			return actual;
		}, Throws.Nothing);
	}

	[Test]
	public void GetUncompletedList()
	{
		var todoList = DataUtil.CreateDummyTodoList().ToArray();
		var expectedList = todoList.Except(todoList.Where(d => d.IsCompleted)).ToArray();
		var service = new TodoService(_mock.Object);
		var actualList = service.GetUnCompletedList().ToArray();
		Assert.Multiple(() =>
		{
			Assert.That(actualList, Has.Length.EqualTo(2));
			Assert.That(actualList.All(d => !d.IsCompleted));
			Assert.That(actualList, Is.EquivalentTo(expectedList).UsingPropertiesComparer());
		});
	}

	[Test]
	public void AddTest()
	{
		var entity = DataUtil.CreateDummyTodo();
		entity.Id = null;
		_mock.Setup(m => m.Add(It.IsAny<Todo>()))
			.Callback<Todo>((e) =>
			{
				Assert.That(e, Is.EqualTo(entity));
			});

		var service = new TodoService(_mock.Object);
		var result = service.Save(entity);
		_mock.Verify(x => x.Todo.Update(entity), Times.Never);
		_mock.Verify(x => x.Todo.Add(entity), Times.Once);
		_mock.Verify(x => x.SaveChanges(), Times.Once);

		Assert.That(result, Is.EqualTo(1));
	}
	[Test]
	public void UpdateTest()
	{
		 var entity = DataUtil.CreateDummyTodo();
		_mock.Setup(m => m.Update(It.IsAny<Todo>()))
			.Callback<Todo>((e) =>
			{
				Assert.That(e, Is.EqualTo(entity));
			});

		var service = new TodoService(_mock.Object); 
		var result = service.Save(entity);
		_mock.Verify(x => x.Todo.Update(entity), Times.Once);
		_mock.Verify(x => x.Todo.Add(entity), Times.Never);
		_mock.Verify(x => x.SaveChanges(), Times.Once);

		Assert.That(result, Is.EqualTo(1));
	}

	/// <summary>
	/// DbContextをモック化
	/// </summary>
	[SetUp]
	public void SetUpMock()
	{
		_mock = new Mock<MvcTodoContext>();
		var entities = DataUtil.CreateDummyTodoList().AsQueryable();
		var dbSetMock = new Mock<DbSet<Todo>>();
		var todoList = DataUtil.CreateDummyTodoList();
		_mock.Setup(x => x.Todo).ReturnsDbSet(todoList, dbSetMock);
		_mock.Setup(m => m.SaveChanges())
			.Returns(1);
	}
}