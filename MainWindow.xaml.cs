using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using NfcClient.Views;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using static NfcClient.MainViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace NfcClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        private IConfiguration _config;
        private readonly HubConnection _connection;
        public MainWindow()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();

            var hubUrl = _config["SignalR:HubUrl"];
            BaseUrl.url = _config["Service:Url"];

            #region SignalR
            _connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

            _connection.StartAsync();

            _connection.On<string, string>("Music", (user, message) =>
            {
                Dispatcher.Invoke(() =>
                {
                    webView.Source = new Uri(message);
                    if (IsSend) 
                    {
                        MyNotifyIcon.ShowBalloonTip($"接下来播放:", $"{user}", BalloonIcon.None);
                    }
                });
            });
            #endregion

            this.DataContext = new MainViewModel(_config);
            this.Visibility = Visibility.Hidden;
            //((MainViewModel)DataContext).CreatedTip += ViewModel_CreatedTip;
        }

        private async void WebView2_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                await webView.EnsureCoreWebView2Async();
                //await webView.CoreWebView2.ExecuteScriptAsync("document.body.style.backgroundColor = 'transparent';");
                await webView.CoreWebView2.ExecuteScriptAsync("document.getElementById('play').click();");
                //await webView.CoreWebView2.ExecuteScriptAsync("document.getElementById('player').style.margin = '0';");
            }
        }

        public static readonly DependencyProperty IsSendProperty =
DependencyProperty.Register(
nameof(IsSend),
typeof(bool),
typeof(MainWindow),
new PropertyMetadata(true));

        public bool IsSend
        {
            get { return (bool)GetValue(IsSendProperty); }
            set { SetValue(IsSendProperty, value); }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddView addView = new AddView();
            addView.Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var item = list.SelectedItem as ListBar;
            if (item != null) 
            {
                UpdateView updateView = new UpdateView(item);
                updateView.Show();
            }
        }

        //private void ViewModel_CreatedTip(object sender, CreatedTipEventArgs e)
        //{
        //    if (IsSend)
        //    {
        //        MyNotifyIcon.ShowBalloonTip($"{e.title}", $"{e.message}", BalloonIcon.None);
        //    }
        //}
    }
}