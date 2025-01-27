using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTodoTest.Utilities
{
	public class ValidationHelper
	{
		public static IList<ValidationResult> Validate(object obj)
		{
			var validationResults = new List<ValidationResult>();
			var validationContext = new ValidationContext(obj, serviceProvider: null, items: null);
			Validator.TryValidateObject(obj, validationContext, validationResults, validateAllProperties: true);
			return validationResults;

		}
	}
}