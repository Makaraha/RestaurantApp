using Avalonia.Controls;
using AvaloniaApplication.ViewModels;

namespace AvaloniaApplication.Views;

public partial class MainWindow : Window
{
    public static MainWindow Instance { get; private set; }

    public MainWindow(MainViewModel mainViewModel)
    {
        InitializeComponent();
        Root.DataContext = mainViewModel;

        Instance = this;
    }
}
