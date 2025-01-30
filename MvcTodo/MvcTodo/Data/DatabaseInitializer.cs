using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using MvcTodo.Entities;
using MvcTodoTest.Utilities;

namespace MvcTodo.Data;

public class DatabaseInitializer(IServiceProvider serviceProvider) : IHostedService
{
	public async Task StartAsync(CancellationToken cancellationToken)
	{
		using var scope = serviceProvider.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<MvcTodoContext>();

		await dbContext.Database.EnsureCreatedAsync(cancellationToken);

		// テストデータの挿入
		if (!await dbContext.Todo.AnyAsync(cancellationToken))
		{
			dbContext.Todo.AddRange(CreateTestData());

			await dbContext.SaveChangesAsync(cancellationToken);
		}
	}

	private static IEnumerable<Todo> CreateTestData()
	{
		return DataUtil.CreateDummyTodoList().Select(todo =>
		{
			todo.Id = null;
			return todo;
		}).ToList();
	}

	public async Task StopAsync(CancellationToken cancellationToken)
	{
		using var scope = serviceProvider.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<MvcTodoContext>();

		await dbContext.Database.EnsureDeletedAsync(cancellationToken); // アプリ終了時に削除
	}
}