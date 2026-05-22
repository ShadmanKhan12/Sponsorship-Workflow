using System;
using SponsorshipWorkflow;

namespace SponsorshipWorkflow.Dtos;

public class SponsorshipRequestDto
{
	public Guid Id { get; set; }
	public string RequestTitle { get; set; }
	public string RequestorName { get; set; }
	public string Department { get; set; }
	public Guid SponsorshipTypeId { get; set; }
	public string? EventName { get; set; }
	public DateTime? EventDate { get; set; }
	public decimal RequestedAmount { get; set; }
	public string? Purpose { get; set; }
	public string? ExpectedBusinessBenefit { get; set; }
	public string? Remarks { get; set; }
	public string? ManagerRemarks { get; set; }
	public string? FinanceRemarks { get; set; }
	public SponsorshipStatus Status { get; set; }
	public DateTime? SubmittedAt { get; set; }
	public DateTime? ApprovedAt { get; set; }
	public DateTime? CancelledAt { get; set; }
}
