using Avalonia.Controls;
using AvaloniaApplication.ViewModels;

namespace AvaloniaApplication.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel mainViewModel)
    {
        InitializeComponent();
        Root.DataContext = mainViewModel;
    }
}
