using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using MvcTodo.ViewModels;
using MvcTodoTest.Utilities;

namespace MvcTodoTest.ViewModels;

[TestFixture]
public class TodoViewModelTest
{
	[Test]
	public void ViewModelTest()
	{
		var actual = new TodoViewModel(1, "やること１", new DateTime(2025, 1, 1), false);
		Assert.Multiple(() =>
		{
			Assert.That(TestUtil.GetDisplayName(actual, "Title"), Is.EqualTo("タイトル"));
			Assert.That(TestUtil.GetDisplayName(actual, "LimitDate"), Is.EqualTo("期限"));
			Assert.That(TestUtil.GetFormattedDate(actual, "LimitDate"), Is.EqualTo("2025/01/01"));
			Assert.That(TestUtil.GetDisplayName(actual, "IsCompleted"), Is.EqualTo("状態"));
		});
	}
	[TestCase(null, true)]
	[TestCase("", true)]
	[TestCase("やること１", false)]
	[Test]
	public void TitleValidationTest(string? title, bool expectedErrorExists)
	{
		const string expectedMessage = "タイトルは必須です。";
		var vm = new TodoViewModel(1, title, new DateTime(2025, 1, 31), false);
		var actualErrors = ValidationHelper.Validate(vm);
		if (expectedErrorExists) 
		{
			Assert.Multiple(() =>
			{
				Assert.That(actualErrors.Any());
				var actualError = actualErrors.First();
				Assert.That(actualError.ErrorMessage, Is.EqualTo(expectedMessage));
			});
		}
		else
		{
			Assert.That(!actualErrors.Any());
		}
	}
	[TestCaseSourceAttribute(nameof(LimitDateTestData))]
	[Test]
	public void LimitDateFutureValidationTest(LimitDateTestRecord data)
	{
		const string expectedMessage = "期限は本日以降の日付を指定してください。";
		var vm = new TodoViewModel(1, "title", data.Value, false);
		var actualErrors = ValidationHelper.Validate(vm);
		if (data.IsError)
		{
			Assert.Multiple(() =>
			{
				Assert.That(actualErrors.Any(), Is.True );
				var actualError = actualErrors.First();
				Assert.That(actualError.ErrorMessage, Is.EqualTo(expectedMessage));
			});
		}
		else
		{
			Assert.That(!actualErrors.Any());
		}
	}

	private static readonly LimitDateTestRecord[] LimitDateTestData =
	[
		new LimitDateTestRecord(null, false),
		new LimitDateTestRecord(DateTime.Now.Date.AddDays(-1), true),
		new LimitDateTestRecord(DateTime.Now.Date, false)
	];



	public record LimitDateTestRecord(DateTime? Value, bool IsError);
}