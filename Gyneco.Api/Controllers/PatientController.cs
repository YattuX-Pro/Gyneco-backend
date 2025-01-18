using Gyneco.Application.Features.Patient.Commands.CreatePatient;
using Gyneco.Application.Features.Patient.Commands.UpdatePatient;
using Gyneco.Application.Features.Patient.Queries.GetPatientRequestDetail;
using Gyneco.Application.Features.Patient.Queries.GetPatientRequestList;
using Gyneco.Application.Models.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gyneco.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PatientController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<PatientDetailDTO>>> GetPatientList([FromBody] PatientRequestQuery request)
    {
        try
        {
            var patientResult = await _mediator.Send(request);
            return Ok(patientResult);
        }
        catch (Exception e)
        {
            throw;
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<PatientDetailDTO>> GetPatientDetail(PatientDetailRequestQuery request)
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
    public async Task<ActionResult> CreatePatient([FromBody] CreatePatientCommand request)
    {
        try
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(CreatePatient), new {id = result});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdatePatient([FromBody] UpdatePatientCommand request)
    {
        try
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(UpdatePatient), new { id = result });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}