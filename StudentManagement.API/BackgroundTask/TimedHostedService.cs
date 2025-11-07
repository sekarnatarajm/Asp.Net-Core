
namespace StudentManagement.API.BackgroundTask
{
    public class TimedHostedService(ILogger<TimedHostedService> _logger) : IHostedService, IDisposable
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private Timer _timer;
        private DateTime _stopTime = DateTime.Now.AddSeconds(25);

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(12));

            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            var currentTime = DateTime.Now;
            if (currentTime > _stopTime)
            {
                StopAsync(_cts.Token);
            }
            _logger.LogInformation("Timed Hosted Service is working.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);
            Dispose();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
