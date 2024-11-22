using CVManager.Application.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CVManager.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private IMediator mediator;

        protected ApiControllerBase(IMediator mediator)
        {
            this.mediator = mediator;
        }
        protected IMediator _mediator => this.mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected new IActionResult Response(ResponseVM response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK: return Ok(response);
                case HttpStatusCode.BadRequest: return BadRequest(response);
                case HttpStatusCode.NotFound: return BadRequest(response);
                case HttpStatusCode.UnprocessableEntity: return BadRequest(response);
                default: return Ok(response);
            }
        }
    }
}
