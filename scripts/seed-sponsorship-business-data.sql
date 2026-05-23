-- SponsorshipWorkflow sample business data for PostgreSQL (Neon)
-- Idempotent: safe to run multiple times (skips when conference type already exists).
-- Prerequisite: run DbMigrator first (schema + identity/openiddict seed).

BEGIN;

-- Sponsorship types
INSERT INTO "AppSponsorshipTypes" (
    "Id", "Name", "Description", "IsActive",
    "ExtraProperties", "ConcurrencyStamp", "CreationTime", "IsDeleted"
) VALUES
    ('a1000001-0001-4001-8001-000000000001', 'Conference & Summit', 'Industry conferences, trade shows, and executive summits.', TRUE, '{}', gen_random_uuid()::text, NOW() AT TIME ZONE 'UTC', FALSE),
    ('a1000001-0001-4001-8001-000000000002', 'Sports & Wellness', 'Marathons, sports leagues, and employee wellness programs.', TRUE, '{}', gen_random_uuid()::text, NOW() AT TIME ZONE 'UTC', FALSE),
    ('a1000001-0001-4001-8001-000000000003', 'Community Outreach', 'Charity drives, local community events, and CSR initiatives.', TRUE, '{}', gen_random_uuid()::text, NOW() AT TIME ZONE 'UTC', FALSE),
    ('a1000001-0001-4001-8001-000000000004', 'Education & Training', 'University partnerships, scholarships, and training workshops.', TRUE, '{}', gen_random_uuid()::text, NOW() AT TIME ZONE 'UTC', FALSE),
    ('a1000001-0001-4001-8001-000000000005', 'Strategic Partnership', 'Co-marketing and long-term brand partnership programs.', TRUE, '{}', gen_random_uuid()::text, NOW() AT TIME ZONE 'UTC', FALSE)
ON CONFLICT ("Id") DO NOTHING;

-- Requests (CreatorId = requestor@test.com when present)
INSERT INTO "AppSponsorshipRequests" (
    "Id", "RequestTitle", "RequestorName", "Department", "SponsorshipTypeId",
    "EventName", "EventDate", "RequestedAmount", "Purpose", "ExpectedBusinessBenefit", "Remarks",
    "ManagerRemarks", "FinanceRemarks", "Status", "SubmittedAt", "ApprovedAt", "CancelledAt",
    "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "IsDeleted"
)
SELECT * FROM (VALUES
    ('b2000002-0002-4002-8002-000000000001'::uuid, 'Q3 Product Launch Reception', 'Alex Rivera', 'Marketing', 'a1000001-0001-4001-8001-000000000001'::uuid,
        'Launch Reception 2026', NOW() AT TIME ZONE 'UTC' + INTERVAL '60 days', 12500.00,
        'Host customers and partners at our flagship product launch.', 'Strengthen enterprise pipeline in APAC.', 'Draft — awaiting final attendee list.',
        NULL, NULL, 0, NULL, NULL, NULL,
        '{}', gen_random_uuid()::text, NOW() AT TIME ZONE 'UTC' - INTERVAL '3 days', u."Id", FALSE),
    ('b2000002-0002-4002-8002-000000000002'::uuid, 'Regional Tech Summit Gold Package', 'Priya Nair', 'Sales', 'a1000001-0001-4001-8001-000000000001'::uuid,
        'APAC Tech Summit 2026', NOW() AT TIME ZONE 'UTC' + INTERVAL '90 days', 28000.00,
        'Gold sponsor booth and speaking slot at APAC Tech Summit.', 'Generate 40+ qualified enterprise leads.', 'Submitted for manager review.',
        NULL, NULL, 1, NOW() AT TIME ZONE 'UTC' - INTERVAL '5 days', NULL, NULL,
        '{}', gen_random_uuid()::text, NOW() AT TIME ZONE 'UTC' - INTERVAL '7 days', u."Id", FALSE),
    ('b2000002-0002-4002-8002-000000000003'::uuid, 'City Marathon Title Sponsorship', 'Jordan Lee', 'Brand', 'a1000001-0001-4001-8001-000000000002'::uuid,
        'Metro City Marathon', NOW() AT TIME ZONE 'UTC' + INTERVAL '120 days', 45000.00,
        'Title sponsor branding along the marathon route and finish line.', 'Increase brand visibility with 15k participants.', 'Manager approved — pending finance.',
        'Approved — aligns with department goals.', NULL, 2, NOW() AT TIME ZONE 'UTC' - INTERVAL '12 days', NULL, NULL,
        '{}', gen_random_uuid()::text, NOW() AT TIME ZONE 'UTC' - INTERVAL '15 days', u."Id", FALSE),
    ('b2000002-0002-4002-8002-000000000004'::uuid, 'STEM Scholarship Program 2026', 'Morgan Chen', 'Corporate Affairs', 'a1000001-0001-4001-8001-000000000004'::uuid,
        'STEM Scholars Initiative', NOW() AT TIME ZONE 'UTC' + INTERVAL '30 days', 60000.00,
        'Fund scholarships for 25 underprivileged STEM students.', 'Support CSR goals and employer brand in universities.', 'Fully approved.',
        'Approved — aligns with department goals.', 'Funds released from CSR pool.', 3, NOW() AT TIME ZONE 'UTC' - INTERVAL '20 days', NOW() AT TIME ZONE 'UTC' - INTERVAL '2 days', NULL,
        '{}', gen_random_uuid()::text, NOW() AT TIME ZONE 'UTC' - INTERVAL '25 days', u."Id", FALSE),
    ('b2000002-0002-4002-8002-000000000005'::uuid, 'Music Festival Main Stage', 'Sam Taylor', 'Marketing', 'a1000001-0001-4001-8001-000000000005'::uuid,
        'Summer Beats Festival', NOW() AT TIME ZONE 'UTC' + INTERVAL '150 days', 75000.00,
        'Main stage branding and VIP hospitality tent.', 'Youth market penetration.', 'Rejected — budget exceeded threshold.',
        'Rejected — exceeds budget threshold.', NULL, 4, NOW() AT TIME ZONE 'UTC' - INTERVAL '18 days', NULL, NULL,
        '{}', gen_random_uuid()::text, NOW() AT TIME ZONE 'UTC' - INTERVAL '22 days', u."Id", FALSE),
    ('b2000002-0002-4002-8002-000000000006'::uuid, 'Neighborhood Food Bank Drive', 'Casey Wong', 'HR', 'a1000001-0001-4001-8001-000000000003'::uuid,
        'Food Bank Volunteer Day', NOW() AT TIME ZONE 'UTC' + INTERVAL '30 days', 8000.00,
        'Sponsor supplies and volunteer kits for community food drive.', 'Employee engagement and local goodwill.', 'Cancelled by requestor.',
        NULL, NULL, 5, NOW() AT TIME ZONE 'UTC' - INTERVAL '8 days', NULL, NOW() AT TIME ZONE 'UTC' - INTERVAL '1 day',
        '{}', gen_random_uuid()::text, NOW() AT TIME ZONE 'UTC' - INTERVAL '10 days', u."Id", FALSE)
) AS v("Id", "RequestTitle", "RequestorName", "Department", "SponsorshipTypeId", "EventName", "EventDate", "RequestedAmount", "Purpose", "ExpectedBusinessBenefit", "Remarks", "ManagerRemarks", "FinanceRemarks", "Status", "SubmittedAt", "ApprovedAt", "CancelledAt", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "IsDeleted")
CROSS JOIN LATERAL (SELECT "Id" FROM "AbpUsers" WHERE "NormalizedEmail" = 'REQUESTOR@TEST.COM' LIMIT 1) u
WHERE NOT EXISTS (SELECT 1 FROM "AppSponsorshipRequests" WHERE "Id" = 'b2000002-0002-4002-8002-000000000001');

-- Workflow history (only when pending-manager request was inserted)
INSERT INTO "AppWorkflowHistories" (
    "Id", "SponsorshipRequestId", "Action", "PreviousStatus", "NewStatus", "Remarks",
    "PerformedByUserId", "PerformedByUserName", "PerformedAt",
    "ExtraProperties", "ConcurrencyStamp", "CreationTime", "IsDeleted"
)
SELECT gen_random_uuid(), h."SponsorshipRequestId", h."Action", h."PreviousStatus", h."NewStatus", h."Remarks",
       u."Id", h."PerformedByUserName", h."PerformedAt", '{}', gen_random_uuid()::text, h."PerformedAt", FALSE
FROM (VALUES
    ('b2000002-0002-4002-8002-000000000001'::uuid, 0, 0, 0, NULL, 'requestor@test.com', NOW() AT TIME ZONE 'UTC' - INTERVAL '3 days'),
    ('b2000002-0002-4002-8002-000000000002'::uuid, 0, 0, 0, NULL, 'requestor@test.com', NOW() AT TIME ZONE 'UTC' - INTERVAL '7 days'),
    ('b2000002-0002-4002-8002-000000000002'::uuid, 1, 0, 1, NULL, 'requestor@test.com', NOW() AT TIME ZONE 'UTC' - INTERVAL '5 days'),
    ('b2000002-0002-4002-8002-000000000003'::uuid, 3, 1, 2, 'Budget aligned with Q3 plan.', 'manager@test.com', NOW() AT TIME ZONE 'UTC' - INTERVAL '10 days'),
    ('b2000002-0002-4002-8002-000000000004'::uuid, 5, 2, 3, 'Approved within CSR allocation.', 'finance@test.com', NOW() AT TIME ZONE 'UTC' - INTERVAL '2 days'),
    ('b2000002-0002-4002-8002-000000000005'::uuid, 4, 1, 4, 'Exceeds discretionary marketing cap.', 'manager@test.com', NOW() AT TIME ZONE 'UTC' - INTERVAL '18 days'),
    ('b2000002-0002-4002-8002-000000000006'::uuid, 7, 1, 5, 'Event postponed.', 'requestor@test.com', NOW() AT TIME ZONE 'UTC' - INTERVAL '1 day')
) AS h("SponsorshipRequestId", "Action", "PreviousStatus", "NewStatus", "Remarks", "PerformedByUserName", "PerformedAt")
LEFT JOIN LATERAL (
    SELECT "Id" FROM "AbpUsers" WHERE "NormalizedEmail" = CASE
        WHEN h."PerformedByUserName" = 'manager@test.com' THEN 'MANAGER@TEST.COM'
        WHEN h."PerformedByUserName" = 'finance@test.com' THEN 'FINANCE@TEST.COM'
        ELSE 'REQUESTOR@TEST.COM' END LIMIT 1
) u ON TRUE
WHERE EXISTS (SELECT 1 FROM "AppSponsorshipRequests" WHERE "Id" = h."SponsorshipRequestId")
  AND NOT EXISTS (SELECT 1 FROM "AppWorkflowHistories" WHERE "SponsorshipRequestId" = h."SponsorshipRequestId" AND "Action" = h."Action");

COMMIT;
