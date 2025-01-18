using Gyneco.Application.Features.MedicalRecord.Commands.CreateMedicalRecord;
using Gyneco.Application.Features.MedicalRecord.Commands.UpdateMedicalRecord;
using Gyneco.Application.Features.MedicalRecord.Queries.GetMedicalRecordRequestDetail;
using Gyneco.Application.Features.MedicalRecord.Queries.GetMedicalRecordRequestList;
using Gyneco.Application.Models.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gyneco.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class MedicalRecordController : ControllerBase
{
    private readonly IMediator _mediator;
    public MedicalRecordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<SearchResult<MedicalRecordDetailDTO>>> GetMedicalRecordsListPage(
        [FromBody] MedicalRecordListRequestQuery request)
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
    public async Task<ActionResult<MedicalRecordDetailDTO>> GetMedicalRecordDetail(
        MedicalRecordDetailRequestQuery request)
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
    public async Task<ActionResult<Unit>> UpdateMedicalRecord([FromBody] UpdateMedicalRecordCommand request)
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
    public async Task<ActionResult<Unit>> CreateMedicalRecord([FromBody] CreateMedicalRecordCommand request)
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