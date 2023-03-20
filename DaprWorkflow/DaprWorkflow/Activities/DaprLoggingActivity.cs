using Dapr.Workflow;
using Microsoft.Extensions.Logging;

namespace DaprWorkflowSample.DaprWorkflow.Activities;

public class DaprLoggingActivity : WorkflowActivity<string, string>
{
    private readonly ILogger<DaprActivity> _logger;

    public DaprLoggingActivity(ILogger<DaprActivity> logger)
    {
        _logger = logger;
    }

    public override Task<string> RunAsync(WorkflowActivityContext context, string input)
    {
        _logger.LogInformation(input);

        return Task.FromResult("Completed");
    }
}