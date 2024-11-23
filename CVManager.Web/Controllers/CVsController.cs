using CVManager.Application.Features.Commands;
using CVManager.Application.Features.Queries;
using CVManager.Application.Features.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVsController : ApiControllerBase
    {
        public CVsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _mediator.Send(new GetCVsQuery());
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CVRequestModel cvRequest)
       {
            var data = await _mediator.Send(new AddCVCommand { CVRequest = cvRequest });
            return Ok(data);
        }
    }
}
