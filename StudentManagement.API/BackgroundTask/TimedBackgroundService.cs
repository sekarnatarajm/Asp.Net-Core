namespace StudentManagement.API.BackgroundTask
{
    public class TimedBackgroundService(ILogger<TimedBackgroundService> _logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Background Service running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Timed Background Service is working.");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _logger.LogInformation("Timed Background Service is stopping.");
        }
    }
}
