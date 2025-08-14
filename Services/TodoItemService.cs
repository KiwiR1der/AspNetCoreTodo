using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem[]> GetIncompleteItemAsync(IdentityUser user)
        {
            /*
            * Where 方法来自 C# 里的一个名为 LINQ(language integrated query) 的特性，它受到函数式编程的启发，简化了在程序代码里数据库查询的写法。
            */
            var items = await _context.Items
                .Where(x => x.IsDone == false && x.UserId == user.Id)
                .ToArrayAsync();
            return items;
        }

        async Task<bool> ITodoItemService.AddItemAsync(TodoItem newItem, IdentityUser user)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(7);

            newItem.UserId = user.Id; // 设置新条目的 UserId 为当前用户的 Id

            _context.Items.Add(newItem);

            var result = await _context.SaveChangesAsync();
            return result == 1; // 如果有一条记录被添加到数据库里，SaveChangesAsync 方法会返回 1
        }

        async Task<bool> ITodoItemService.MarkDoneAsync(Guid id, IdentityUser user)
        {
            var item = await _context.Items
                .Where(x => x.Id == id && x.UserId == user.Id).SingleOrDefaultAsync();

            if (item == null)
            {
                return false; // 如果没有找到对应的项，返回 false
            }

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // 如果有一条记录被更新到数据库里，SaveChangesAsync 方法会返回 1
        }
    }
}
