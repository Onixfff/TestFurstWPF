using System.Collections.ObjectModel;
using System.Windows.Input;
using TestFurstWPF.Services;

namespace TestFurstWPF.ViewModels
{
    public class DownTimeViewModel
    {
        private readonly DbService _db;

        // Коллекция данных для DataGrid
        public ObservableCollection<Downtime> Downtimes { get; set; }

        // Команда для обновления данных
        public ICommand RefreshCommand { get; }

        public DownTimeViewModel()
        {
            _db = new DbService();
            Downtimes = new ObservableCollection<Downtime>();
            RefreshCommand = new RelayCommand(async () => await LoadDowntimesAsync());

            // Загрузка данных при старте
            Task.Run(LoadDowntimesAsync);
        }

        private async Task LoadDowntimesAsync()
        {
            try
            {
                var data = await _db.GetDownTimeAsync();

                // Обновляем коллекцию на основном потоке
                App.Current.Dispatcher.Invoke(() =>
                {
                    Downtimes.Clear();
                    foreach (var downtime in data)
                    {
                        Downtimes.Add(downtime);
                    }
                });
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки данных: {ex.Message}");
            }
        }
    }
}
