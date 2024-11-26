using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1;

public class UserCrmFunction(ILogger<UserCrmFunction> logger)
{
    [Function(nameof(UserCrmFunction))]
    public async Task Run(
        [ServiceBusTrigger("user.created", "user.crm", Connection = "ServiceBusConnection")]
        ServiceBusReceivedMessage message)
    {
        await Task.Yield();

        logger.LogInformation("Message Body: {body}", message.Body);

        logger.LogInformation("User synced with CRM system");
    }
}