using IotaSeedVault.Controller;
using IotaSeedVault.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IotaSeedVault
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentVault;
        private static ObservableCollection<IotaSeed> tempData = new ObservableCollection<IotaSeed>();

        public MainWindow()
        {
            InitializeComponent();
            GenFakeData();
            LoadFakeData();
        }

        public void GenFakeData()
        {
            tempData.Clear();
            tempData.Add(IotaSeed.generateIotaSeed());
            tempData.Add(IotaSeed.generateIotaSeed());
            tempData.Add(IotaSeed.generateIotaSeed());
        }
        public void LoadFakeData()
        {
            dgData.DataContext = tempData;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            JsonSerialization.WriteToJsonFile<ObservableCollection<IotaSeed>>(@"person.txt", tempData);
        }
    }
}
