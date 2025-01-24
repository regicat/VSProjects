using System.ComponentModel;

namespace MvcTodo.ViewModels
{
	public class TodoViewModel(int? TodoId, string? Title, string? Description, DateTime? LimitDate, bool IsCompleted)
	{
		public int? TodoId { get; init; } = TodoId;
		public string? Title { get; init; } = Title;
		public string? Description { get; init; } = Description;
		public DateTime? LimitDate { get; init; } = LimitDate;
		public bool IsCompleted { get; init; } = IsCompleted;

		protected bool Equals(TodoViewModel other)
		{
			return TodoId == other.TodoId && Title == other.Title && Description == other.Description && Nullable.Equals(LimitDate, other.LimitDate) && IsCompleted == other.IsCompleted;
		}

		public override string ToString()
		{
			return
				$"{nameof(TodoId)}: {TodoId}, {nameof(Title)}: {Title}, {nameof(Description)}: {Description}, {nameof(LimitDate)}: {LimitDate}, {nameof(IsCompleted)}: {IsCompleted}";
		}

		public override bool Equals(object? obj)
		{
			if (obj is null) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((TodoViewModel)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(TodoId, Title, Description, LimitDate, IsCompleted);
		}




	}
}
