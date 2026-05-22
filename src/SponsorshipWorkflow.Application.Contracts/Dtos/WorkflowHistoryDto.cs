using System;
using SponsorshipWorkflow;

namespace SponsorshipWorkflow.Dtos;

public class WorkflowHistoryDto
{
	public Guid Id { get; set; }
	public Guid SponsorshipRequestId { get; set; }
	public WorkflowAction Action { get; set; }
	public SponsorshipStatus PreviousStatus { get; set; }
	public SponsorshipStatus NewStatus { get; set; }
	public string? Remarks { get; set; }
	public Guid? PerformedByUserId { get; set; }
	public string? PerformedByUserName { get; set; }
	public DateTime PerformedAt { get; set; }
}
