namespace DaprWorkflowSample.Dto;

public class WorkflowRequest
{
    public int NumberOfParallelSubOrchestration { get; set; }
    public int NumberOfParallelActivities { get; set; }
}