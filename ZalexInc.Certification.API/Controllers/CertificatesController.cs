using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZalexInc.Certification.Application.Commands;
using ZalexInc.Certification.Application.Queries;
using ZalexInc.Certification.Domain.Request;


namespace ZalexInc.Certification.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificatesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CertificatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCertificate(CreateCertificateCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCertificate(Guid id, UpdateCertificateCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch between route and request body");

            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok();

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificate(Guid id)
        {
            var command = new DeleteCertificateCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok();

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCertificateById(Guid id)
        {
            var query = new GetCertificateByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result.Success)
                return Ok(result.Data);

            return NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCertificates(int pageIndex = 1, int pageSize = 10, string sortBy = "Id", CertificateFilter filter = null)
        {
            var query = new GetPaginatedCertificatesQuery(pageIndex, pageSize, sortBy, filter);
            var result = await _mediator.Send(query);
            if (result.Success)
                return Ok(result.Data);
            return NotFound(result.Message);
        }
    }
}

