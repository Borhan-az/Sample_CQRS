using CQRSWebAPI.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("/{Id}")]
        public async Task<IActionResult> GetUserbyName(int Id, CancellationToken cancellationToken )
        {
            var res = await mediator.Send(new GetUserById.Query(Id), cancellationToken);
            
            return res==null ? NotFound() : Ok(res);
        }
    }
}