using RatGang.Server.Tasks.Database.Entety;
using RatGang.Server.Tasks.Entety.Request;

namespace RatGang.Server.Tasks.Services;

public interface IArticleService
{
    public Article GetArticle(Guid id);
    public List<Article> GetArticles(Guid userId);
    public List<Article> GetArticles(Guid userId, long lastEditTime);
    public List<Article> GetArticles(Guid userId, int count, int offset);
    public List<Article> GetArticles(int offset, int count);

    public Article SetArticle(CreateArticleRequest options);

    public Article EditArticle(EditArticleRequest options);

    public void DeleteArticle(Guid id);
    public void DeleteArticles(List<Guid> ids);
}
