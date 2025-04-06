using Microsoft.EntityFrameworkCore;

namespace RatGang.Server.Tasks.Database.Entety;

[Index(nameof(Name))]
[Index(nameof(LastEditTime))]
public class Article
{
    public Guid Id { get; set; }
    public Guid LastEditorId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long LastEditTime { get; set; } = 0;

    public string Content { get; set; } = string.Empty;
    public string ImageId { get; set; } = string.Empty;

    public List<ChangeArticleEvent> ArticleEvents { get; set; } = [];
}
