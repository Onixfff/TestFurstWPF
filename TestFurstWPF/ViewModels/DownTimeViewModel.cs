using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using TestFurstWPF.Services;

namespace TestFurstWPF.ViewModels
{
    public partial class DownTimeViewModel : ObservableObject
    {
        private readonly DbService _db;
        private readonly ILogger<DownTimeViewModel> _logger;

        [ObservableProperty]
        private ObservableCollection<Downtime> downtimes;

        [ObservableProperty]
        private string? errorMessage;

        public IAsyncRelayCommand RefreshCommand { get; }

        public DownTimeViewModel(DbService db, ILogger<DownTimeViewModel> logger)
        {
            _logger = logger;
            _db = db;
            Downtimes = new ObservableCollection<Downtime>();
            RefreshCommand = new AsyncRelayCommand(LoadDowntimesAsync);

            // Загрузка данных при инициализации
            Task.Run(() => RefreshCommand.ExecuteAsync(null));
        }

        private async Task LoadDowntimesAsync()
        {
            try
            {
                var updatedDowntimes = await Task.Run(async () =>
                {
                    var result = await _db.GetDownTimeAsync();

                    var tempCollection = new ObservableCollection<Downtime>();

                    if (result.IsFailure)
                    {
                        tempCollection.Add(new Downtime());
                    }

                    foreach (var downtime in result.Value)
                    {
                        tempCollection.Add(downtime);
                    }

                    return tempCollection;
                });

                Downtimes = updatedDowntimes;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки данных: {ex.Message}");
                ErrorMessage = $"Ошибка: {ex.Message}";
            }
        }
    }
}
