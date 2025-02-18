using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace App.Payment.AzureFunction.OrderFunctions
{
    public static class OrderActivities
    {
        [Function(nameof(ValidateOrder))]
        public static bool ValidateOrder([ActivityTrigger] string orderId, FunctionContext context)
        {
            var logger = context.GetLogger(nameof(ValidateOrder));
            logger.LogInformation($"Validando pedido {orderId}");
            return true; // Simulando sucesso
        }

        [Function(nameof(ProcessPayment))]
        public static bool ProcessPayment([ActivityTrigger] string orderId, FunctionContext context)
        {
            var logger = context.GetLogger(nameof(ProcessPayment));
            logger.LogInformation($"Processando pagamento do pedido {orderId}");
            return true; // Simulando pagamento aprovado
        }

        [Function(nameof(SendConfirmation))]
        public static void SendConfirmation([ActivityTrigger] string orderId, FunctionContext context)
        {
            var logger = context.GetLogger(nameof(SendConfirmation));
            logger.LogInformation($"Enviando confirmação para o pedido {orderId}");
        }
    }
}
