
/*
 * 通常，你保存在数据库里的模型（实体），跟你在 MVC 里用的模型（视图模型）非常相似，但又不尽相同。
 * 在现在的情形下， TodoItem 模型代表单一的一个数据库里的条目，而视图则需要展示两个、十个，甚至是一百个待办事项（取决于用户拖延症的病情轻重）。
 */

namespace AspNetCoreTodo.Models
{
    public class TodoViewModel
    {
        public TodoItem[] Items { get; set; }
    }
}
