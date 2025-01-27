using System.ComponentModel.DataAnnotations;

namespace MvcTodo.Entities
{
	public record Todo(
		int? Id, 
		string? Title, 
		string? Description,
		[property:DataType(DataType.Date)]
		DateTime? LimitDate, 
		bool IsCompleted)
	{

		public Todo(string? Title, DateTime? LimitDate) : this(null, Title, null, LimitDate, false)
		{

		}

		public Todo(string? Title) : this(null, Title, null, null, false)
		{

		}

	}
}
