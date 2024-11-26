using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class UserController : ControllerBase
{
    [HttpPost("/user/create", Name = "CreateUser")]
    public async Task<IActionResult> CreateUser(
        [FromBody] User user,
        [FromServices]IConfiguration configuration,
        [FromServices] ILogger<UserController> logger)
    {
        // Code that validates the user object and saves it to a database
        // Ommited for brevity

        // Send a message to a Service Bus topic
        var client = new ServiceBusClient(configuration.GetConnectionString("ServiceBusConnection"));
        var sender = client.CreateSender("user.created");
        await sender.SendMessageAsync(new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user))));
        logger.LogInformation("User created: {user}", user);

        return Ok();
    }
}

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}