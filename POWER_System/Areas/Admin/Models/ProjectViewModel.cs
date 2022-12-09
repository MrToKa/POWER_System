namespace POWER_System.Areas.Admin.Models;

public class ProjectViewModel
{
    public Guid Id { get; set; }

    public string Number { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Contractor { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool Assigned { get; set; } = false!;
}