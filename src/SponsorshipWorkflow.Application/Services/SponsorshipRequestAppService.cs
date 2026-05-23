using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using SponsorshipWorkflow.Dtos;
using SponsorshipWorkflow.Permissions;

namespace SponsorshipWorkflow.Services;

public class SponsorshipRequestAppService : SponsorshipWorkflowAppService
{
	private readonly IRepository<Entities.SponsorshipRequest, Guid> _requestRepository;
	private readonly IRepository<Entities.WorkflowHistory, Guid> _historyRepository;
	private readonly ICurrentUser _currentUser;

	public SponsorshipRequestAppService(
		IRepository<Entities.SponsorshipRequest, Guid> requestRepository,
		IRepository<Entities.WorkflowHistory, Guid> historyRepository,
		ICurrentUser currentUser)
	{
		_requestRepository = requestRepository;
		_historyRepository = historyRepository;
		_currentUser = currentUser;
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.Default)]
	public virtual async Task<SponsorshipRequestDto> GetAsync(Guid id)
	{
		var entity = await _requestRepository.GetAsync(id);
		return SponsorshipWorkflowApplicationMappers.Map(entity);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.Create)]
	public virtual async Task<SponsorshipRequestDto> CreateAsync(CreateUpdateSponsorshipRequestDto input)
	{
		var entity = SponsorshipWorkflowApplicationMappers.Map(input);
		entity.Status = SponsorshipStatus.Draft;
		var inserted = await _requestRepository.InsertAsync(entity);

		await InsertHistoryAsync(inserted.Id, WorkflowAction.Created, SponsorshipStatus.Draft, SponsorshipStatus.Draft);

		return SponsorshipWorkflowApplicationMappers.Map(inserted);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.Edit)]
	public virtual async Task<SponsorshipRequestDto> UpdateDraftAsync(Guid id, CreateUpdateSponsorshipRequestDto input)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.Draft)
		{
			throw new BusinessException("SponsorshipWorkflow:InvalidStatus").WithData("Message", "Only drafts can be updated.");
		}

		SponsorshipWorkflowApplicationMappers.Map(input, entity);
		await _requestRepository.UpdateAsync(entity);

		await InsertHistoryAsync(entity.Id, WorkflowAction.Updated, SponsorshipStatus.Draft, SponsorshipStatus.Draft);

		return SponsorshipWorkflowApplicationMappers.Map(entity);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.Submit)]
	public virtual async Task SubmitAsync(Guid id)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.Draft)
		{
			throw new BusinessException("SponsorshipWorkflow:InvalidStatus").WithData("Message", "Only drafts can be submitted.");
		}

		entity.Status = SponsorshipStatus.PendingManagerApproval;
		entity.SubmittedAt = Clock.Now;
		await _requestRepository.UpdateAsync(entity);

		await InsertHistoryAsync(entity.Id, WorkflowAction.Submitted, SponsorshipStatus.Draft, SponsorshipStatus.PendingManagerApproval);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.Cancel)]
	public virtual async Task CancelAsync(Guid id)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status is SponsorshipStatus.Approved or SponsorshipStatus.Rejected or SponsorshipStatus.Cancelled)
		{
			throw new BusinessException("SponsorshipWorkflow:InvalidStatus").WithData("Message", "Cannot cancel a completed request.");
		}

		var previous = entity.Status;
		entity.Status = SponsorshipStatus.Cancelled;
		entity.CancelledAt = Clock.Now;
		await _requestRepository.UpdateAsync(entity);

		await InsertHistoryAsync(entity.Id, WorkflowAction.Cancelled, previous, SponsorshipStatus.Cancelled, "Cancelled by user");
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerApprove)]
	public virtual async Task ApproveByManagerAsync(Guid id, ApproveRejectRequestDto input)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.PendingManagerApproval)
		{
			throw new BusinessException("SponsorshipWorkflow:InvalidStatus").WithData("Message", "Only pending manager requests can be approved.");
		}

		entity.Status = SponsorshipStatus.PendingFinanceReview;
		entity.ManagerRemarks = input.Remarks;
		await _requestRepository.UpdateAsync(entity);

		await InsertHistoryAsync(entity.Id, WorkflowAction.ManagerApproved, SponsorshipStatus.PendingManagerApproval, SponsorshipStatus.PendingFinanceReview, input.Remarks);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerReject)]
	public virtual async Task RejectByManagerAsync(Guid id, ApproveRejectRequestDto input)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.PendingManagerApproval)
		{
			throw new BusinessException("SponsorshipWorkflow:InvalidStatus").WithData("Message", "Only pending manager requests can be rejected.");
		}

		entity.Status = SponsorshipStatus.Rejected;
		entity.ManagerRemarks = input.Remarks;
		await _requestRepository.UpdateAsync(entity);

		await InsertHistoryAsync(entity.Id, WorkflowAction.ManagerRejected, SponsorshipStatus.PendingManagerApproval, SponsorshipStatus.Rejected, input.Remarks);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceApprove)]
	public virtual async Task ApproveByFinanceAsync(Guid id, ApproveRejectRequestDto input)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.PendingFinanceReview)
		{
			throw new BusinessException("SponsorshipWorkflow:InvalidStatus").WithData("Message", "Only pending finance requests can be approved.");
		}

		entity.Status = SponsorshipStatus.Approved;
		entity.FinanceRemarks = input.Remarks;
		entity.ApprovedAt = Clock.Now;
		await _requestRepository.UpdateAsync(entity);

		await InsertHistoryAsync(entity.Id, WorkflowAction.FinanceApproved, SponsorshipStatus.PendingFinanceReview, SponsorshipStatus.Approved, input.Remarks);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceReject)]
	public virtual async Task RejectByFinanceAsync(Guid id, ApproveRejectRequestDto input)
	{
		var entity = await _requestRepository.GetAsync(id);
		if (entity.Status != SponsorshipStatus.PendingFinanceReview)
		{
			throw new BusinessException("SponsorshipWorkflow:InvalidStatus").WithData("Message", "Only pending finance requests can be rejected.");
		}

		entity.Status = SponsorshipStatus.Rejected;
		entity.FinanceRemarks = input.Remarks;
		await _requestRepository.UpdateAsync(entity);

		await InsertHistoryAsync(entity.Id, WorkflowAction.FinanceRejected, SponsorshipStatus.PendingFinanceReview, SponsorshipStatus.Rejected, input.Remarks);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.Default)]
	public virtual async Task<List<SponsorshipRequestListDto>> GetMyRequestsAsync()
	{
		if (_currentUser.Id == null)
		{
			return new List<SponsorshipRequestListDto>();
		}

		var userId = _currentUser.Id;
		var email = _currentUser.Email;
		var list = await _requestRepository.GetListAsync(x =>
			x.CreatorId == userId ||
			(email != null && x.RequestorName.ToLower() == email.ToLower()));
		return SponsorshipWorkflowApplicationMappers.MapToListDtos(list);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerApprove)]
	public virtual async Task<List<SponsorshipRequestListDto>> GetPendingManagerApprovalsAsync()
	{
		var list = await _requestRepository.GetListAsync(x => x.Status == SponsorshipStatus.PendingManagerApproval);
		return SponsorshipWorkflowApplicationMappers.MapToListDtos(list);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceApprove)]
	public virtual async Task<List<SponsorshipRequestListDto>> GetPendingFinanceReviewsAsync()
	{
		var list = await _requestRepository.GetListAsync(x => x.Status == SponsorshipStatus.PendingFinanceReview);
		return SponsorshipWorkflowApplicationMappers.MapToListDtos(list);
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.ViewAll)]
	public virtual async Task<PagedResultDto<SponsorshipRequestListDto>> GetAllRequestsAsync(int skip = 0, int take = 20)
	{
		var queryable = await _requestRepository.GetQueryableAsync();
		var total = await AsyncExecuter.CountAsync(queryable);
		var list = await AsyncExecuter.ToListAsync(
			queryable.OrderByDescending(x => x.CreationTime).Skip(skip).Take(take));

		return new PagedResultDto<SponsorshipRequestListDto>(total, SponsorshipWorkflowApplicationMappers.MapToListDtos(list));
	}

	[Authorize(SponsorshipWorkflowPermissions.SponsorshipRequests.ViewWorkflowHistory)]
	public virtual async Task<List<WorkflowHistoryDto>> GetWorkflowHistoryAsync(Guid sponsorshipRequestId)
	{
		var list = await _historyRepository.GetListAsync(x => x.SponsorshipRequestId == sponsorshipRequestId);
		return SponsorshipWorkflowApplicationMappers.MapToWorkflowHistoryDtos(list);
	}

	private Task InsertHistoryAsync(
		Guid requestId,
		WorkflowAction action,
		SponsorshipStatus previousStatus,
		SponsorshipStatus newStatus,
		string? remarks = null)
	{
		return _historyRepository.InsertAsync(new Entities.WorkflowHistory
		{
			SponsorshipRequestId = requestId,
			Action = action,
			PreviousStatus = previousStatus,
			NewStatus = newStatus,
			PerformedAt = Clock.Now,
			PerformedByUserId = _currentUser.Id,
			PerformedByUserName = _currentUser.UserName,
			Remarks = remarks
		});
	}
}
