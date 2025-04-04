using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api")]
[ApiController]
public class WorkspaceController : ControllerBase
{
    // Kas šitą darys, reikia pakeisti controllerį, kad gautų duomenis iš DB.
    [HttpGet("workspaces")]
    [Authorize]
    public IActionResult GetWorkspaces(int pageNumber, int pageSize)
    {
        // Generate dummy data
        var workspaces = GenerateDummyWorkspaces(pageNumber, pageSize);

        return Ok(new
        {
            pageNumber,
            pageSize,
            totalItems = 100, // You can dynamically calculate the total count if needed
            totalPages = (int)Math.Ceiling(100 / (double)pageSize), // Example total pages
            data = workspaces
        });
    }

    // Šitą ištrinti po to, kai bus padarytas duomenų gavimas iš DB
    private List<Workspace> GenerateDummyWorkspaces(int pageNumber, int pageSize)
    {
        var workspaces = new List<Workspace>();

        // Create dummy data (100 items for example)
        for (int i = 0; i < 100; i++)
        {
            workspaces.Add(new Workspace
            {
                Id = Guid.NewGuid(),
                Name = $"Workspace {i + 1}",
                CreatedAt = DateTime.UtcNow.AddDays(-i), // CreatedAt decreasing over time
                CreatedBy = $"User{(i % 10) + 1}" // CreatedBy with dummy user names
            });
        }

        // Paginate the data based on pageNumber and pageSize
        return workspaces
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}

// Šitą ištrinti po to, kai bus padarytas gavimas iš DB
public class Workspace
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}
