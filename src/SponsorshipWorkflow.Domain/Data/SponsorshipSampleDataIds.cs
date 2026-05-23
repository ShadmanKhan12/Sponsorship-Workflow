using System;

namespace SponsorshipWorkflow.Data;

/// <summary>
/// Stable IDs for sample sponsorship data (SQL script + IDataSeedContributor).
/// </summary>
public static class SponsorshipSampleDataIds
{
    public static readonly Guid TypeConference = Guid.Parse("a1000001-0001-4001-8001-000000000001");
    public static readonly Guid TypeSports = Guid.Parse("a1000001-0001-4001-8001-000000000002");
    public static readonly Guid TypeCommunity = Guid.Parse("a1000001-0001-4001-8001-000000000003");
    public static readonly Guid TypeEducation = Guid.Parse("a1000001-0001-4001-8001-000000000004");
    public static readonly Guid TypePartnership = Guid.Parse("a1000001-0001-4001-8001-000000000005");

    public static readonly Guid RequestDraft = Guid.Parse("b2000002-0002-4002-8002-000000000001");
    public static readonly Guid RequestPendingManager = Guid.Parse("b2000002-0002-4002-8002-000000000002");
    public static readonly Guid RequestPendingFinance = Guid.Parse("b2000002-0002-4002-8002-000000000003");
    public static readonly Guid RequestApproved = Guid.Parse("b2000002-0002-4002-8002-000000000004");
    public static readonly Guid RequestRejected = Guid.Parse("b2000002-0002-4002-8002-000000000005");
    public static readonly Guid RequestCancelled = Guid.Parse("b2000002-0002-4002-8002-000000000006");
}
