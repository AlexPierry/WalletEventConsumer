using System.Text.Json;
using Confluent.Kafka;
using WalletEventConsumer.Data;
using WalletEventConsumer.Model;

namespace WalletEventConsumer.Services
{
    public class BalanceConsumerService : IHostedService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _topic = "balances";

        public BalanceConsumerService(IServiceProvider serviceProvider)
        {
            var config = new ConsumerConfig
            {
                GroupId = "wallet",
                BootstrapServers = Environment.GetEnvironmentVariable("Kafka__BootstrapServers"),
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            Console.WriteLine("Trying to connect to Kafka on: " + config.BootstrapServers);

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);
            Task.Run(() => StartConsuming(cancellationToken), cancellationToken);
            return Task.CompletedTask;
        }

        private void StartConsuming(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(cancellationToken);
                    Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'.");

                    // Save to database
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        var balanceEvent = JsonSerializer.Deserialize<BalanceModel>(consumeResult.Message.Value)!;
                        balanceEvent.Date = DateTime.Now;
                        dbContext.BalanceEvents.Add(balanceEvent);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                _consumer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            return Task.CompletedTask;
        }
    }
}
