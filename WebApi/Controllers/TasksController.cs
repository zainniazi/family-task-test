using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _taskService.CreateTaskCommandHandler(command);
            return Created($"/api/task/{result.Payload.Id}", result);
        }

        [Route("{id}/toggle-complete")]
        [HttpGet]
        [ProducesResponseType(typeof(ToggleTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Toggle(Guid? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _taskService.ToggleTaskCommandHandler((Guid)id);
                return Ok(result);
            }
            catch (NotFoundException<Guid>)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllTasksQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAllTasksQueryHandler();
            return Ok(result);
        }

        [Route("get-all-by-member/{memberId}")]
        [HttpGet]
        [ProducesResponseType(typeof(GetAllTasksByMemberQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByMember(Guid? memberId)
        {
            if (memberId == null)
            {
                return BadRequest();
            }
            var result = await _taskService.GetAllTasksByMemberQueryHandler((Guid)memberId);
            return Ok(result);
        }

        [HttpPost]
        [Route("assign-member")]
        [ProducesResponseType(typeof(AssignMemberCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> AssignMember(AssignMemberCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _taskService.AssignMemberCommandHandler(command);
                return Ok(result);
            }
            catch (NotFoundException<Guid>)
            {
                return Ok(new AssignMemberCommandResult { Succeed = false, Message = "Invalid task" });
            }
        }
    }
}
