using System.Collections.Generic;
using System.Linq;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using SponsorshipWorkflow.Dtos;
using SponsorshipWorkflow.Entities;

namespace SponsorshipWorkflow;

[Mapper]
public static partial class SponsorshipWorkflowApplicationMappers
{
	public static partial SponsorshipRequestDto Map(SponsorshipRequest source);
	public static partial SponsorshipRequestListDto MapToListDto(SponsorshipRequest source);
	public static partial SponsorshipRequest Map(CreateUpdateSponsorshipRequestDto source);
	public static partial void Map(CreateUpdateSponsorshipRequestDto source, SponsorshipRequest destination);

	public static List<SponsorshipRequestListDto> MapToListDtos(IReadOnlyList<SponsorshipRequest>? sources)
	{
		if (sources == null || sources.Count == 0)
		{
			return new List<SponsorshipRequestListDto>();
		}

		return sources.Select(MapToListDto).ToList();
	}

	public static partial SponsorshipTypeDto Map(SponsorshipType source);
	public static partial SponsorshipType Map(CreateUpdateSponsorshipTypeDto source);
	public static partial void Map(CreateUpdateSponsorshipTypeDto source, SponsorshipType destination);

	public static List<SponsorshipTypeDto> MapToTypeDtos(IReadOnlyList<SponsorshipType>? sources)
	{
		if (sources == null || sources.Count == 0)
		{
			return new List<SponsorshipTypeDto>();
		}

		return sources.Select(Map).ToList();
	}

	public static partial WorkflowHistoryDto Map(WorkflowHistory source);

	public static List<WorkflowHistoryDto> MapToWorkflowHistoryDtos(IEnumerable<WorkflowHistory>? sources)
	{
		if (sources == null)
		{
			return new List<WorkflowHistoryDto>();
		}

		return sources.OrderBy(x => x.PerformedAt).Select(Map).ToList();
	}
}
