using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using SponsorshipWorkflow;

namespace SponsorshipWorkflow.Entities;

public class WorkflowHistory : FullAuditedAggregateRoot<Guid>
{
	[Required]
	public Guid SponsorshipRequestId { get; set; }

	[Required]
	public WorkflowAction Action { get; set; }

	public SponsorshipStatus PreviousStatus { get; set; }

	public SponsorshipStatus NewStatus { get; set; }

	[StringLength(1000)]
	public string? Remarks { get; set; }

	public Guid? PerformedByUserId { get; set; }

	[StringLength(200)]
	public string? PerformedByUserName { get; set; }

	public DateTime PerformedAt { get; set; }

	// Navigation
	public virtual SponsorshipRequest? SponsorshipRequest { get; set; }
}
