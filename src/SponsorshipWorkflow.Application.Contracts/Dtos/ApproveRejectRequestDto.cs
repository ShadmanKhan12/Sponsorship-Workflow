using System.ComponentModel.DataAnnotations;

namespace SponsorshipWorkflow.Dtos;

public class ApproveRejectRequestDto
{
	[StringLength(1000)]
	public string? Remarks { get; set; }
}
