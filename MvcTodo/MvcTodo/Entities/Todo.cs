namespace MvcTodo.Entities
{
	public record Todo(int? TodoId, string? Title, string? Description, DateTime? LimitDate, bool IsCompleted)
	{

		public Todo(string? Title, DateTime? LimitDate) : this(null, Title, null, LimitDate, false)
		{

		}

		public Todo(string? Title) : this(null, Title, null, null, false)
		{

		}

	}
}
