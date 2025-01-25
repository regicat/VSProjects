using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MvcTodo.ViewModels;
using MvcTodoTest.Utilities;

namespace MvcTodoTest.ViewModels;

[TestFixture]
public class TodoViewModelTest
{
	[Test]
	public void ViewModelTest()
	{
		var actual = new TodoViewModel(1, "やること１", "", new DateTime(2025,1,31), false);
		Assert.Multiple(() =>
		{
			Assert.That(TestUtil.GetDisplayName(actual, "Title"), Is.EqualTo("タイトル"));
			Assert.That(TestUtil.GetDisplayName(actual, "LimitDate"), Is.EqualTo("期限"));
			Assert.That(TestUtil.GetFormattedDate(actual, "LimitDate"), Is.EqualTo("2025/01/31"));
		});
	}
}