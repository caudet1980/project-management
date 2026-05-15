using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services.Interface;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProjectsController : BaseApiController
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService, IErrorLoggingService errorLoggingService)
        : base(errorLoggingService)
        => _projectService = projectService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var projects = await _projectService.GetAllAsync(UserId);
            return Ok(projects);
        }
        catch (Exception ex)
        {
            return await HandleException(ex);
        }
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var project = await _projectService.GetByIdAsync(id, UserId);
            if (project == null)
                return NotFound(new { message = "Project not found." });
            return Ok(project);
        }
        catch (Exception ex)
        {
            return await HandleException(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectDto createProjectDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var project = await _projectService.CreateAsync(createProjectDto, UserId);
            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }
        catch (Exception ex)
        {
            return await HandleException(ex);
        }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var deleted = await _projectService.DeleteAsync(id, UserId);
            if (!deleted)
                return NotFound(new { message = "Project not found." });
            return NoContent();
        }
        catch (Exception ex)
        {
            return await HandleException(ex);
        }
    }
}