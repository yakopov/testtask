using Akoyur.TestTask.Dto;
using Akoyur.TestTask.Entities;

namespace Akoyur.TestTask.Mappings;

/// <summary>
/// Extension methods for mapping Country entities to CountryDto objects.
/// </summary>
public static class CountryMappingExtensions
{
    /// <summary>
    /// Converts a Country entity to a CountryDto.
    /// </summary>
    /// <param name="country">The country entity to map.</param>
    /// <returns>A CountryDto representation of the country.</returns>
    public static CountryDto ToDto(this Country country)
    {
        return new CountryDto
        {
            Id = country.Id,
            Name = country.Name
        };
    }
}

