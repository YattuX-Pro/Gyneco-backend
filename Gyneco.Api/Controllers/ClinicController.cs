using Gyneco.Application.Features.Clinic.Commands.CreateClinic;
using Gyneco.Application.Features.Clinic.Commands.UpdateClinic;
using Gyneco.Application.Features.Clinic.Queries.GerClinicRequestDetail;
using Gyneco.Application.Features.Clinic.Queries.GetClinicRequestList;
using Gyneco.Application.Models.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gyneco.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ClinicController : ControllerBase
{
    private readonly IMediator _mediator;
    public ClinicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<SearchResult<ClinicDetailDTO>>> GetClinicListPage([FromBody] ClinicListRequestQuery request)
    {
        try
        {
            var result  = await _mediator.Send(request);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public async Task<ActionResult<ClinicDetailDTO>> GetClinicDetail(ClinicDetailRequestQuery request)
    {
        try
        {
            var result  = await _mediator.Send(request);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult<Unit>> UpdateClinic([FromBody] UpdateClinicCommand request)
    {
        try
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult<Unit>> CreateClinic([FromBody]CreateClinicCommand request)
    {
        try
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}