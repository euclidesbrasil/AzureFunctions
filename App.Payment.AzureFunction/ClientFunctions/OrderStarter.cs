using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DurableTask.Client;
using App.Payment.AzureFunction.OrderFunctions;

namespace App.Payment.AzureFunction.ClientFunctions
{
    public static class OrderStarter
    {
        [Function("StartOrderProcess")]
        public static async Task<IActionResult> StartOrderProcess(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            [DurableClient] DurableTaskClient client)
        {
            string orderId = Guid.NewGuid().ToString();
            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(OrderOrchestrator), orderId);

            return new OkObjectResult($"Orquestração iniciada com ID: {instanceId}");
        }
    }
}
