using IotaSeedVault.Controller;
using IotaSeedVault.Model;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;

namespace IotaSeedVault
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //vault location and filename
        private string currentVault;        
        private static ObservableCollection<IotaSeed> vaultData =  new ObservableCollection<IotaSeed>();

        public MainWindow()
        {
            InitializeComponent();            
            LoadVaultData(vaultData);
        }

        public void LoadVaultData(ObservableCollection<IotaSeed> data)
        {
            vaultData = data;
            dgData.DataContext = vaultData;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {            
            if (((currentVault == null) || (currentVault == "")) && (vaultData.Count > 0))
            {                
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    currentVault = saveFileDialog.FileName;
                }                    
            }
            JsonSerialization.WriteToJsonFile<ObservableCollection<IotaSeed>>(currentVault, vaultData);
            vaultData.Clear();
            currentVault = null;
            
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                currentVault = openFileDialog.FileName;
                LoadVaultData(JsonSerialization.ReadFromJsonFile<ObservableCollection<IotaSeed>>(currentVault));
            }
        }
    }
}
