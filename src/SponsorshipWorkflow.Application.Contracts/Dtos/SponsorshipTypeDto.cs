using System;

namespace SponsorshipWorkflow.Dtos;

public class SponsorshipTypeDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public bool IsActive { get; set; }
}
