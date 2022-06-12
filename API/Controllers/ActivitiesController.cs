using Application.ActivityM.Comman;
using Application.ActivityM.Query;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> Get()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Activity activity)
        {
            return Ok(await Mediator.Send(new Create.Command { Activity=activity}));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id,Activity activity)
        {
            activity.Id = id;
            return Ok(await Mediator.Send(new Update.Command { Activity=activity}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command { Id=id }));
        }
    }
}
