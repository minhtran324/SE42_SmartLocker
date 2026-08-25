namespace SLMS.Domain.Enums;

// UC-C01 / BR-D04: account lifecycle
public enum UserAccountStatus
{
    PendingVerification,
    Active,
    Locked,
    Disabled
}

// Internal roles per UC-A01/A06. Traveler/Guest are represented via UserAccountStatus + no role.
public enum InternalRole
{
    SystemAdministrator,
    StationOperator,
    Technician
}

// UC-C09 / UC-C10 / UC-C15 / UC-C16 booking lifecycle
public enum BookingStatus
{
    PendingPayment,
    Confirmed,
    Stored,
    Completed,
    Cancelled,
    Expired,
    NoShow
}

// UC-C10 / UC-C15 / UC-C17 / UC-C16 payment kinds
public enum PaymentKind
{
    Base,
    Extension,
    Overdue,
    Refund
}

public enum PaymentStatus
{
    Pending,
    Paid,
    Failed,
    Expired,
    Refunded
}

// UC-A04: locker 4-state status
public enum LockerStatus
{
    Available,
    Occupied,
    Reserved,
    Maintenance
}

public enum StationStatus
{
    Active,
    Suspended,
    Maintenance
}

// UC-A08: incident lifecycle
public enum IncidentStatus
{
    Open,
    Investigating,
    Resolved,
    Closed
}

public enum IncidentType
{
    PaymentError,
    DoorIntrusion,
    DeviceOffline,
    EmergencyRetrieval,
    AbandonedProperty,
    Other
}
