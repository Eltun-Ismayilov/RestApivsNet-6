using Application.MediatR.PhotoM.Comman;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class PhotoController : BaseApiController
    {
        [HttpPost]

        public async Task<IActionResult> Create([FromForm] Create.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("id")]

        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
