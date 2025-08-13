using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<TodoItem[]> GetIncompleteItemAsync()
        {
            var item1 = new TodoItem
            {
                Title = "Learn ASP.NET Core",
                DueAt = DateTimeOffset.Now.AddDays(1)
            };

            var item2 = new TodoItem
            {
                Title = "Build awesome apps",
                DueAt = DateTimeOffset.Now.AddDays(2)
            };

            return Task.FromResult(new[] { item1, item2 });
        }

        Task<bool> ITodoItemService.AddItemAsync(TodoItem newItem)
        {
            throw new NotImplementedException();
        }

        Task<bool> ITodoItemService.MarkDoneAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
