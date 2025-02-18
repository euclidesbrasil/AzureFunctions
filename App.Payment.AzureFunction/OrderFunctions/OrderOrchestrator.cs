using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace App.Payment.AzureFunction.OrderFunctions
{
    public static class OrderOrchestrator
    {
        [Function(nameof(OrderOrchestrator))]
        public static async Task<string> RunOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            var orderId = context.GetInput<string>();

            var validationResult = await context.CallActivityAsync<bool>(nameof(OrderActivities.ValidateOrder), orderId);
            if (!validationResult)
            {
                throw new Exception("Pedido inválido");
            }

            var paymentResult = await context.CallActivityAsync<bool>(nameof(OrderActivities.ProcessPayment), orderId);
            if (!paymentResult)
            {
                throw new Exception("Falha no pagamento");
            }

            await context.CallActivityAsync(nameof(OrderActivities.SendConfirmation), orderId);

            return "Pedido processado com sucesso!";
        }
    }
}
