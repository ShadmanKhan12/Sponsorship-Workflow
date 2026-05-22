using System;
using System.ComponentModel.DataAnnotations;

namespace SponsorshipWorkflow.Dtos;

public class CreateUpdateSponsorshipRequestDto
{
	[Required]
	[StringLength(200)]
	public string RequestTitle { get; set; }

	[Required]
	[StringLength(100)]
	public string RequestorName { get; set; }

	[Required]
	[StringLength(100)]
	public string Department { get; set; }

	[Required]
	public Guid SponsorshipTypeId { get; set; }

	[StringLength(200)]
	public string? EventName { get; set; }

	public DateTime? EventDate { get; set; }

	[Range(0, double.MaxValue)]
	public decimal RequestedAmount { get; set; }

	[StringLength(1000)]
	public string? Purpose { get; set; }

	[StringLength(1000)]
	public string? ExpectedBusinessBenefit { get; set; }

	[StringLength(1000)]
	public string? Remarks { get; set; }
}
