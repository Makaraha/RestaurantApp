using System;
using AvaloniaApplication.Views;
using AvaloniaApplication.Views.Popups;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.BaseViewModels
{
    public class BaseViewModel : ReactiveObject
    {
        protected virtual void ShowException(Exception exception)
        {
            var popup = new ExceptionPopup(exception);
            popup.ShowDialog(MainWindow.Instance);
        }
    }
}
