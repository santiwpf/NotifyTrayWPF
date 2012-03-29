using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace Librerias.Controles.WPF
{
    /// <summary>
    /// Lógica de interacción para NotifyTray.xaml
    /// </summary>
    public partial class NotifyTray : Window
    {
        private bool _storyBoardCompleted = false;
        private readonly DispatcherTimer _messageTimer = new DispatcherTimer();

        public NotifyTray(string message, string caption = "")
        {
            InitializeComponent();
            InitializeWindow(message, caption);
            InitializeTimer();
        }

        void InitializeWindow(string message, string caption)
        {
            Top = SystemParameters.PrimaryScreenHeight - Height - 20;
            Left = SystemParameters.PrimaryScreenWidth - Width - 10;
            textCaption.Text = caption;
            textMessage.Text = message;
        }

        void InitializeTimer()
        {
            _messageTimer.Tick += new EventHandler(MessageTimerTick);
            _messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 3000);
            _messageTimer.Start();
        }

        void MessageTimerTick(object sender, EventArgs e)
        {
            _messageTimer.Stop();
            Close();
        }

        void MouseIn(object sender, MouseEventArgs e)
        {
            _messageTimer.Stop();
        }

        void MouseOut(object sender, MouseEventArgs e)
        {
            _messageTimer.Start();
        }

        void WindowClicked(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        void WindowClosing(object sender, CancelEventArgs e)
        {
            if (!_storyBoardCompleted)
            {
                var unloadStoryboard = (Storyboard)FindResource("PopupOff");
                unloadStoryboard.Begin();
                e.Cancel = true;
            }
        }

        void CloseStoryBoardCompleted(object sender, EventArgs e)
        {
            _storyBoardCompleted = true;
            Close();
        }
    }
}

