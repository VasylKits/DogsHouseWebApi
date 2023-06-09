using Application.Features.Dogs.Commands.Create;
using Application.Features.Dogs.Commands.Delete;
using Application.Features.Dogs.Commands.Edit;
using Application.Features.Dogs.Queries.GetAll;
using Application.Features.Dogs.Queries.Ping;
using Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Common.Models.Result.Implementations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DogsController : ApiControllerBase
{
    [HttpGet("~/ping")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Ping()
        => (await Mediator.Send(new PingRequest())).ToActionResult();

    [HttpGet("~/dogs")]
    [ProducesResponseType(typeof(List<GetDogsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDogs([FromQuery] GetAllDogsQuery query)
        => (await Mediator.Send(query)).ToActionResult();

    [HttpPost("~/dog")]
    [ProducesResponseType(typeof(CreateDogResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDog([FromBody] CreateDogCommand command)
        => (await Mediator.Send(command)).ToActionResult();

    [HttpPut]
    [ProducesResponseType(typeof(CreateDogResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDog([FromBody] EditDogCommand command)
        => (await Mediator.Send(command)).ToActionResult();

    [HttpDelete("~/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteDog([FromRoute] int id)
        => (await Mediator.Send(new DeleteDogCommand(id))).ToActionResult();
}