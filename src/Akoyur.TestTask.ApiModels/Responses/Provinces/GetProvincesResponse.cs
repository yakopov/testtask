using Akoyur.TestTask.ApiModels.Common;
using Akoyur.TestTask.Dto;

namespace Akoyur.TestTask.ApiModels.Responses.Provinces;

/// <summary>
/// Represents a response containing a collection of province data, including pagination details.
/// </summary>
public class GetProvincesResponse : CollectionResponse<ProvinceDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProvincesResponse"/> class.
    /// </summary>
    /// <param name="items">The collection of provinces in the response.</param>
    /// <param name="hasMore">Indicates whether there are more items available for pagination. Default is false.</param>
    /// <param name="cursor">An optional cursor for pagination, indicating the position for the next set of items. Default is null.</param>
    public GetProvincesResponse(IReadOnlyCollection<ProvinceDto> items, bool hasMore = false, int? cursor = null)
        : base(items, hasMore, cursor)
    {
    }
}
