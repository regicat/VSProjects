namespace MvcTodo.Entities
{
	public record Todo
	{
        public int? TodoId { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public DateTime? LimitDate { get; set; }
		public bool IsCompleted { get; set; }

		//protected bool Equals(Todo other)
		//{
		//	return TodoId == other.TodoId && Title == other.Title && Description == other.Description && Nullable.Equals(LimitDate, other.LimitDate) && IsCompleted == other.IsCompleted;
		//}

		//public override bool Equals(object? obj)
		//{
		//	if (obj is null) return false;
		//	if (ReferenceEquals(this, obj)) return true;
		//	if (obj.GetType() != GetType()) return false;
		//	return Equals((Todo)obj);
		//}

		//public override int GetHashCode()
		//{
		//	return HashCode.Combine(
		//		TodoId ?? 0, 
		//		Title, 
		//		Description, 
		//		LimitDate, 
		//		IsCompleted);

		//}
	}
}
