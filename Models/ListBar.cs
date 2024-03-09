using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfcClient
{
    public partial class ListBar:ObservableObject
    {
        /// <summary>
        /// ID
        /// </summary>
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string url;
    }
}
