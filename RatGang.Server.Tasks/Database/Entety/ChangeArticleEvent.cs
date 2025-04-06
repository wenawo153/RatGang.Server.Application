using Microsoft.EntityFrameworkCore;

namespace RatGang.Server.Tasks.Database.Entety;

[Index(nameof(ArticleId))]
[Index(nameof(ChangeEvents))]
public class ChangeArticleEvent
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }

    public Guid EditorId { get; set; }
    public ChangeEvents ChangeEvents { get; set; }

    public Article Article { get; set; } = default!;
}

public enum ChangeEvents
{
    Posting,
    Edit,
    Delete
}
