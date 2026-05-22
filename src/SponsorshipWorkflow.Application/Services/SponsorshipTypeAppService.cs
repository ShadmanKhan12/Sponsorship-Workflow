using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using SponsorshipWorkflow.Dtos;
using SponsorshipWorkflow.Permissions;

namespace SponsorshipWorkflow.Services;
using Volo.Abp.Authorization;
using Microsoft.AspNetCore.Authorization;

[Authorize(SponsorshipWorkflowPermissions.SponsorshipTypes.Manage)]
public class SponsorshipTypeAppService : SponsorshipWorkflowAppService
{
	private readonly IRepository<Entities.SponsorshipType, Guid> _repository;

	public SponsorshipTypeAppService(IRepository<Entities.SponsorshipType, Guid> repository)
	{
		_repository = repository;
	}

	public virtual async Task<SponsorshipTypeDto> CreateAsync(CreateUpdateSponsorshipTypeDto input)
	{
		var entity = ObjectMapper.Map<CreateUpdateSponsorshipTypeDto, Entities.SponsorshipType>(input);
		var inserted = await _repository.InsertAsync(entity);
		return ObjectMapper.Map<Entities.SponsorshipType, SponsorshipTypeDto>(inserted);
	}

	public virtual async Task<SponsorshipTypeDto> UpdateAsync(Guid id, CreateUpdateSponsorshipTypeDto input)
	{
		var entity = await _repository.GetAsync(id);
		ObjectMapper.Map(input, entity);
		await _repository.UpdateAsync(entity);
		return ObjectMapper.Map<Entities.SponsorshipType, SponsorshipTypeDto>(entity);
	}

	public virtual async Task DeleteAsync(Guid id)
	{
		await _repository.DeleteAsync(id);
	}

	public virtual async Task<List<SponsorshipTypeDto>> GetListAsync()
	{
		var list = await _repository.GetListAsync();
		return ObjectMapper.Map<List<Entities.SponsorshipType>, List<SponsorshipTypeDto>>(list);
	}
}
