using System;
using System.Windows.Input;
using Avalonia.Controls;
using ReactiveUI;

namespace AvaloniaApplication.Views.Popups
{
    public partial class ExceptionPopup : Window
    {
        public ExceptionPopup() { }

        public ExceptionPopup(Exception exception)
        {
            DataContext = this;

            InitializeComponent();
            Description.Text = exception.Message;
            CloseButton.Click += (_, _) => Close();
        }
    }
}
