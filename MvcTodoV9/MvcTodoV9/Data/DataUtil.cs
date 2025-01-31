using MvcTodoV9.Entities;

namespace MvcTodoV9.Data;

public class DataUtil
{
	public static Todo CreateDummyTodo()
	{
		return new Todo(1, "やること１", string.Empty, new DateTime(2025, 1, 31), false);
	}

	public static List<Todo> CreateDummyTodoList()
	{
		var entityList = new List<Todo>()
		{
			new Todo(1,"やること１",string.Empty,null,false),
			new Todo(3,"やること２",null ,new DateTime(2025,1,31),false),
			new Todo(2,"やること３",null,null,true),

		};
		return entityList;
	}
}