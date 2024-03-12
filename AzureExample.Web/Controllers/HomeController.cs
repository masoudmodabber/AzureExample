using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AzureExample.Web.Models;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AzureExample.Web.Controllers;

public class HomeController : Controller
{
    private readonly ServiceBusSender _serviceBusSender;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, ServiceBusClient serviceBusClient, IConfiguration configuration)
    {
        _logger = logger;
        string queueName = configuration.GetSection("AzureSettings:QueueName").Value;
        _serviceBusSender = serviceBusClient.CreateSender(queueName);
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