using System.Data.Entity;

namespace BloggerData
{
    public class BloggerEntities : DbContext
    {
        public BloggerEntities()
            : base("BloggerEntities")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Article> Articles { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleConfiguration());
            modelBuilder.Configurations.Add(new BlogConfiguration());
        }
    }
}
