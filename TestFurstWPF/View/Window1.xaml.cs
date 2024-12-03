using System.Windows;
using TestFurstWPF.ViewModels;

namespace TestFurstWPF.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private DownTimeViewModel _downTimeViewModel;

        public Window1(DownTimeViewModel viewModel)
        {
            InitializeComponent();
            _downTimeViewModel = viewModel;
            DataContext = _downTimeViewModel;
        }

        private async void Window_Load(object sender, RoutedEventArgs e)
        {
            await _downTimeViewModel.LoadDowntimesAsync();
        }
    }
}
