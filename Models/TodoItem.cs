using System.ComponentModel.DataAnnotations;

/*
 * 保存在数据库里的条目（有时候也称为一个 记录(entity)）
 */

namespace AspNetCoreTodo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public bool IsDone { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTimeOffset? DueAt { get; set; }
    }
}
