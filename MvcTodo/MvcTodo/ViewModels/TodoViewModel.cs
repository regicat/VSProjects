using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MvcTodo.Validations;

namespace MvcTodo.ViewModels
{
	public record TodoViewModel(
		[property: HiddenInput] int? TodoId,

		[property: DisplayName("タイトル")]
		[property: Required(ErrorMessage = "タイトルは必須です。")]
		string? Title,

		string? Description,

		[property: DisplayName("期限")]
		[property: BindProperty]
		[property: DataType(DataType.Date)]
		[property: DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
		[property: FutureDate(ErrorMessage = "期限は本日以降の日付を指定してください。")]
		DateTime? LimitDate,

		[property: DisplayName("状態")]
		bool IsCompleted
	);

}

