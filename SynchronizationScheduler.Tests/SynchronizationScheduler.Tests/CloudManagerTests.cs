using Microsoft.EntityFrameworkCore;
using Moq;
using SynchronizationScheduler.Application.Managers;
using SynchronizationScheduler.Domain.Models.Cloud;
using SynchronizationScheduler.Infrastructure.CloudContext;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SynchronizationScheduler.Tests
{
    public class CloudManagerTests
    {
        [Fact]
        public void GetUsers_WhenUsersExist_ReturnSomeUsers()
        {
            //Arrange
            var userData = new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Test",
                    Email = "Test"
                }
            }.AsQueryable();

            var mockSet = SetDbSetMock<User>(userData);

            var mockContext = new Mock<CloudDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var cloudManager = new CloudManager(mockContext.Object);

            //Act
            var users = cloudManager.GetUsers();

            //Assert
            Assert.Equal(1, users.Count());
        }

        [Fact]
        public void GetUsers_WhenUsersNotExist_ReturnEmpty()
        {
            //Arrange
            var userData = new List<User>().AsQueryable();

            var mockSet = SetDbSetMock<User>(userData);
            var mockContext = new Mock<CloudDbContext>();

            mockContext.Setup(c => c.Users).Returns(mockSet.Object);
            var cloudManager = new CloudManager(mockContext.Object);

            //Act
            var users = cloudManager.GetUsers().ToList();

            //Assert
            Assert.Empty(users);
        }

        [Fact]
        public void GetPosts_WhenPostsExist_ReturnSomePosts()
        {
            //Arrange
            var userData = new List<Post>().AsQueryable();

            var mockSet = SetDbSetMock(userData);

            var mockContext = new Mock<CloudDbContext>();
            mockContext.Setup(c => c.Posts).Returns(mockSet.Object);

            var cloudManager = new CloudManager(mockContext.Object);

            //Act
            var posts = cloudManager.GetPosts().ToList();

            //Assert
            Assert.Empty(posts);
        }

        [Fact]
        public void GetPosts_WhenPostsNotExist_ReturnEmpty()
        {
            //Arrange
            var userData = new List<Post>
            {
                new Post
                {
                    Id = 1
                }
            }.AsQueryable();

            var mockSet = SetDbSetMock(userData);

            var mockContext = new Mock<CloudDbContext>();
            mockContext.Setup(c => c.Posts).Returns(mockSet.Object);

            var cloudManager = new CloudManager(mockContext.Object);

            //Act
            var posts = cloudManager.GetPosts();

            //Assert
            Assert.Equal(1, posts.Count());
        }

        [Fact]
        public void GetPosts_WhenCommentsExist_ReturnSomeComments()
        {
            //Arrange
            var userData = new List<Comment>
            {
                new Comment
                {
                    Id = 1
                }
            }.AsQueryable();

            var mockSet = SetDbSetMock(userData);

            var mockContext = new Mock<CloudDbContext>();
            mockContext.Setup(c => c.Comments).Returns(mockSet.Object);

            var cloudManager = new CloudManager(mockContext.Object);

            //Act
            var comments = cloudManager.GetComments();

            //Assert
            Assert.Equal(1, comments.Count());
        }

        [Fact]
        public void GetPosts_WhenCommentsNotExist_ReturnEmpty()
        {
            //Arrange
            var userData = new List<Comment>().AsQueryable();
            var mockSet = SetDbSetMock(userData);

            var mockContext = new Mock<CloudDbContext>();
            mockContext.Setup(c => c.Comments).Returns(mockSet.Object);

            var cloudManager = new CloudManager(mockContext.Object);

            //Act
            var comments = cloudManager.GetComments().ToList();

            //Assert
            Assert.Empty(comments);
        }


        private Mock<DbSet<T>> SetDbSetMock<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }
    }
}
