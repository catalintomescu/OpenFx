using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BloggerData.Tests
{
    [TestClass]
    public class ArticleRepositoryTests
    {
        IArticleRepository _articleRepository;
        List<Article> _randomArticles;

        [TestInitialize]
        public void Setup()
        {
            _randomArticles = SetupArticles();
            _articleRepository = SetupArticleRepository();
        }

        public List<Article> SetupArticles()
        {
            int _counter = new int();
            List<Article> _articles = BloggerInitializer.GetAllArticles();

            foreach (Article _article in _articles)
                _article.ID = ++_counter;

            return _articles;
        }

        public IArticleRepository SetupArticleRepository()
        {
            // Init repository
            var repo = new Mock<IArticleRepository>();

            // Setup mocking behavior
            repo.Setup(r => r.GetAll()).Returns(_randomArticles);

            repo.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(new Func<int, Article>(
                    id => _randomArticles.Find(a => a.ID.Equals(id))));

            repo.Setup(r => r.Add(It.IsAny<Article>()))
                .Callback(new Action<Article>(newArticle =>
                {
                    var maxArticleID = _randomArticles.Last().ID;
                    var nextArticleID = maxArticleID + 1;
                    newArticle.ID = nextArticleID;
                    newArticle.DateCreated = DateTime.Now;
                    _randomArticles.Add(newArticle);
                }));

            repo.Setup(r => r.Update(It.IsAny<Article>()))
                .Callback(new Action<Article>(x =>
                {
                    var oldArticle = _randomArticles.Find(a => a.ID == x.ID);
                    oldArticle.DateEdited = DateTime.Now;
                    oldArticle = x;
                }));

            repo.Setup(r => r.Delete(It.IsAny<Article>()))
                .Callback(new Action<Article>(x =>
                {
                    var _articleToRemove = _randomArticles.Find(a => a.ID == x.ID);

                    if (_articleToRemove != null)
                        _randomArticles.Remove(_articleToRemove);
                }));

            // Return mock implementation
            return repo.Object;
        }

        [TestMethod]
        public void RepoShouldReturnAllArticles()
        {
            var articles = _articleRepository.GetAll().ToList();

            Assert.AreEqual(articles, _randomArticles);
        }
    }
}
