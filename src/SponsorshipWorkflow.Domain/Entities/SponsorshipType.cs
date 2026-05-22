using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using SponsorshipWorkflow;

namespace SponsorshipWorkflow.Entities;

public class SponsorshipType : FullAuditedAggregateRoot<Guid>
{
	[Required]
	[StringLength(100)]
	public string Name { get; set; }

	[StringLength(1000)]
	public string? Description { get; set; }

	public bool IsActive { get; set; }
}
