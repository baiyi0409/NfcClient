using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flurl.Http;
using NfcClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NfcClient.ViewModels
{
    public partial class UpdateViewModel:ObservableObject
    {

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string url;
        public UpdateViewModel(ListBar item)
        {
            Id = item.Id.ToString();
            Name = item.Name;
            Url = item.Url;
        }

        [RelayCommand]
        async void UpdateMusic() 
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Url))
            {
                return;
            }

            var client = new FlurlClient();
            var musicItem = new ListBar { Id = int.Parse(Id), Name = Name, Url = Url };
            var response = await client.Request($"{BaseUrl.url}/api/NFC/UpdateMusic?id={Id}").PostJsonAsync(musicItem);
            if (response != null && response.StatusCode == 200)
            {
                var mainViewModel = Application.Current.MainWindow.DataContext as MainViewModel;

                if (mainViewModel != null)
                {
                    var item = mainViewModel.ListBars.FirstOrDefault(x => x.Id == int.Parse(Id));
                    item.Name = Name;
                    item.Url = Url;
                }

                var currentWindow = Application.Current.Windows.OfType<UpdateView>().FirstOrDefault();

                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }
        }
    }
}
