using System;
using NHibernateBootstrap.Core.Domain;
using NUnit.Framework;

namespace NHibernateBootstrap.Tests
{
    [TestFixture]
    public class BlogTestFixture : InMemoryDatabaseTest
    {
        public BlogTestFixture()
            : base(typeof(Blog).Assembly)
        {
        }

        [Test]
        public void CanSaveAndLoadBlog()
        {
            object id;

            using (var tx = session.BeginTransaction())
            {
                id = session.Save(new Blog
                                      {
                                          AllowsComments = true,
                                          CreatedAt = new DateTime(2000, 1, 1),
                                          Subtitle = "Hello",
                                          Title = "World",
                                      });

                tx.Commit();
            }

            session.Clear();


            using (var tx = session.BeginTransaction())
            {
                var blog = session.Get<Blog>(id);

                Assert.AreEqual(new DateTime(2000, 1, 1), blog.CreatedAt);
                Assert.AreEqual("Hello", blog.Subtitle);
                Assert.AreEqual("World", blog.Title);
                Assert.True(blog.AllowsComments);

                tx.Commit();
            }
        }
    }
}