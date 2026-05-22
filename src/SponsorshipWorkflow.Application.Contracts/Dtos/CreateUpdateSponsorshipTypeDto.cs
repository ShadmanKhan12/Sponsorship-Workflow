using System.ComponentModel.DataAnnotations;

namespace SponsorshipWorkflow.Dtos;

public class CreateUpdateSponsorshipTypeDto
{
	[Required]
	[StringLength(100)]
	public string Name { get; set; }

	[StringLength(1000)]
	public string? Description { get; set; }

	public bool IsActive { get; set; }
}
