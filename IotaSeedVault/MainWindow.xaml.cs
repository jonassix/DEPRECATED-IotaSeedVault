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
                saveFileDialog.Filter = "Iota Vault File (*.IVF)|*.IVF";
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
            openFileDialog.Filter = "Iota Vault File (*.IVF)|*.IVF";
            if (openFileDialog.ShowDialog() == true)
            {
                currentVault = openFileDialog.FileName;
                LoadVaultData(JsonSerialization.ReadFromJsonFile<ObservableCollection<IotaSeed>>(currentVault));
            }
        }

        private void btnRemoveSeed_Click(object sender, RoutedEventArgs e)
        {
            IotaSeed iS = ((FrameworkElement)sender).DataContext as IotaSeed;
            vaultData.Remove(iS);
        }

        private void btnNewSeed_Click(object sender, RoutedEventArgs e)
        {
            vaultData.Add(IotaSeed.generateIotaSeed("undefined"));
        }
    }
}
