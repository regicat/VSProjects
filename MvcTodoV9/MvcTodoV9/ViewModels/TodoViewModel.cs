using Microsoft.AspNetCore.Mvc;
using MvcTodoV9.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcTodoV9.ViewModels
{
	public class TodoViewModel(
		int? todoId,
		string? title,
		DateTime? limitDate,
		bool isCompleted
	)
	{
		public TodoViewModel() : this(null, null, null, false)
		{
		}

		[HiddenInput] public int? TodoId { get; init; } = todoId;
		[DisplayName("タイトル")]
		[Required(ErrorMessage = "タイトルは必須です。")]
		public string? Title { get; init; } = title;


		[DisplayName("期限")]
		[BindProperty]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
		[FutureDate(ErrorMessage = "期限は本日以降の日付を指定してください。")]
		public DateTime? LimitDate { get; init; } = limitDate;

		[DisplayName("状態")] public bool IsCompleted { get; init; } = isCompleted;

		public override string ToString()
		{
			return
				$"{nameof(TodoId)}: {TodoId}, {nameof(Title)}: {Title}, {nameof(LimitDate)}: {LimitDate}, {nameof(IsCompleted)}: {IsCompleted}";
		}
		public void Deconstruct(out int? todoId, out string? title, out DateTime? limitDate, out bool isCompleted)
		{
			todoId = this.TodoId;
			title = this.Title;
			limitDate = this.LimitDate;
			isCompleted = this.IsCompleted;
		}
	}
}

