using Components.Consumers;
using MassTransit;
using Microsoft.Extensions.Hosting;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<LoginConsumer>();

            cfg.UsingRabbitMq(ConfigureBus);
        });
    }).Build();


static async void ConfigureBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
{
    configurator.ReceiveEndpoint(KebabCaseEndpointNameFormatter.Instance.Consumer<LoginConsumer>(), e =>
    {
        e.ConfigureConsumer<LoginConsumer>(context);
    });

    configurator.ConfigureEndpoints(context);
}

await host.RunAsync();













//TESTE DE REQUISICAO HTTP REQUEST

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        while (true)
//        {
//            using (HttpClient client = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await client.GetAsync("http://localhost:5551/Home/GetToken");

//                    if (response.IsSuccessStatusCode)
//                    {
//                        string result = await response.Content.ReadAsStringAsync();
//                        if (!string.IsNullOrEmpty(result))
//                        {
//                            HttpContent content = new StringContent(result, Encoding.UTF8, "application/json");
//                            await client.PostAsync("http://localhost:5501/Home/PostUsername", content);
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("Erro na chamada HTTP: " + response.StatusCode);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Erro na chamada HTTP, erro: " + ex);
//                }
//            }

//            // Aguardar um determinado período antes da próxima iteração
//            await Task.Delay(TimeSpan.FromSeconds(10));
//        }
//    }
//}