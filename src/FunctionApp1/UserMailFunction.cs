using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1;

public class UserMailFunction(ILogger<UserMailFunction> logger)
{
    [Function(nameof(UserMailFunction))]
    public async Task Run(
        [ServiceBusTrigger("user.created", "user.mail", Connection = "ServiceBusConnection")]
        ServiceBusReceivedMessage message)
    {
        await Task.Yield();
        
        logger.LogInformation("Message Body: {body}", message.Body);
        
        logger.LogInformation("User Email successfuly sent!");
    }
}