using CQRSWebAPI.Command;
using CQRSWebAPI.Model;
using CQRSWebAPI.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        //private readonly ILogger<UserController> logger;

        public UserController(IMediator mediator/*, ILogger<UserController> logger*/)
        {
            this.mediator = mediator;
            //this.logger = logger;
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserbyName(int Id, CancellationToken cancellationToken)
        {
            var res = await mediator.Send(new GetUserById.Query(Id), cancellationToken);

            return res == null ? NotFound() : Ok(res);
        }
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUser(AddUser.Command command) => Ok(await mediator.Send(command));
    }
}