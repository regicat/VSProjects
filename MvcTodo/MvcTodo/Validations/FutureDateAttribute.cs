using System.ComponentModel.DataAnnotations;

namespace MvcTodo.Validations
{
	public class FutureDateAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value is not DateTime dateValue)
			{
				return ValidationResult.Success; 
			}

			return dateValue > DateTime.Now.Date ? ValidationResult.Success : // 未来日付の場合は成功
				new ValidationResult(ErrorMessage ?? "日付は未来である必要があります。");
		}

		public override bool IsValid(object? value)
		{
			if (value is not DateTime dateValue)
			{
				return true; 
			}

			return dateValue >= DateTime.Now.Date ;
		}
	}
}
