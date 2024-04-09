using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace NfcClient
{
    public partial class MainViewModel:ObservableObject
    {
        private readonly IConfiguration _configuration;
        private string service;
        public MainViewModel(IConfiguration configuration)
        {
            this._configuration = configuration;
            service = _configuration["Service:Url"];
            CreateList();
        }

        [ObservableProperty]
        private ObservableCollection<ListBar> listBars;

        private async void CreateList()
        {
            ListBars = new ObservableCollection<ListBar>();
            if (string.IsNullOrEmpty(service))
            {
                return;
            }

            #region 请求数据
            var client = new FlurlClient();
            var response = await client.Request($"{service}/api/NFC/SelectMusicByAll").PostJsonAsync(null);

            if (response != null && response.StatusCode == 200)
            {
                var apiResponse = await response.GetJsonAsync<ApiResponse<ListBar[]>>();
                if (apiResponse.code == 200) 
                {
                    foreach (var item in apiResponse.data)
                    {
                        ListBars.Add(item);
                    }
                }
            }
            #endregion
        }

        [ObservableProperty]
        private string source;


        [RelayCommand]
        async void SearchMusic(string query)
        {
            if (string.IsNullOrEmpty(service))
            {
                return;
            }

            if (string.IsNullOrEmpty(query))
            {
                CreateList();
            }
            else
            {
                #region 请求数据
                var client = new FlurlClient();
                var response = await client.Request($"{service}/api/NFC/SelectMusicByQuery?name={query}").PostJsonAsync(null);
                if (response != null && response.StatusCode == 200)
                {
                    ListBars.Clear();
                    var apiResponse = await response.GetJsonAsync<ApiResponse<ListBar[]>>();
                    if (apiResponse.code == 200)
                    {
                        foreach (var item in apiResponse.data)
                        {
                            ListBars.Add(item);
                        }
                    }
                }
                #endregion
            }
        }


        [RelayCommand]
        async void DeleteMusic(ListBar item) 
        {
            if (string.IsNullOrEmpty(service))
            {
                return;
            }

            if (item == null) 
            {
                return;
            }


            #region 删除数据
            var id = item.Id;
            var client = new FlurlClient();
            var response = await client.Request($"{service}/api/NFC/DeleteMusic?id={id}").PostJsonAsync(null);
            if (response != null)
            {
                if (response.StatusCode == 200) 
                {
                    CreateList();
                }
            }
            #endregion
        }


        #region 主题控制
        [RelayCommand]
        private void ThemeChange(string? param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return;
            }

            if (param == "theme")
            {
                SwitchThemes();
                return;
            }
        }


        private static void SwitchThemes()
        {
            var currentTheme = Wpf.Ui.Appearance.ApplicationThemeManager.GetAppTheme();

            Wpf.Ui.Appearance.ApplicationThemeManager.Apply(
                currentTheme == Wpf.Ui.Appearance.ApplicationTheme.Light
                    ? Wpf.Ui.Appearance.ApplicationTheme.Dark
                    : Wpf.Ui.Appearance.ApplicationTheme.Light
            );
        }
        #endregion 

        #region 冒泡提示
        public class CreatedTipEventArgs : EventArgs
        {
            public string title { get; set; }

            public string message { get; set; }
        }

        public event EventHandler<CreatedTipEventArgs> CreatedTip;

        //[RelayCommand]
        //private void Tip()
        //{
        //    CreatedTip?.Invoke(this, new CreatedTipEventArgs() { title = "NotifyIcon", message = "提示" });
        //}
        #endregion

        #region 窗口控制
        [RelayCommand]
        private void MinWindow(Window window)
        {
            if (window.Visibility == Visibility.Hidden)
            {
                window.Visibility = Visibility.Visible;
                //window.WindowState = WindowState.Normal;
                window.Activate();
            }
            else
            {
                window.Visibility = Visibility.Hidden;
                //window.WindowState = WindowState.Minimized;
            }
        }
        #endregion
    }
}
