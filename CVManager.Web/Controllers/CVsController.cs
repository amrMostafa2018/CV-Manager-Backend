using CVManager.Application.Features.Commands;
using CVManager.Application.Features.Queries;
using CVManager.Application.Features.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CVManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVsController : ApiControllerBase
    {
        public CVsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("getCVs")]
        public async Task<IActionResult> GetCVs()
        {
            var data = await _mediator.Send(new GetCVsQuery());
            return Ok(data);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCV(CVRequestModel cvRequest)
       {
            var data = await _mediator.Send(new AddCVCommand { CVRequest = cvRequest });
            return Ok(data);
        }
    }
}
