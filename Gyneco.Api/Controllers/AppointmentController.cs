using Gyneco.Application.Features.Appointment.Commands.CreateAppoitment;
using Gyneco.Application.Features.Appointment.Commands.UpdateAppointment;
using Gyneco.Application.Features.Appointment.Queries.GetAppointmentRequestDetail;
using Gyneco.Application.Features.Appointment.Queries.GetAppointmentRequestList;
using Gyneco.Application.Models.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gyneco.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AppointmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAppointment([FromBody] CreateAppointmentCommand request)
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

    [HttpPut]
    public async Task<ActionResult> UpdateAppointment([FromBody] UpdateAppointmentCommand request)
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
    public async Task<ActionResult<SearchResult<AppointmentDetailDTO>>> GetAllAppointments([FromBody] AppointmentListRequestQuery request)
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

    [HttpGet]
    public async Task<ActionResult<AppointmentDetailDTO>> GetAppointment(AppointmentDetailRequestQuery request)
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