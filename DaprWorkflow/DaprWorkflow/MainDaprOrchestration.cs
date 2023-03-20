using Dapr.Workflow;
using DaprWorkflowSample.DaprWorkflow.Activities;
using DaprWorkflowSample.DaprWorkflow.SubWorkflows;
using DaprWorkflowSample.Dto;

namespace DaprWorkflowSample.DaprWorkflow;

public class MainDaprOrchestration : Workflow<WorkflowRequest, string>
{
    public override async Task<string> RunAsync(WorkflowContext context, WorkflowRequest input)
    {
        var initialStartTime = context.CurrentUtcDateTime;
        var activityIteration = 0;
        var scheduledSubOrchestrations = new List<Task>();
        while (activityIteration < input.NumberOfParallelActivities)
        {
            scheduledSubOrchestrations.Add(
                context.CallChildWorkflowAsync<string>(nameof(DaprSubOrchestration), input));
            activityIteration++;
        }

        await Task.WhenAll(scheduledSubOrchestrations);
        var ts = DateTime.UtcNow - initialStartTime;
        await context.CallActivityAsync(nameof(DaprLoggingActivity),
            $"The Dapr Workflow finished in {ts.TotalMilliseconds} ");
        return "Completed";
    }
}