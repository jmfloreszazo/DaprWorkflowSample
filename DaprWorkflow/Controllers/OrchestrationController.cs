using Dapr.Workflow;
using DaprWorkflowSample.DaprWorkflow;
using DaprWorkflowSample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DaprWorkflowSample.Controllers;

[ApiController]
[Route("api")]
public class OrchestrationController : ControllerBase
{
    private readonly WorkflowEngineClient _daprClient;

    public OrchestrationController(WorkflowEngineClient daprClient)
    {
        _daprClient = daprClient;
    }

    [HttpPost]
    [Route("daprworkflow")]
    public async Task<string> StartDaprWorkflow([FromBody] WorkflowRequest data)
    {
        return await _daprClient.ScheduleNewWorkflowAsync(nameof(MainDaprOrchestration), Guid.NewGuid().ToString(),
            data);
    }
}