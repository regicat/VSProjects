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

	}
}
