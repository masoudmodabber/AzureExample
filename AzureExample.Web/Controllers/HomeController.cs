using Microsoft.AspNetCore.Mvc;
using AzureExample.Web.Models;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using AzureExample.Configuration;
namespace AzureExample.Web.Controllers;

public class HomeController : Controller
{
    private readonly ServiceBusSender _serviceBusSender;
    private readonly ILogger<HomeController> _logger;
    private readonly string? _orderQueue;

    public HomeController(ILogger<HomeController> logger, ServiceBusClient serviceBusClient, IOptions<AzureSettings> azureSettings)
    {
        _logger = logger;
        _orderQueue = azureSettings.Value.OrderQueue;
        _serviceBusSender = serviceBusClient.CreateSender(_orderQueue);
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Order(Order order)
    {
        var message = new ServiceBusMessage(order.NumberOfCones.ToString());
        await _serviceBusSender.SendMessageAsync(message);
        return RedirectToAction("Index");
    }
}