using System;
using SponsorshipWorkflow;

namespace SponsorshipWorkflow.Dtos;

public class SponsorshipRequestListDto
{
	public Guid Id { get; set; }
	public string RequestTitle { get; set; }
	public string RequestorName { get; set; }
	public SponsorshipStatus Status { get; set; }
	public decimal RequestedAmount { get; set; }
	public DateTime? SubmittedAt { get; set; }
}
