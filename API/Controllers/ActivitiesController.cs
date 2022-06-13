using Application.ActivityM.Comman;
using Application.ActivityM.Query;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
        //[Authorize]//Icaze vermir gerek login olmalisan
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Activity activity)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Activity=activity}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id,Activity activity)
        {
            activity.Id = id;
            return HandleResult(await Mediator.Send(new Update.Command { Activity=activity}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id=id }));
        }
    }
}
