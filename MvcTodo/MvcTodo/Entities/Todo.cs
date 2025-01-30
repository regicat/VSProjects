using System.ComponentModel.DataAnnotations;

namespace MvcTodo.Entities;

public class Todo(
	int? id, 
	string? title, 
	string? description,
	DateTime? limitDate, 
	bool isCompleted)
{
	public Todo(string? title, DateTime? limitDate) : this(null, title, null, limitDate, false)
	{

	}

	public Todo(string? title) : this(null, title, null, null, false)
	{

	}

	public int? Id { get; set; } = id;
	[MaxLength(1000)]
	public string? Title { get; set; } = title;
	[MaxLength(10000)]
	public string? Description { get; set; } = description;
	[DataType(DataType.Date)] public DateTime? LimitDate { get; set; } = limitDate;
	public bool IsCompleted { get; set; } = isCompleted;


	public override string ToString()
	{
		return
			$"{nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(Description)}: {Description}, {nameof(LimitDate)}: {LimitDate}, {nameof(IsCompleted)}: {IsCompleted}";
	}
}