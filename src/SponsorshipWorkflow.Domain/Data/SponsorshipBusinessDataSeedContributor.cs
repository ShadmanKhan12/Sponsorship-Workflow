using System;
using System.Threading.Tasks;
using SponsorshipWorkflow.Entities;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace SponsorshipWorkflow.Data;

public class SponsorshipBusinessDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<SponsorshipType, Guid> _typeRepository;
    private readonly IRepository<SponsorshipRequest, Guid> _requestRepository;
    private readonly IRepository<WorkflowHistory, Guid> _historyRepository;
    private readonly IIdentityUserRepository _userRepository;
    private readonly IClock _clock;

    public SponsorshipBusinessDataSeedContributor(
        IRepository<SponsorshipType, Guid> typeRepository,
        IRepository<SponsorshipRequest, Guid> requestRepository,
        IRepository<WorkflowHistory, Guid> historyRepository,
        IIdentityUserRepository userRepository,
        IClock clock)
    {
        _typeRepository = typeRepository;
        _requestRepository = requestRepository;
        _historyRepository = historyRepository;
        _userRepository = userRepository;
        _clock = clock;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        if (await _typeRepository.AnyAsync(x => x.Id == SponsorshipSampleDataIds.TypeConference))
        {
            return;
        }

        var requestor = await _userRepository.FindByNormalizedEmailAsync("REQUESTOR@TEST.COM");
        var manager = await _userRepository.FindByNormalizedEmailAsync("MANAGER@TEST.COM");
        var finance = await _userRepository.FindByNormalizedEmailAsync("FINANCE@TEST.COM");

        var now = _clock.Now;

        await SeedTypesAsync(now);
        await SeedRequestsAsync(now, requestor?.Id, manager?.Id, finance?.Id);
        await SeedWorkflowHistoryAsync(now, requestor?.Id, manager?.Id, finance?.Id);
    }

    private async Task SeedTypesAsync(DateTime now)
    {
        await InsertTypeAsync(SponsorshipSampleDataIds.TypeConference, "Conference & Summit", "Industry conferences, trade shows, and executive summits.");
        await InsertTypeAsync(SponsorshipSampleDataIds.TypeSports, "Sports & Wellness", "Marathons, sports leagues, and employee wellness programs.");
        await InsertTypeAsync(SponsorshipSampleDataIds.TypeCommunity, "Community Outreach", "Charity drives, local community events, and CSR initiatives.");
        await InsertTypeAsync(SponsorshipSampleDataIds.TypeEducation, "Education & Training", "University partnerships, scholarships, and training workshops.");
        await InsertTypeAsync(SponsorshipSampleDataIds.TypePartnership, "Strategic Partnership", "Co-marketing and long-term brand partnership programs.");
    }

    private async Task InsertTypeAsync(Guid id, string name, string description)
    {
        var entity = new SponsorshipType
        {
            Name = name,
            Description = description,
            IsActive = true,
            CreationTime = _clock.Now
        };
        EntityHelper.TrySetId(entity, () => id);
        await _typeRepository.InsertAsync(entity, autoSave: true);
    }

    private async Task SeedRequestsAsync(DateTime now, Guid? requestorId, Guid? managerId, Guid? financeId)
    {
        await _requestRepository.InsertAsync(BuildRequest(
            SponsorshipSampleDataIds.RequestDraft,
            "Q3 Product Launch Reception",
            "Alex Rivera",
            "Marketing",
            SponsorshipSampleDataIds.TypeConference,
            "Launch Reception 2026",
            now.AddMonths(2),
            12500m,
            "Host customers and partners at our flagship product launch.",
            "Strengthen enterprise pipeline in APAC.",
            "Draft — awaiting final attendee list.",
            SponsorshipStatus.Draft,
            null, null, null,
            requestorId, now.AddDays(-3)), autoSave: true);

        await _requestRepository.InsertAsync(BuildRequest(
            SponsorshipSampleDataIds.RequestPendingManager,
            "Regional Tech Summit Gold Package",
            "Priya Nair",
            "Sales",
            SponsorshipSampleDataIds.TypeConference,
            "APAC Tech Summit 2026",
            now.AddMonths(3),
            28000m,
            "Gold sponsor booth and speaking slot at APAC Tech Summit.",
            "Generate 40+ qualified enterprise leads.",
            "Submitted for manager review.",
            SponsorshipStatus.PendingManagerApproval,
            now.AddDays(-5), null, null,
            requestorId, now.AddDays(-7)), autoSave: true);

        await _requestRepository.InsertAsync(BuildRequest(
            SponsorshipSampleDataIds.RequestPendingFinance,
            "City Marathon Title Sponsorship",
            "Jordan Lee",
            "Brand",
            SponsorshipSampleDataIds.TypeSports,
            "Metro City Marathon",
            now.AddMonths(4),
            45000m,
            "Title sponsor branding along the marathon route and finish line.",
            "Increase brand visibility with 15k participants.",
            "Manager approved — pending finance.",
            SponsorshipStatus.PendingFinanceReview,
            now.AddDays(-12), null, null,
            requestorId, now.AddDays(-15)), autoSave: true);

        await _requestRepository.InsertAsync(BuildRequest(
            SponsorshipSampleDataIds.RequestApproved,
            "STEM Scholarship Program 2026",
            "Morgan Chen",
            "Corporate Affairs",
            SponsorshipSampleDataIds.TypeEducation,
            "STEM Scholars Initiative",
            now.AddMonths(1),
            60000m,
            "Fund scholarships for 25 underprivileged STEM students.",
            "Support CSR goals and employer brand in universities.",
            "Fully approved.",
            SponsorshipStatus.Approved,
            now.AddDays(-20), now.AddDays(-2), null,
            requestorId, now.AddDays(-25)), autoSave: true);

        await _requestRepository.InsertAsync(BuildRequest(
            SponsorshipSampleDataIds.RequestRejected,
            "Music Festival Main Stage",
            "Sam Taylor",
            "Marketing",
            SponsorshipSampleDataIds.TypePartnership,
            "Summer Beats Festival",
            now.AddMonths(5),
            75000m,
            "Main stage branding and VIP hospitality tent.",
            "Youth market penetration.",
            "Rejected — budget exceeded threshold.",
            SponsorshipStatus.Rejected,
            now.AddDays(-18), null, null,
            requestorId, now.AddDays(-22)), autoSave: true);

        await _requestRepository.InsertAsync(BuildRequest(
            SponsorshipSampleDataIds.RequestCancelled,
            "Neighborhood Food Bank Drive",
            "Casey Wong",
            "HR",
            SponsorshipSampleDataIds.TypeCommunity,
            "Food Bank Volunteer Day",
            now.AddMonths(1),
            8000m,
            "Sponsor supplies and volunteer kits for community food drive.",
            "Employee engagement and local goodwill.",
            "Cancelled by requestor.",
            SponsorshipStatus.Cancelled,
            now.AddDays(-8), null, now.AddDays(-1),
            requestorId, now.AddDays(-10)), autoSave: true);
    }

    private async Task SeedWorkflowHistoryAsync(DateTime now, Guid? requestorId, Guid? managerId, Guid? financeId)
    {
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestDraft, WorkflowAction.Created, SponsorshipStatus.Draft, SponsorshipStatus.Draft, requestorId, "requestor@test.com", now.AddDays(-3));
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestPendingManager, WorkflowAction.Created, SponsorshipStatus.Draft, SponsorshipStatus.Draft, requestorId, "requestor@test.com", now.AddDays(-7));
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestPendingManager, WorkflowAction.Submitted, SponsorshipStatus.Draft, SponsorshipStatus.PendingManagerApproval, requestorId, "requestor@test.com", now.AddDays(-5));

        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestPendingFinance, WorkflowAction.Created, SponsorshipStatus.Draft, SponsorshipStatus.Draft, requestorId, "requestor@test.com", now.AddDays(-15));
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestPendingFinance, WorkflowAction.Submitted, SponsorshipStatus.Draft, SponsorshipStatus.PendingManagerApproval, requestorId, "requestor@test.com", now.AddDays(-14));
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestPendingFinance, WorkflowAction.ManagerApproved, SponsorshipStatus.PendingManagerApproval, SponsorshipStatus.PendingFinanceReview, managerId, "manager@test.com", now.AddDays(-10), "Budget aligned with Q3 plan.");

        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestApproved, WorkflowAction.Created, SponsorshipStatus.Draft, SponsorshipStatus.Draft, requestorId, "requestor@test.com", now.AddDays(-25));
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestApproved, WorkflowAction.Submitted, SponsorshipStatus.Draft, SponsorshipStatus.PendingManagerApproval, requestorId, "requestor@test.com", now.AddDays(-22));
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestApproved, WorkflowAction.ManagerApproved, SponsorshipStatus.PendingManagerApproval, SponsorshipStatus.PendingFinanceReview, managerId, "manager@test.com", now.AddDays(-8));
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestApproved, WorkflowAction.FinanceApproved, SponsorshipStatus.PendingFinanceReview, SponsorshipStatus.Approved, financeId, "finance@test.com", now.AddDays(-2), "Approved within CSR allocation.");

        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestRejected, WorkflowAction.Created, SponsorshipStatus.Draft, SponsorshipStatus.Draft, requestorId, "requestor@test.com", now.AddDays(-22));
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestRejected, WorkflowAction.Submitted, SponsorshipStatus.Draft, SponsorshipStatus.PendingManagerApproval, requestorId, "requestor@test.com", now.AddDays(-20));
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestRejected, WorkflowAction.ManagerRejected, SponsorshipStatus.PendingManagerApproval, SponsorshipStatus.Rejected, managerId, "manager@test.com", now.AddDays(-18), "Exceeds discretionary marketing cap.");

        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestCancelled, WorkflowAction.Created, SponsorshipStatus.Draft, SponsorshipStatus.Draft, requestorId, "requestor@test.com", now.AddDays(-10));
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestCancelled, WorkflowAction.Submitted, SponsorshipStatus.Draft, SponsorshipStatus.PendingManagerApproval, requestorId, "requestor@test.com", now.AddDays(-9));
        await InsertHistoryAsync(SponsorshipSampleDataIds.RequestCancelled, WorkflowAction.Cancelled, SponsorshipStatus.PendingManagerApproval, SponsorshipStatus.Cancelled, requestorId, "requestor@test.com", now.AddDays(-1), "Event postponed.");
    }

    private SponsorshipRequest BuildRequest(
        Guid id,
        string title,
        string requestorName,
        string department,
        Guid typeId,
        string eventName,
        DateTime eventDate,
        decimal amount,
        string purpose,
        string benefit,
        string remarks,
        SponsorshipStatus status,
        DateTime? submittedAt,
        DateTime? approvedAt,
        DateTime? cancelledAt,
        Guid? creatorId,
        DateTime creationTime)
    {
        var entity = new SponsorshipRequest
        {
            RequestTitle = title,
            RequestorName = requestorName,
            Department = department,
            SponsorshipTypeId = typeId,
            EventName = eventName,
            EventDate = eventDate,
            RequestedAmount = amount,
            Purpose = purpose,
            ExpectedBusinessBenefit = benefit,
            Remarks = remarks,
            Status = status,
            SubmittedAt = submittedAt,
            ApprovedAt = approvedAt,
            CancelledAt = cancelledAt,
            ManagerRemarks = status == SponsorshipStatus.PendingFinanceReview || status == SponsorshipStatus.Approved
                ? "Approved — aligns with department goals."
                : status == SponsorshipStatus.Rejected ? "Rejected — exceeds budget threshold." : null,
            FinanceRemarks = status == SponsorshipStatus.Approved ? "Funds released from CSR pool." : null
        };

        EntityHelper.TrySetId(entity, () => id);
        entity.CreatorId = creatorId;
        entity.CreationTime = creationTime;
        return entity;
    }

    private async Task InsertHistoryAsync(
        Guid requestId,
        WorkflowAction action,
        SponsorshipStatus previous,
        SponsorshipStatus next,
        Guid? userId,
        string userName,
        DateTime performedAt,
        string? remarks = null)
    {
        await _historyRepository.InsertAsync(new WorkflowHistory
        {
            SponsorshipRequestId = requestId,
            Action = action,
            PreviousStatus = previous,
            NewStatus = next,
            PerformedByUserId = userId,
            PerformedByUserName = userName,
            PerformedAt = performedAt,
            Remarks = remarks
        }, autoSave: true);
    }
}
