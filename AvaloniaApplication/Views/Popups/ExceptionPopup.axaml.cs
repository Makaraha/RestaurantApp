using Avalonia.Controls;
using System;

namespace AvaloniaApplication.Views.Popups
{
    public partial class ExceptionPopup : Window
    {
        public ExceptionPopup() 
        {
            InitializeComponent();
        }

        public ExceptionPopup(Exception exception)
        {
            DataContext = this;

            InitializeComponent();
            Description.Text = exception.Message;
            CloseButton.Click += (_, _) => Close();
        }
    }
}
