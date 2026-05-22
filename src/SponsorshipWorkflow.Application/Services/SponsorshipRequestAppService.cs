using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using SponsorshipWorkflow.Dtos;
using SponsorshipWorkflow.Permissions;
using SponsorshipWorkflow;
using Volo.Abp.Authorization;

namespace SponsorshipWorkflow.Services;

public class SponsorshipRequestAppService : SponsorshipWorkflowAppService
{
	private readonly IRepository<Entities.SponsorshipRequest, Guid> _requestRepository;
	private readonly IRepository<Entities.WorkflowHistory, Guid> _historyRepository;
	private readonly IRepository<Entities.SponsorshipType, Guid> _typeRepository;
	private readonly ICurrentUser _currentUser;

	public SponsorshipRequestAppService(
		IRepository<Entities.SponsorshipRequest, Guid> requestRepository,
		IRepository<Entities.WorkflowHistory, Guid> historyRepository,
		IRepository<Entities.SponsorshipType, Guid> typeRepository,
		ICurrentUser currentUser)
	{
		_requestRepository = requestRepository;
		_historyRepository = historyRepository;
		_typeRepository = typeRepository;
		_currentUser = currentUser;
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.Create)]
	public virtual async Task<SponsorshipRequestDto> CreateAsync(CreateUpdateSponsorshipRequestDto input)
	{
		var entity = ObjectMapper.Map<CreateUpdateSponsorshipRequestDto, Entities.SponsorshipRequest>(input);
		entity.Status = SponsorshipStatus.Draft;
		var inserted = await _requestRepository.InsertAsync(entity);

		await _historyRepository.InsertAsync(new Entities.WorkflowHistory
		{
			SponsorshipRequestId = inserted.Id,
			Action = WorkflowAction.Created,
			PreviousStatus = SponsorshipStatus.Draft,
			NewStatus = SponsorshipStatus.Draft,
			PerformedAt = DateTime.UtcNow,
			PerformedByUserId = _currentUser.Id,
			PerformedByUserName = _currentUser.UserName
		});

		return ObjectMapper.Map<Entities.SponsorshipRequest, SponsorshipRequestDto>(inserted);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.Edit)]
	public virtual async Task<SponsorshipRequestDto> UpdateDraftAsync(Guid id, CreateUpdateSponsorshipRequestDto input)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.Draft)
			throw new BusinessException("InvalidStatus", "Only drafts can be updated");

		ObjectMapper.Map(input, entity);
		await _requestRepository.UpdateAsync(entity);

		await _historyRepository.InsertAsync(new Entities.WorkflowHistory
		{
			SponsorshipRequestId = entity.Id,
			Action = WorkflowAction.Updated,
			PreviousStatus = SponsorshipStatus.Draft,
			NewStatus = SponsorshipStatus.Draft,
			PerformedAt = DateTime.UtcNow,
			PerformedByUserId = _currentUser.Id,
			PerformedByUserName = _currentUser.UserName
		});

		return ObjectMapper.Map<Entities.SponsorshipRequest, SponsorshipRequestDto>(entity);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.Submit)]
	public virtual async Task SubmitAsync(Guid id)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.Draft)
			throw new BusinessException("InvalidStatus", "Only drafts can be submitted");

		entity.Status = SponsorshipStatus.PendingManagerApproval;
		entity.SubmittedAt = DateTime.UtcNow;
		await _requestRepository.UpdateAsync(entity);

		await _historyRepository.InsertAsync(new Entities.WorkflowHistory
		{
			SponsorshipRequestId = entity.Id,
			Action = WorkflowAction.Submitted,
			PreviousStatus = SponsorshipStatus.Draft,
			NewStatus = SponsorshipStatus.PendingManagerApproval,
			PerformedAt = DateTime.UtcNow,
			PerformedByUserId = _currentUser.Id,
			PerformedByUserName = _currentUser.UserName
		});
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.Cancel)]
	public virtual async Task CancelAsync(Guid id)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status == SponsorshipStatus.Approved || entity.Status == SponsorshipStatus.Rejected || entity.Status == SponsorshipStatus.Cancelled)
			throw new BusinessException("InvalidStatus", "Cannot cancel a completed request");

		if (entity.Status == SponsorshipStatus.PendingFinanceReview && !_currentUser.IsInRole("FinanceAdmin"))
			throw new BusinessException("Forbidden", "Only finance admin cannot be cancelled at this stage");

		var prev = entity.Status;
		entity.Status = SponsorshipStatus.Cancelled;
		entity.CancelledAt = DateTime.UtcNow;
		await _requestRepository.UpdateAsync(entity);

		await _historyRepository.InsertAsync(new Entities.WorkflowHistory
		{
			SponsorshipRequestId = entity.Id,
			Action = WorkflowAction.Cancelled,
			PreviousStatus = prev,
			NewStatus = SponsorshipStatus.Cancelled,
			PerformedAt = DateTime.UtcNow,
			PerformedByUserId = _currentUser.Id,
			PerformedByUserName = _currentUser.UserName,
			Remarks = "Cancelled by user"
		});
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerApprove)]
	public virtual async Task ApproveByManagerAsync(Guid id, ApproveRejectRequestDto input)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.PendingManagerApproval)
			throw new BusinessException("InvalidStatus", "Only PendingManagerApproval can be approved by manager");

		var prev = entity.Status;
		entity.Status = SponsorshipStatus.PendingFinanceReview;
		entity.ManagerRemarks = input.Remarks;
		await _requestRepository.UpdateAsync(entity);

		await _historyRepository.InsertAsync(new Entities.WorkflowHistory
		{
			SponsorshipRequestId = entity.Id,
			Action = WorkflowAction.ManagerApproved,
			PreviousStatus = prev,
			NewStatus = entity.Status,
			PerformedAt = DateTime.UtcNow,
			PerformedByUserId = _currentUser.Id,
			PerformedByUserName = _currentUser.UserName,
			Remarks = input.Remarks
		});
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerReject)]
	public virtual async Task RejectByManagerAsync(Guid id, ApproveRejectRequestDto input)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.PendingManagerApproval)
			throw new BusinessException("InvalidStatus", "Only PendingManagerApproval can be rejected by manager");

		var prev = entity.Status;
		entity.Status = SponsorshipStatus.Rejected;
		entity.ManagerRemarks = input.Remarks;
		await _requestRepository.UpdateAsync(entity);

		await _historyRepository.InsertAsync(new Entities.WorkflowHistory
		{
			SponsorshipRequestId = entity.Id,
			Action = WorkflowAction.ManagerRejected,
			PreviousStatus = prev,
			NewStatus = entity.Status,
			PerformedAt = DateTime.UtcNow,
			PerformedByUserId = _currentUser.Id,
			PerformedByUserName = _currentUser.UserName,
			Remarks = input.Remarks
		});
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceApprove)]
	public virtual async Task ApproveByFinanceAsync(Guid id, ApproveRejectRequestDto input)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.PendingFinanceReview)
			throw new BusinessException("InvalidStatus", "Only PendingFinanceReview can be approved by finance");

		var prev = entity.Status;
		entity.Status = SponsorshipStatus.Approved;
		entity.FinanceRemarks = input.Remarks;
		entity.ApprovedAt = DateTime.UtcNow;
		await _requestRepository.UpdateAsync(entity);

		await _historyRepository.InsertAsync(new Entities.WorkflowHistory
		{
			SponsorshipRequestId = entity.Id,
			Action = WorkflowAction.FinanceApproved,
			PreviousStatus = prev,
			NewStatus = entity.Status,
			PerformedAt = DateTime.UtcNow,
			PerformedByUserId = _currentUser.Id,
			PerformedByUserName = _currentUser.UserName,
			Remarks = input.Remarks
		});
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceReject)]
	public virtual async Task RejectByFinanceAsync(Guid id, ApproveRejectRequestDto input)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.PendingFinanceReview)
			throw new BusinessException("InvalidStatus", "Only PendingFinanceReview can be rejected by finance");

		var prev = entity.Status;
		entity.Status = SponsorshipStatus.Rejected;
		entity.FinanceRemarks = input.Remarks;
		await _requestRepository.UpdateAsync(entity);

		await _historyRepository.InsertAsync(new Entities.WorkflowHistory
		{
			SponsorshipRequestId = entity.Id,
			Action = WorkflowAction.FinanceRejected,
			PreviousStatus = prev,
			NewStatus = entity.Status,
			PerformedAt = DateTime.UtcNow,
			PerformedByUserId = _currentUser.Id,
			PerformedByUserName = _currentUser.UserName,
			Remarks = input.Remarks
		});
	}

	public virtual async Task<List<SponsorshipRequestListDto>> GetMyRequestsAsync()
	{
		var userName = _currentUser.UserName;
		var list = await _requestRepository.GetListAsync();
		return ObjectMapper.Map<List<Entities.SponsorshipRequest>, List<SponsorshipRequestListDto>>(list.Where(x => x.CreatorId == _currentUser.Id).ToList());
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerApprove)]
	public virtual async Task<List<SponsorshipRequestListDto>> GetPendingManagerApprovalsAsync()
	{
		var list = await _requestRepository.GetListAsync();
		list = list.Where(x => x.Status == SponsorshipStatus.PendingManagerApproval).ToList();
		return ObjectMapper.Map<List<Entities.SponsorshipRequest>, List<SponsorshipRequestListDto>>(list);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceApprove)]
	public virtual async Task<List<SponsorshipRequestListDto>> GetPendingFinanceReviewsAsync()
	{
		var list = await _requestRepository.GetListAsync();
		list = list.Where(x => x.Status == SponsorshipStatus.PendingFinanceReview).ToList();
		return ObjectMapper.Map<List<Entities.SponsorshipRequest>, List<SponsorshipRequestListDto>>(list);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.ViewAll)]
	public virtual async Task<PagedResultDto<SponsorshipRequestListDto>> GetAllRequestsAsync(int skip = 0, int take = 20)
	{
		var total = await _requestRepository.GetCountAsync();
		var list = await _requestRepository.GetListAsync();
		list = list.Skip(skip).Take(take).ToList();
		return new PagedResultDto<SponsorshipRequestListDto>(total, ObjectMapper.Map<List<Entities.SponsorshipRequest>, List<SponsorshipRequestListDto>>(list));
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.ViewWorkflowHistory)]
	public virtual async Task<List<WorkflowHistoryDto>> GetWorkflowHistoryAsync(Guid sponsorshipRequestId)
	{
		var list = await _historyRepository.GetListAsync(x => x.SponsorshipRequestId == sponsorshipRequestId);
		return ObjectMapper.Map<List<Entities.WorkflowHistory>, List<WorkflowHistoryDto>>(list.OrderBy(x => x.PerformedAt).ToList());
	}
}
