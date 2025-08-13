using AspNetCoreTodo.Models;

/*
 * 在接口中，一个对象中方法和属性的定义与实际包含这些方法和属性的类分离开来。
 */

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemAsync();

        Task<bool> AddItemAsync(TodoItem newItem);

        Task<bool> MarkDoneAsync(Guid id);
    }
}
