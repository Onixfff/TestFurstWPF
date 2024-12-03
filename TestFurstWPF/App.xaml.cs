using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;
using TestFurstWPF.Services;
using TestFurstWPF.ViewModels;
using TestFurstWPF.View;

namespace TestFurstWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<DbService>();
                    services.AddTransient<DownTimeViewModel>();
                    services.AddTransient<Window1>();
                    services.AddLogging(builder =>
                    {
                        builder.AddConsole();
                        builder.SetMinimumLevel(LogLevel.Trace);
                    });
                })
                .Build();
            base.OnStartup(e);

            var mainWindow = _host.Services.GetRequiredService<Window1>();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }

}
