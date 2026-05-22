using System.Collections.Generic;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using SponsorshipWorkflow.Dtos;
using SponsorshipWorkflow.Entities;

namespace SponsorshipWorkflow;

[Mapper]
public static partial class SponsorshipWorkflowApplicationMappers
{
	// SponsorshipRequest mappings
	public static partial SponsorshipRequestDto Map(SponsorshipRequest source);
	public static partial SponsorshipRequestListDto MapToListDto(SponsorshipRequest source);
	public static partial SponsorshipRequest Map(CreateUpdateSponsorshipRequestDto source);
	public static partial List<SponsorshipRequestListDto> Map(List<SponsorshipRequest> sources);

	// SponsorshipType mappings
	public static partial SponsorshipTypeDto Map(SponsorshipType source);
	public static partial SponsorshipType Map(CreateUpdateSponsorshipTypeDto source);
	public static partial List<SponsorshipTypeDto> Map(List<SponsorshipType> sources);

	// WorkflowHistory mappings
	public static partial WorkflowHistoryDto Map(WorkflowHistory source);
	public static partial List<WorkflowHistoryDto> MapHistoryList(List<WorkflowHistory> sources);
}
