using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services.Interface;

namespace TaskManagerApi.Controllers;

[ApiController]
[Authorize]
[Route("api/projects/{projectId:Guid}/[controller]")]
public class TasksController : BaseApiController
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService, IErrorLoggingService errorLoggingService)
        : base(errorLoggingService)
        => _taskService = taskService;

    #region DELETE
    /// <summary>
    /// Deletes a specific task by its ID for the authenticated user.
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    /// <response code="204">Task deleted successfully.</response>
    /// <response code="401">Unauthorized - the user is not authenticated.</response>
    /// <response code="404">Not Found - no task with the specified ID exists or it does not belong to the authenticated user.</response>
    /// <response code="400">Bad Request - invalid request parameters.</response>
    /// <response code="500">Internal Server Error - an error occurred while processing the request</response>
    [HttpDelete]
    [Route("{taskId:Guid}")]
    public async Task<IActionResult> Delete(Guid taskId)
    {
        try
        {            
            var deleted = await _taskService.DeleteAsync(taskId, UserId);
            if (!deleted)
                return NotFound(new { message = "Task not found." });
            
            return NoContent();
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (AggregateException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return await HandleException(ex);
        }
    }
    #endregion

    #region GET
    /// <summary>
    /// Retrieves a specific task by its ID for the authenticated user.
    /// </summary>
    /// <param name="taskId">The ID of the task to retrieve.</param>
    /// <returns>The task with the specified ID if it exists and belongs to the authenticated user; otherwise, an appropriate error response.</returns>
    /// <response code="200">Task retrieved successfully.</response>
    /// <response code="401">Unauthorized - the user is not authenticated.</response>
    /// <response code="404">Not Found - no task with the specified ID exists or it does not belong to the authenticated user.</response>
    /// <response code="400">Bad Request - invalid request parameters.</response>
    /// <response code="500">Internal Server Error - an error occurred while processing the request</response>
    [HttpGet]
    [Route("{taskId:Guid}")]
    public async Task<IActionResult> GetById(Guid taskId)
    {
        try
        {
            var task = await _taskService.GetByIdAsync(taskId, UserId);
            if (task == null)
                return NotFound(new { message = "Task not found." });
            return Ok(task);
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return await HandleException(ex);
        }
    }
    #endregion

    #region POST
    /// <summary>
    /// Creates a new task for the authenticated user.
    /// </summary>
    /// <param name="projectId" />
    /// <param name="createTaskDto" />
    /// <returns>The created task.</returns>
    /// <response code="201">Task created successfully.</response>
    /// <response code="401">Unauthorized - the user is not authenticated.</response>
    /// <response code="400">Bad Request - invalid request parameters.</response>
    /// <response code="500">Internal Server Error - an error occurred while processing the request</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] Guid projectId, [FromBody] CreateTaskDto createTaskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var task = await _taskService.CreateAsync(projectId, createTaskDto, UserId);
            return CreatedAtAction(nameof(GetById), new { projectId, taskId = task.Id }, task);
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (AggregateException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return await HandleException(ex);
        }
    }

    #endregion
}