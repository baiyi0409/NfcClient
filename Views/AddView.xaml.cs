using Microsoft.Extensions.Configuration;
using NfcClient.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace NfcClient.Views
{
    /// <summary>
    /// AddView.xaml 的交互逻辑
    /// </summary>
    public partial class AddView : FluentWindow
    {

        private IConfiguration _config;
        public AddView()
        {
            InitializeComponent();
            this.DataContext = new AddViewModel();
        }
    }
}
