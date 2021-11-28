using System;
using System.Windows;

namespace IM.Services
{
    public static class DialogService
    {
        public static void ShowError(string message, Window owner = null)
        {
            if (owner == null)
                owner = Application.Current.MainWindow;

            MessageBox.Show(owner, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowException(Exception e, Window owner = null)
        {
            if (owner == null)
                owner = Application.Current.MainWindow;

            MessageBox.Show(owner, e.Message, "Error", MessageBoxButton.OK);
        }

        public static void ShowInfo(string message, Window owner = null)
        {
            if (owner == null)
                owner = Application.Current.MainWindow;

            MessageBox.Show(owner, message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static MessageBoxResult PromptYesNo(string caption, string message, Window owner = null)
        {
            if (owner == null)
                owner = Application.Current.MainWindow;

            return MessageBox.Show(owner, message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
