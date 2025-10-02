namespace Odysseus.Host.Domain.Enums;

/// <summary>
/// Represents the status of a job application
/// </summary>
public enum JobStatus
{
    NotApplied = 0,
    Applied = 1,
    InProgress = 2,
    WaitingResponse = 3,
    WaitingJobOffer = 4,
    Accepted = 5,
    Denied = 6,
    Failed = 7
}
