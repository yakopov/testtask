using Akoyur.TestTask.Application.UseCases.Users;
using Akoyur.TestTask.Infrastructure.Database.Users;
using MediatR;
using Moq;

namespace Akoyur.TestTask.Application.UnitTests.UseCases.Users;

/// <summary>
/// Unit tests for the CheckEmailCanBeRegisteredQueryHandler class.
/// </summary>
[TestFixture]
public class CheckEmailCanBeRegisteredQueryHandlerTests
{
    private Mock<IMediator> _mediatorMock;
    private CheckEmailCanBeRegisteredQueryHandler _handler;

    /// <summary>
    /// Set up test environment, mocking IMediator and initializing handler.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
        _handler = new CheckEmailCanBeRegisteredQueryHandler(_mediatorMock.Object);
    }

    /// <summary>
    /// Test for handling null request, expecting ArgumentNullException.
    /// </summary>
    [Test]
    public void Handle_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        CheckEmailCanBeRegisteredQuery request = null;

        // Act & Assert
        _ = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(request, CancellationToken.None));
    }

    /// <summary>
    /// Test for valid request, ensuring the correct query is sent and result is returned.
    /// </summary>
    [Test]
    public async Task Handle_ValidRequest_SendsCheckEmailCanBeRegisteredDbQuery()
    {
        // Arrange
        var email = "test@example.com";
        var canBeRegistered = true;

        // Setting up mock to return the result for the email check query
        _mediatorMock
            .Setup(m => m.Send(It.Is<CheckEmailCanBeRegisteredDbQuery>(q => q.Email == email), It.IsAny<CancellationToken>()))
            .ReturnsAsync(canBeRegistered);

        var request = new CheckEmailCanBeRegisteredQuery(email);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        // Verify that the Send method was called once with the expected query
        _mediatorMock.Verify(m => m.Send(It.Is<CheckEmailCanBeRegisteredDbQuery>(q => q.Email == email), It.IsAny<CancellationToken>()), Times.Once);

        // Assert that the result is not null and contains the expected CanBeRegistered value
        Assert.IsNotNull(result);
        Assert.That(result.CanBeRegistered, Is.EqualTo(canBeRegistered));
    }
}
