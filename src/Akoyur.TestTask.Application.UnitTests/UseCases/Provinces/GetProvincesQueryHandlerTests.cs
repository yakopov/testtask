using Akoyur.TestTask.Application.UseCases.Countries.GetProvinces;
using Akoyur.TestTask.Dto;
using Akoyur.TestTask.Enumerations;
using Akoyur.TestTask.Infrastructure.Database.Province;
using MediatR;
using Moq;

namespace Akoyur.TestTask.Application.UnitTests.UseCases.Provinces;

/// <summary>
/// Unit tests for the GetProvincesQueryHandler class.
/// </summary>
[TestFixture]
public class GetProvincesQueryHandlerTests
{
    private Mock<IMediator> _mediatorMock;
    private GetProvincesQueryHandler _handler;

    /// <summary>
    /// Set up test environment, mocking IMediator and initializing handler.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
        _handler = new GetProvincesQueryHandler(_mediatorMock.Object);
    }

    /// <summary>
    /// Test for handling null request, expecting ArgumentNullException.
    /// </summary>
    [Test]
    public void Handle_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        GetProvincesQuery request = null;

        // Act & Assert
        _ = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(request, CancellationToken.None));
    }

    /// <summary>
    /// Test for valid request, ensuring the correct query is sent and result is returned.
    /// </summary>
    [Test]
    public async Task Handle_ValidRequest_SendsGetProvincesDbQuery()
    {
        // Arrange
        var expectedProvinces = new List<ProvinceDto>
        {
            new ProvinceDto { Id = 1, Name = "California" },
            new ProvinceDto { Id = 2, Name = "Ontario" }
        };

        // Setting up mock to return expected provinces when queried with CountryId and SortOrder.NameAsc
        _mediatorMock
            .Setup(m => m.Send(It.Is<GetProvincesDbQuery>(q => q.CountryId == 1 && q.SortOrder == SortOrder.NameAsc), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProvinces);

        var request = new GetProvincesQuery(1);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        // Verify that the Send method was called once with the expected query
        _mediatorMock.Verify(m => m.Send(It.Is<GetProvincesDbQuery>(q => q.CountryId == 1 && q.SortOrder == SortOrder.NameAsc), It.IsAny<CancellationToken>()), Times.Once);

        // Assert that the result is not null and contains the expected provinces
        Assert.IsNotNull(result);
        Assert.That(result.Items, Is.EqualTo(expectedProvinces));
    }
}
