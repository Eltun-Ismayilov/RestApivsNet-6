﻿using Application.MediatR.FollowUser.Comman;
using Application.MediatR.FollowUser.List;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : BaseApiController
    {
        [HttpPost("{username}")]
        public async Task<IActionResult> Follow(string username)
        {
            return HandleResult(await Mediator.Send(new FollowToggle.Command { TargetUsername = username }));
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetFollowings(string username,string predicate)
        {
            return HandleResult(await Mediator.Send(new List.Query { Username = username , Predicate=predicate}));
        }
    }
}
