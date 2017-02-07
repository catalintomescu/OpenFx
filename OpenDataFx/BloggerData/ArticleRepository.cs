using System.Linq;
using OpenDataFx;

namespace BloggerData
{
    public class ArticleRepository : RepositoryBase<Article, BloggerEntities>, IArticleRepository
    {
        public ArticleRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Article GetArticleByTitle(string articleTitle)
        {
            var _article = this.DbContext.Articles.Where(b => b.Title == articleTitle).FirstOrDefault();

            return _article;
        }
    }

    public interface IArticleRepository : IRepository<Article>
    {
        Article GetArticleByTitle(string articleTitle);
    }
}
