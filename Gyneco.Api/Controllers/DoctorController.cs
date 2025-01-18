using Gyneco.Application.Features.Doctor.Commands.CreateDoctor;
using Gyneco.Application.Features.Doctor.Commands.UpdateDoctor;
using Gyneco.Application.Features.Doctor.Queries.GetDoctorRequestDetail;
using Gyneco.Application.Features.Doctor.Queries.GetDoctorRequestList;
using Gyneco.Application.Models.Search;
using Gyneco.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gyneco.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DoctorController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DoctorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateDoctor([FromBody]CreateDoctorCommand request)
    {
        try
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(CreateDoctor), new {id = result});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateDoctor([FromBody] UpdateDoctorCommand request)
    {
        try
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(UpdateDoctor), new {id = result});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public async Task<ActionResult<DoctorDetailDTO>> GetDoctorDetail(DoctorDetailRequestQuery request)
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
    public async Task<ActionResult<SearchResult<DoctorDetailDTO>>> GetDoctorListPage([FromBody]DoctorListRequestQuery listRequest)
    {
        try
        {
            var result = await _mediator.Send(listRequest);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}