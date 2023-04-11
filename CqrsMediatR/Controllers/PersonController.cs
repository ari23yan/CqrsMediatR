using Application.PersonFeatures.Command.Add.CreatePersonCommand;
using Application.PersonFeatures.Command.Delete.DeletePersonCommand;
using Application.PersonFeatures.Command.Edit.UpdatePersonCommand;
using Application.PersonFeatures.Queries.FindPersonById;
using Application.PersonFeatures.Queries.GetPersonsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsMediatR.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
        //private readonly IMediator mediator;
        //public PersonController(IMediator mediator)
        //{
        //    this.Mediator = mediator;
        //}
        [HttpPost]
        public async Task<IActionResult> Create(AddPersonCommandModel command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllPersonsListQueryModel()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetPersonByIdQueryModel { Id = id }));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeletePersonCommandModel { Id = id }));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, EditPersonCommandModel command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
