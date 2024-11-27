using System.Windows;
using TestFurstWPF.ViewModels;

namespace TestFurstWPF.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            DataContext = new DownTimeViewModel();
        }
    }
}
