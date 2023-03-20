using Dapr.Workflow;
using DaprWorkflowSample.DaprWorkflow.Activities;
using DaprWorkflowSample.Dto;

namespace DaprWorkflowSample.DaprWorkflow.SubWorkflows;

public class DaprSubOrchestration : Workflow<WorkflowRequest, string>
{
    public override async Task<string> RunAsync(WorkflowContext context, WorkflowRequest input)
    {
        var activityIteration = 0;
        var scheduledActivities = new List<Task>();
        while (activityIteration < input.NumberOfParallelActivities)
        {
            scheduledActivities.Add(context.CallActivityAsync(nameof(DaprActivity), "execute"));
            activityIteration++;
        }

        await Task.WhenAll(scheduledActivities.ToArray());
        await context.CallActivityAsync(nameof(DaprLoggingActivity),
            $"Finished executing sub-orchestration with InstanceId : {context.InstanceId}");
        return "Completed";
    }
}