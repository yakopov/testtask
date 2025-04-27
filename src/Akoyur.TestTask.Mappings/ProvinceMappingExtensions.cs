using Akoyur.TestTask.Dto;
using Akoyur.TestTask.Entities;

namespace Akoyur.TestTask.Mappings;

/// <summary>
/// Extension methods for mapping Province entities to ProvinceDto objects.
/// </summary>
public static class ProvinceMappingExtensions
{
    /// <summary>
    /// Converts a Province entity to a ProvinceDto.
    /// </summary>
    /// <param name="province">The province entity to map.</param>
    /// <returns>A ProvinceDto representation of the province.</returns>
    public static ProvinceDto ToDto(this Province province)
    {
        return new ProvinceDto
        {
            Id = province.Id,
            Name = province.Name,
            CountryId = province.CountryId,
        };
    }
}
