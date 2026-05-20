using LibraryMS.Application.DTOs.ClientDto;
using LibraryMS.Application.Features.Client.Commands.RegisterClient;
using LibraryMS.Application.Features.Client.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;

namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int pageNumber, int pageSize)
    {
        var result = await _mediator.Send(new GetAllClientQuery(pageNumber, pageSize));
        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] ClientRequestDto dto)
    {
        var result = await _mediator.Send(dto.ToCommand());
        if (result.IsFailure)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status201Created, result);
    }
}