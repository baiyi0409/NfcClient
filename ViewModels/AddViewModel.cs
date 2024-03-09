using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flurl.Http;
using NfcClient.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NfcClient.ViewModels
{
    public partial class AddViewModel : ObservableObject
    {
        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string url;

        [RelayCommand]
        async void InsertMusic()
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Url)) 
            {
                return;
            }

            var client = new FlurlClient();
            // 使用Id, Name, 和Url属性的值构造一个ListBar对象
            var musicItem = new ListBar { Id = int.Parse(Id), Name = Name, Url = Url };
            var response = await client.Request($"{BaseUrl.url}/api/NFC/InsertMusic").PostJsonAsync(musicItem);
            if (response != null && response.StatusCode == 200) 
            {
                var mainViewModel = Application.Current.MainWindow.DataContext as MainViewModel;

                if (mainViewModel != null)
                {
                    mainViewModel.ListBars.Add(musicItem);
                }

                var currentWindow = Application.Current.Windows.OfType<AddView>().FirstOrDefault();

                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }
        }
    }
}
