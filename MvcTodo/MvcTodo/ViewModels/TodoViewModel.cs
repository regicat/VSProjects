using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcTodo.ViewModels
{
	public record TodoViewModel(
		int? TodoId, 
		[property: DisplayName("タイトル")] 
		string? Title, 
		string? Description,
		[property: DisplayName("期限"), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
		DateTime? LimitDate, 
		bool IsCompleted)
	{
		public TodoViewModel(string? Title, DateTime? LimitDate) : this(null, Title, null, LimitDate, false)
		{

		}

		public TodoViewModel(string? Title) : this(null, Title, null, null, false)
		{

		}

		//public int? TodoId { get; init; } = TodoId;
		//[DisplayName("タイトル")]
		//public string? Title { get; init; } = Title;
		//public string? Description { get; init; } = Description;
		//[DisplayName("期限")]
		//[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
		//public DateTime? LimitDate { get; init; } = LimitDate;
		//public bool IsCompleted { get; init; } = IsCompleted;

		//protected bool Equals(TodoViewModel other)
		//{
		//	return TodoId == other.TodoId && Title == other.Title && Description == other.Description && Nullable.Equals(LimitDate, other.LimitDate) && IsCompleted == other.IsCompleted;
		//}

		//public override string ToString()
		//{
		//	return
		//		$"{nameof(TodoId)}: {TodoId}, {nameof(Title)}: {Title}, {nameof(Description)}: {Description}, {nameof(LimitDate)}: {LimitDate}, {nameof(IsCompleted)}: {IsCompleted}";
		//}

		//public override bool Equals(object? obj)
		//{
		//	if (obj is null) return false;
		//	if (ReferenceEquals(this, obj)) return true;
		//	return obj.GetType() == GetType() && Equals((TodoViewModel)obj);
		//}

		//public override int GetHashCode()
		//{
		//	return HashCode.Combine(TodoId, Title, Description, LimitDate, IsCompleted);
		//}




	}
}
