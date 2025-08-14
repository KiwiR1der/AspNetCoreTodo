using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<TodoItem[]> GetIncompleteItemAsync(IdentityUser user)
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

        Task<bool> ITodoItemService.AddItemAsync(TodoItem newItem, IdentityUser user)
        {
            throw new NotImplementedException();
        }

        Task<bool> ITodoItemService.MarkDoneAsync(Guid id, IdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}
