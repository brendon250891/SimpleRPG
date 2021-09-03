using Engine.Services;
using System.Windows;
using System.Windows.Threading;

namespace SimpleRPGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            string exceptionMessageText = $"An exception occured: {e.Exception.Message}\r\n\r\nat: {e.Exception.StackTrace}";

            LoggingService.Log(e.Exception);

            MessageBox.Show(exceptionMessageText, "Unhandled Exception", MessageBoxButton.OK);
        }
    }
}
