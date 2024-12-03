using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Windows;
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
        private string? _errorMessage;

        public IAsyncRelayCommand RefreshCommand { get; }

        public DownTimeViewModel(DbService db, ILogger<DownTimeViewModel> logger)
        {
            _logger = logger;
            _db = db;
            Downtimes = new ObservableCollection<Downtime>();
            RefreshCommand = new AsyncRelayCommand(LoadDowntimesAsync);
        }

        public async Task LoadDowntimesAsync()
        {
            try
            {
                var result = await _db.GetDownTimeAsync();

                if (result.IsFailure)
                {
                    ErrorMessage = $"Ошибка {result.Error}";
                }

                if (result.IsSuccess)
                {
                    if (result.Value.Count > 0)
                    {
                        Downtimes.Clear();
                        foreach (var downtime in result.Value)
                        {
                            Downtimes.Add(downtime);
                        }
                        ErrorMessage = null;

                        _logger.LogInformation("Данные успешно загружены.");
                    }
                    else
                    {
                        ErrorMessage = $"Данных пока нет";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка загрузки данных");
                ErrorMessage = $"Ошибка: {ex.Message}";
            }
        }
    }
}
