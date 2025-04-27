using Akoyur.TestTask.ApiModels.Responses.Countries;
using Akoyur.TestTask.ApiModels.Responses.Provinces;
using Akoyur.TestTask.Application.UseCases.Countries;
using Akoyur.TestTask.Application.UseCases.Countries.GetProvinces;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Akoyur.TestTask.WebAPI.Controllers;

/// <summary>
/// Controller for managing countries and their provinces.
/// </summary>
[ApiController]
[ApiVersion("1")]
[Route("api/v1/countries")]
public class CountriesController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Gets a list of all countries.
    /// </summary>
    /// <returns>List of countries</returns>
    [HttpGet]
    [Route("")]
    public Task<GetAllCountriesResponse> GetAllCountries()
        => mediator.Send(new GetAllCountriesQuery());

    /// <summary>
    /// Gets a list of provinces for a specific country.
    /// </summary>
    /// <param name="countryId">The ID of the country</param>
    /// <returns>List of provinces for the specified country</returns>
    [HttpGet]
    [Route("{countryId:int}/provinces")]
    public Task<GetProvincesResponse> GetCountryProvinces([FromRoute] int countryId)
        => mediator.Send(new GetProvincesQuery(countryId));
}
