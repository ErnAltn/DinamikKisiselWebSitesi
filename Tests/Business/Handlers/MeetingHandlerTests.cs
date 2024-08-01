
using Business.Handlers.Meetings.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Meetings.Queries.GetMeetingQuery;
using Entities.Concrete;
using static Business.Handlers.Meetings.Queries.GetMeetingsQuery;
using static Business.Handlers.Meetings.Commands.CreateMeetingCommand;
using Business.Handlers.Meetings.Commands;
using Business.Constants;
using static Business.Handlers.Meetings.Commands.UpdateMeetingCommand;
using static Business.Handlers.Meetings.Commands.DeleteMeetingCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class MeetingHandlerTests
    {
        Mock<IMeetingRepository> _meetingRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _meetingRepository = new Mock<IMeetingRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Meeting_GetQuery_Success()
        {
            //Arrange
            var query = new GetMeetingQuery();

            _meetingRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Meeting, bool>>>())).ReturnsAsync(new Meeting()
//propertyler buraya yazılacak
//{																		
//MeetingId = 1,
//MeetingName = "Test"
//}
);

            var handler = new GetMeetingQueryHandler(_meetingRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.MeetingId.Should().Be(1);

        }

        [Test]
        public async Task Meeting_GetQueries_Success()
        {
            //Arrange
            var query = new GetMeetingsQuery();

            _meetingRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Meeting, bool>>>()))
                        .ReturnsAsync(new List<Meeting> { new Meeting() { /*TODO:propertyler buraya yazılacak MeetingId = 1, MeetingName = "test"*/ } });

            var handler = new GetMeetingsQueryHandler(_meetingRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Meeting>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Meeting_CreateCommand_Success()
        {
            Meeting rt = null;
            //Arrange
            var command = new CreateMeetingCommand();
            //propertyler buraya yazılacak
            //command.MeetingName = "deneme";

            _meetingRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Meeting, bool>>>()))
                        .ReturnsAsync(rt);

            _meetingRepository.Setup(x => x.Add(It.IsAny<Meeting>())).Returns(new Meeting());

            var handler = new CreateMeetingCommandHandler(_meetingRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _meetingRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Meeting_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateMeetingCommand();
            //propertyler buraya yazılacak 
            //command.MeetingName = "test";

            _meetingRepository.Setup(x => x.Query())
                                           .Returns(new List<Meeting> { new Meeting() { /*TODO:propertyler buraya yazılacak MeetingId = 1, MeetingName = "test"*/ } }.AsQueryable());

            _meetingRepository.Setup(x => x.Add(It.IsAny<Meeting>())).Returns(new Meeting());

            var handler = new CreateMeetingCommandHandler(_meetingRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Meeting_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateMeetingCommand();
            //command.MeetingName = "test";

            _meetingRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Meeting, bool>>>()))
                        .ReturnsAsync(new Meeting() { /*TODO:propertyler buraya yazılacak MeetingId = 1, MeetingName = "deneme"*/ });

            _meetingRepository.Setup(x => x.Update(It.IsAny<Meeting>())).Returns(new Meeting());

            var handler = new UpdateMeetingCommandHandler(_meetingRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _meetingRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Meeting_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteMeetingCommand();

            _meetingRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Meeting, bool>>>()))
                        .ReturnsAsync(new Meeting() { /*TODO:propertyler buraya yazılacak MeetingId = 1, MeetingName = "deneme"*/});

            _meetingRepository.Setup(x => x.Delete(It.IsAny<Meeting>()));

            var handler = new DeleteMeetingCommandHandler(_meetingRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _meetingRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

