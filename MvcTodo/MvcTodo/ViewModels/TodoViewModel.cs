using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcTodo.ViewModels
{
	public record TodoViewModel(
		[property:HiddenInput]
		int? TodoId, 
		[property: DisplayName("タイトル")] 
		string? Title, 
		string? Description,
		[property: DisplayName("期限"), BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
		DateTime? LimitDate,
		[property: DisplayName("状態")] bool IsCompleted)
	{
		public TodoViewModel(string? Title, DateTime? LimitDate) : this(null, Title, null, LimitDate, false)
		{

		}

		public TodoViewModel(string? Title) : this(null, Title, null, null, false)
		{

		}

	}
}
