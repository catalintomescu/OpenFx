using System.Linq;
using OpenDataFx;

namespace BloggerData
{
    public class BlogRepository : RepositoryBase<Blog, BloggerEntities>, IBlogRepository
    {
        public BlogRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Blog GetBlogByName(string blogName)
        {
            var _blog = this.DbContext.Blogs.Where(b => b.Name == blogName).FirstOrDefault();

            return _blog;
        }
    }

    public interface IBlogRepository : IRepository<Blog>
    {
        Blog GetBlogByName(string blogName);
    }
}
