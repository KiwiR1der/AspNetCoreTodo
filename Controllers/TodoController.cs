using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/*
 * 由控制器本身处理的路由叫 action 
 * 一个 action 方法可以返回视图、JSON数据，或者 200 OK和404 Not Found 之类的状态码。
 * 返回类型 IActionResult 给了你足够的灵活性，以返回上面提到的任意一个。
 * 
 * ITodoItemService :
 * 接口如此有用的原因就在于，因为它们有助于解耦（分离）你程序里的逻辑。
 * 既然这个控制器依赖于 ITodoItemService 接口，而不是任何 特定的 类，它就不知道也不必关心实际使用的是哪个具体的类。
 * 它可以是 FakeTodoItemService，或者是其它读写数据库的类，或者别的什么类。
 * 只要它符合该接口的要求，控制器就能工作
 */

namespace AspNetCoreTodo.Controllers
{
    [Authorize] // 需要用户登录才能访问
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<IdentityUser> _userManager;

        public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _todoItemService.GetIncompleteItemAsync();

            var model = new TodoViewModel
            {
                Items = items
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var successful = await _todoItemService.AddItemAsync(item);

            if (!successful)
            {
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var successful = await _todoItemService.MarkDoneAsync(id);

            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }
    }
}
