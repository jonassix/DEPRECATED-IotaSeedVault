using IotaSeedVault.Controller;
using IotaSeedVault.Model;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace IotaSeedVault
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //vault location and filename
        private string currentVault;
        private string cryptKey = string.Empty;
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

        private void BtnSave_Click(object sender, RoutedEventArgs e)
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

            if (cryptKey == string.Empty)
            {
                CryptKeyDialog popup = new CryptKeyDialog();
                popup.ShowDialog();
                cryptKey = popup.CryptKey;
            }

            JsonSerialization.WriteToJsonFile<ObservableCollection<IotaSeed>>(cryptKey, currentVault, vaultData);
            vaultData.Clear();
            currentVault = null;
            cryptKey = string.Empty;
            
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Iota Vault File (*.IVF)|*.IVF";
                if (openFileDialog.ShowDialog() == true)
                {
                    currentVault = openFileDialog.FileName;
                    CryptKeyDialog popup = new CryptKeyDialog();
                    popup.ShowDialog();
                    cryptKey = popup.CryptKey;
                    LoadVaultData(JsonSerialization.ReadFromJsonFile<ObservableCollection<IotaSeed>>(cryptKey, currentVault));
                }
            }
            catch
            {
                MessageBox.Show("Failure: Wrong password or wrong file");
            }
        }

        private void CopySeed_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;           
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid item = (DataGrid)contextMenu.PlacementTarget;
  
            Clipboard.SetText(((IotaSeed)item.SelectedCells[0].Item).Seed);
        }

        private void BtnRemoveSeed_Click(object sender, RoutedEventArgs e)
        {
            IotaSeed iS = ((FrameworkElement)sender).DataContext as IotaSeed;
            vaultData.Remove(iS);
        }

        private void BtnNewSeed_Click(object sender, RoutedEventArgs e)
        {
            vaultData.Add(IotaSeed.GenerateIotaSeed("undefined"));
        }
    }
}
