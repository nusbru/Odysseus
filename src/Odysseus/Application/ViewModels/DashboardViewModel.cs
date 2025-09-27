using Odysseus.Domain.Enums;

namespace Odysseus.Application.ViewModels;

/// <summary>
/// ViewModel for the dashboard page displaying job application statistics and list
/// </summary>
public class DashboardViewModel
{
    public IEnumerable<JobApplyViewModel> JobApplications { get; set; } = new List<JobApplyViewModel>();

    public int TotalApplications { get; set; }
    public int PendingApplications { get; set; }
    public int InProgressApplications { get; set; }
    public int AcceptedApplications { get; set; }
    public int RejectedApplications { get; set; }

    /// <summary>
    /// Gets applications grouped by status
    /// </summary>
    public Dictionary<JobStatus, int> ApplicationsByStatus { get; set; } = new();

    /// <summary>
    /// Gets recent applications (within last 7 days)
    /// </summary>
    public IEnumerable<JobApplyViewModel> RecentApplications =>
        JobApplications.Where(x => x.IsRecent).OrderByDescending(x => x.DateOfApply);

    /// <summary>
    /// Gets applications waiting for response
    /// </summary>
    public IEnumerable<JobApplyViewModel> WaitingForResponse =>
        JobApplications.Where(x => x.Status == JobStatus.WaitingResponse || x.Status == JobStatus.WaitingJobOffer)
                      .OrderByDescending(x => x.DateOfApply);

    /// <summary>
    /// Gets the success rate percentage
    /// </summary>
    public double SuccessRate
    {
        get
        {
            if (TotalApplications == 0) return 0;
            return Math.Round((double)AcceptedApplications / TotalApplications * 100, 1);
        }
    }

    /// <summary>
    /// Gets the response rate percentage (applications that got a response, positive or negative)
    /// </summary>
    public double ResponseRate
    {
        get
        {
            if (TotalApplications == 0) return 0;
            var responsesReceived = AcceptedApplications + RejectedApplications;
            return Math.Round((double)responsesReceived / TotalApplications * 100, 1);
        }
    }

    /// <summary>
    /// Gets applications finished (accepted or rejected)
    /// </summary>
    public int FinishedApplications => AcceptedApplications + RejectedApplications;

    /// <summary>
    /// Gets applications grouped by country
    /// </summary>
    public Dictionary<string, int> ApplicationsByCountry { get; set; } = new();

    /// <summary>
    /// Gets whether there are any applications
    /// </summary>
    public bool HasApplications => TotalApplications > 0;

    /// <summary>
    /// Gets a summary message for the dashboard
    /// </summary>
    public string SummaryMessage
    {
        get
        {
            if (TotalApplications == 0)
                return "You haven't added any job applications yet. Start by adding your first application!";

            var pending = PendingApplications + InProgressApplications;
            if (pending > 0)
                return $"You have {pending} application(s) in progress. Keep up the great work!";

            return $"You have {TotalApplications} total applications with a {SuccessRate}% success rate.";
        }
    }

    /// <summary>
    /// Calculates statistics from the job applications list
    /// </summary>
    public void CalculateStatistics()
    {
        TotalApplications = JobApplications.Count();

        var groupedByStatus = JobApplications.GroupBy(x => x.Status)
                                           .ToDictionary(g => g.Key, g => g.Count());

        ApplicationsByStatus = groupedByStatus;

        PendingApplications = groupedByStatus.GetValueOrDefault(JobStatus.Applied, 0) +
                            groupedByStatus.GetValueOrDefault(JobStatus.WaitingResponse, 0) +
                            groupedByStatus.GetValueOrDefault(JobStatus.WaitingJobOffer, 0);

        InProgressApplications = groupedByStatus.GetValueOrDefault(JobStatus.InProgress, 0);

        AcceptedApplications = groupedByStatus.GetValueOrDefault(JobStatus.Accepted, 0);

        RejectedApplications = groupedByStatus.GetValueOrDefault(JobStatus.Denied, 0) +
                             groupedByStatus.GetValueOrDefault(JobStatus.Failed, 0);

        // Calculate countries statistics
        ApplicationsByCountry = JobApplications.GroupBy(x => x.CompanyCountry)
                                             .ToDictionary(g => g.Key, g => g.Count());
    }
}
