using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Create_WI_from_Template
{
    class Constants
    {
        // Setting Class Guids
        public static readonly Guid CRSettings = new Guid("c7fe33bb-9760-3f88-59fc-0951e3221be4");
        public static readonly Guid PRSettings = new Guid("da0eeac9-9c85-e72b-f321-44a3fcec9c9a");
        public static readonly Guid IRSettings = new Guid("613c9f3e-9b94-1fef-4088-16c33bfd0be1");
        public static readonly Guid SRSettings = new Guid("fa662352-1660-33ae-6316-7fe1c9fecc6d");
        public static readonly Guid RRSettings = new Guid("6712c1b4-295c-1ac7-4726-d671f31ec4f4");
        public static readonly Guid ACSettings = new Guid("5e04a50d-01d1-6fce-7946-15580aa8681d");

        // Work Item Class Guids
        public static readonly Guid CRClass = new Guid("e6c9cf6e-d7fe-1b5d-216c-c3f5d2c7670c");
        public static readonly Guid PRClass = new Guid("422afc88-5eff-f4c5-f8f6-e01038cde67f");
        public static readonly Guid IRClass = new Guid("a604b942-4c7b-2fb2-28dc-61dc6f465c68");
        public static readonly Guid SRClass = new Guid("04b69835-6343-4de2-4b19-6be08c612989");
        public static readonly Guid RRClass = new Guid("d02dc3b6-d709-46f8-cb72-452fa5e082b8");

        // Work Item Prefix Properties
        public const string SRPrefix = "ServiceRequestPrefix";
        public const string CRPrefix = "SystemWorkItemChangeRequestIdPrefix";
        public const string PRPrefix = "ProblemIdPrefix";
        public const string IRPrefix = "PrefixForId";
        public const string RRPrefix = "SystemWorkItemReleaseRecordIdPrefix";

        // Activity Class Guids
        public static readonly Guid MAClass = new Guid("7ac62bd4-8fce-a150-3b40-16a39a61383d");
        public static readonly Guid RBClass = new Guid("5fe5d511-efb9-54a1-4be9-811f60e186c4");
        public static readonly Guid RAClass = new Guid("bfd90aaa-80dd-0fbb-6eaf-65d92c1d8e36");
        public static readonly Guid PAClass = new Guid("568c49f2-d7d6-d7d7-89dc-dfb5b39fded7");
        public static readonly Guid SAClass = new Guid("0ad0812b-f267-52bf-9f11-c56587786791");
        public static readonly Guid DAClass = new Guid("e786e1c7-b1fe-5b8b-ef8f-9e2dc346c44f");

        // Activity Prefix Properties
        public const string ACPrefix = "SystemWorkItemActivityIdPrefix";
        public const string MAPrefix = "SystemWorkItemActivityManualActivityIdPrefix";
        public const string RBPrefix = "MicrosoftSystemCenterOrchestratorRunbookAutomationActivityBaseIdPrefix";
        public const string RAPrefix = "SystemWorkItemActivityReviewActivityIdPrefix";
        public const string PAPrefix = "SystemWorkItemActivityParallelActivityIdPrefix";
        public const string SAPrefix = "SystemWorkItemActivitySequentialActivityIdPrefix";
        public const string DAPrefix = "SystemWorkItemActivityDependentActivityIdPrefix";

        // Relationship Guids
        public static readonly Guid WIContainsACRelId = new Guid("2da498be-0485-b2b2-d520-6ebd1698e61b");
        public static readonly Guid CreatedByUserRelId = new Guid("df738111-c7a2-b450-5872-c5f3b927481a");

        // Work Item Statuses
        public static readonly Guid SRStatusEnum = new Guid("a52fbc7d-0ee3-c630-f820-37eae24d6e9b");
        public static readonly Guid CRStatusEnum = new Guid("a87c003e-8c19-a25f-f8b2-151b56670e5c");
        public static readonly Guid PRStatusEnum = new Guid("720438eb-ba08-1263-0944-6791fcb48991");
        public static readonly Guid IRStatusEnum = new Guid("5e2d3932-ca6d-1515-7310-6f58584df73e");
        public static readonly Guid RRStatusEnum = new Guid("9b3c924a-3f95-b9d8-6711-42aa8271dd30");

        // User Class
        public static readonly Guid ADUserBaseClassId = new Guid("3567434d-015f-8dcc-f188-0a407f3a2168");
    }
}
