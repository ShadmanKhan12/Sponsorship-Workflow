using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Domain.Repositories;
using SponsorshipWorkflow.Dtos;
using SponsorshipWorkflow.Permissions;

namespace SponsorshipWorkflow.Services;

public class SponsorshipTypeAppService : SponsorshipWorkflowAppService
{
	private readonly IRepository<Entities.SponsorshipType, Guid> _repository;

	public SponsorshipTypeAppService(IRepository<Entities.SponsorshipType, Guid> repository)
	{
		_repository = repository;
	}

	[Authorize]
	public virtual async Task<List<SponsorshipTypeDto>> GetListAsync()
	{
		var queryable = await _repository.GetQueryableAsync();
		if (!await AuthorizationService.IsGrantedAsync(SponsorshipWorkflowPermissions.SponsorshipTypes.Manage))
		{
			queryable = queryable.Where(x => x.IsActive);
		}

		var list = await AsyncExecuter.ToListAsync(queryable.OrderBy(x => x.Name));
		return SponsorshipWorkflowApplicationMappers.MapToTypeDtos(list);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipTypes.Manage)]
	public virtual async Task<SponsorshipTypeDto> CreateAsync(CreateUpdateSponsorshipTypeDto input)
	{
		var entity = SponsorshipWorkflowApplicationMappers.Map(input);
		var inserted = await _repository.InsertAsync(entity);
		return SponsorshipWorkflowApplicationMappers.Map(inserted);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipTypes.Manage)]
	public virtual async Task<SponsorshipTypeDto> UpdateAsync(Guid id, CreateUpdateSponsorshipTypeDto input)
	{
		var entity = await _repository.GetAsync(id);
		SponsorshipWorkflowApplicationMappers.Map(input, entity);
		await _repository.UpdateAsync(entity);
		return SponsorshipWorkflowApplicationMappers.Map(entity);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipTypes.Manage)]
	public virtual async Task DeleteAsync(Guid id)
	{
		await _repository.DeleteAsync(id);
	}
}
