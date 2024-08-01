
using Business.Handlers.Articles.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Articles.Queries.GetArticleQuery;
using Entities.Concrete;
using static Business.Handlers.Articles.Queries.GetArticlesQuery;
using static Business.Handlers.Articles.Commands.CreateArticleCommand;
using Business.Handlers.Articles.Commands;
using Business.Constants;
using static Business.Handlers.Articles.Commands.UpdateArticleCommand;
using static Business.Handlers.Articles.Commands.DeleteArticleCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ArticleHandlerTests
    {
        Mock<IArticleRepository> _articleRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _articleRepository = new Mock<IArticleRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Article_GetQuery_Success()
        {
            //Arrange
            var query = new GetArticleQuery();

            _articleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Article, bool>>>())).ReturnsAsync(new Article()
//propertyler buraya yazılacak
//{																		
//ArticleId = 1,
//ArticleName = "Test"
//}
);

            var handler = new GetArticleQueryHandler(_articleRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ArticleId.Should().Be(1);

        }

        [Test]
        public async Task Article_GetQueries_Success()
        {
            //Arrange
            var query = new GetArticlesQuery();

            _articleRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Article, bool>>>()))
                        .ReturnsAsync(new List<Article> { new Article() { /*TODO:propertyler buraya yazılacak ArticleId = 1, ArticleName = "test"*/ } });

            var handler = new GetArticlesQueryHandler(_articleRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Article>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Article_CreateCommand_Success()
        {
            Article rt = null;
            //Arrange
            var command = new CreateArticleCommand();
            //propertyler buraya yazılacak
            //command.ArticleName = "deneme";

            _articleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Article, bool>>>()))
                        .ReturnsAsync(rt);

            _articleRepository.Setup(x => x.Add(It.IsAny<Article>())).Returns(new Article());

            var handler = new CreateArticleCommandHandler(_articleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _articleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Article_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateArticleCommand();
            //propertyler buraya yazılacak 
            //command.ArticleName = "test";

            _articleRepository.Setup(x => x.Query())
                                           .Returns(new List<Article> { new Article() { /*TODO:propertyler buraya yazılacak ArticleId = 1, ArticleName = "test"*/ } }.AsQueryable());

            _articleRepository.Setup(x => x.Add(It.IsAny<Article>())).Returns(new Article());

            var handler = new CreateArticleCommandHandler(_articleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Article_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateArticleCommand();
            //command.ArticleName = "test";

            _articleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Article, bool>>>()))
                        .ReturnsAsync(new Article() { /*TODO:propertyler buraya yazılacak ArticleId = 1, ArticleName = "deneme"*/ });

            _articleRepository.Setup(x => x.Update(It.IsAny<Article>())).Returns(new Article());

            var handler = new UpdateArticleCommandHandler(_articleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _articleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Article_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteArticleCommand();

            _articleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Article, bool>>>()))
                        .ReturnsAsync(new Article() { /*TODO:propertyler buraya yazılacak ArticleId = 1, ArticleName = "deneme"*/});

            _articleRepository.Setup(x => x.Delete(It.IsAny<Article>()));

            var handler = new DeleteArticleCommandHandler(_articleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _articleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

