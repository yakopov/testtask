using Akoyur.TestTask.Application.UseCases.Users;
using Akoyur.TestTask.Infrastructure.Database.Province;
using Akoyur.TestTask.Infrastructure.Database.Users;
using FluentValidation;
using MediatR;
using Moq;

namespace Akoyur.TestTask.Application.UnitTests.UseCases.Users;

/// <summary>
/// Unit tests for the CreateUserCommandHandler class.
/// </summary>
[TestFixture]
public class CreateUserCommandHandlerTests
{
    private Mock<IMediator> _mediatorMock;
    private CreateUserCommandHandler _handler;

    /// <summary>
    /// Set up test environment, mocking IMediator and initializing handler.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
        _handler = new CreateUserCommandHandler(_mediatorMock.Object);
    }

    /// <summary>
    /// Test for handling null request, expecting ArgumentNullException.
    /// </summary>
    [Test]
    public void Handle_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        CreateUserCommand request = null;

        // Act & Assert
        _ = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(request, CancellationToken.None));
    }

    /// <summary>
    /// Test for handling case when the province does not exist, expecting ValidationException.
    /// </summary>
    [Test]
    public async Task Handle_ProvinceDoesNotExist_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateUserCommand("test@example.com", "password123", 1);

        _mediatorMock
            .Setup(m => m.Send(It.Is<CheckProvinceIdExistsDbQuery>(q => q.ProvinceId == 1), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);  // Province does not exist

        // Act & Assert
        var ex = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Errors?.SingleOrDefault()?.ErrorMessage, Is.EqualTo("Invalid province identifier."));
    }

    /// <summary>
    /// Test for handling case when the email is already taken, expecting ValidationException.
    /// </summary>
    [Test]
    public async Task Handle_EmailAlreadyTaken_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateUserCommand("test@example.com", "password123", 1);

        _mediatorMock
            .Setup(m => m.Send(It.Is<CheckProvinceIdExistsDbQuery>(q => q.ProvinceId == 1), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);  // Province exists
        _mediatorMock
            .Setup(m => m.Send(It.Is<CheckEmailCanBeRegisteredDbQuery>(q => q.Email == "test@example.com"), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);  // Email is already taken

        // Act & Assert
        var ex = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Errors?.SingleOrDefault()?.ErrorMessage, Is.EqualTo("Email is already taken."));
    }

    /// <summary>
    /// Test for handling valid request, ensuring user creation and response.
    /// </summary>
    [Test]
    public async Task Handle_ValidRequest_CreatesUser()
    {
        // Arrange
        var command = new CreateUserCommand("test@example.com", "password123", 1);
        var userId = 123;

        _mediatorMock
            .Setup(m => m.Send(It.Is<CheckProvinceIdExistsDbQuery>(q => q.ProvinceId == 1), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);  // Province exists
        _mediatorMock
            .Setup(m => m.Send(It.Is<CheckEmailCanBeRegisteredDbQuery>(q => q.Email == "test@example.com"), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);  // Email is available
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateUserDbCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userId);  // User creation

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        // Verify that the Send method was called once for user creation
        _mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserDbCommand>(), It.IsAny<CancellationToken>()), Times.Once);

        // Assert that the result is not null and contains the expected UserId
        Assert.IsNotNull(result);
        Assert.That(result.UserId, Is.EqualTo(userId));
    }
}
