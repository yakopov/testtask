using Akoyur.TestTask.Application.UseCases.Countries;
using Akoyur.TestTask.Dto;
using Akoyur.TestTask.Enumerations;
using Akoyur.TestTask.Infrastructure.Database.Countries;
using MediatR;
using Moq;

namespace Akoyur.TestTask.Application.UnitTests.UseCases.Countries;

/// <summary>
/// Unit tests for the GetAllCountriesQueryHandler class.
/// </summary>
[TestFixture]
public class GetAllCountriesQueryHandlerTests
{
    private Mock<IMediator> _mediatorMock;
    private GetAllCountriesQueryHandler _handler;

    /// <summary>
    /// Set up test environment, mocking IMediator and initializing handler.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
        _handler = new GetAllCountriesQueryHandler(_mediatorMock.Object);
    }

    /// <summary>
    /// Test for handling null request, expecting ArgumentNullException.
    /// </summary>
    [Test]
    public void Handle_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        GetAllCountriesQuery request = null;

        // Act & Assert
        _ = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(request, CancellationToken.None));
    }

    /// <summary>
    /// Test for valid request, ensuring the correct query is sent and result is returned.
    /// </summary>
    [Test]
    public async Task Handle_ValidRequest_SendsGetCountriesDbQuery()
    {
        // Arrange
        var expectedCountries = new List<CountryDto>
        {
            new CountryDto { Id = 1, Name = "USA" },
            new CountryDto { Id = 2, Name = "Canada" }
        };

        // Setting up mock to return expected countries when queried with SortOrder.NameAsc
        _mediatorMock
            .Setup(m => m.Send(It.Is<GetCountriesDbQuery>(q => q.SortOrder == SortOrder.NameAsc), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCountries);

        var request = new GetAllCountriesQuery();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        // Verify that the Send method was called once with the expected query
        _mediatorMock.Verify(m => m.Send(It.Is<GetCountriesDbQuery>(q => q.SortOrder == SortOrder.NameAsc), It.IsAny<CancellationToken>()), Times.Once);

        // Assert that the result is not null and contains the expected countries
        Assert.IsNotNull(result);
        Assert.That(result.Items, Is.EqualTo(expectedCountries));
    }
}
