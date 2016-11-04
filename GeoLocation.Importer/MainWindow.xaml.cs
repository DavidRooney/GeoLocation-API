using GeoLocation.Importer.Enums;
using GeoLocation.Importer.Services;
using GeoLocation.Importer.Utils;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GeoLocation.Importer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string SelectedImportOption { get; set; }

        public MainWindow()
        {
            // default selected option
            SelectedImportOption = ImportOptionEnum.GeoLite2CountryBlocks.ToString();

            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;

            // Open dialog box at users documents folder.
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                txtEditor.Text = openFileDialog.FileName;
            }
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectedImportOption = (string)(sender as RadioButton).Content;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            ImportService service = new ImportService();
            ImportOptionEnum importOption = EnumUtil.ParseEnum<ImportOptionEnum>(this.SelectedImportOption);

            string message;
            var success = service.RunImport(txtEditor.Text, importOption, out message);

            if (success)
            {
                txtImportMessage.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                txtImportMessage.Foreground = new SolidColorBrush(Colors.Red);
            }

            txtImportMessage.Text = message;
        }

        private void DeleteElasticSearchIndex_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the index? This will delete all the GeoLite2 data.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ImportService service = new ImportService();
                string message;
                var success = service.DeleteElasticSearchIndex(out message);

                if (success)
                {
                    txtImportMessage.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    txtImportMessage.Foreground = new SolidColorBrush(Colors.Red);
                }

                txtImportMessage.Text = message;
            }
        }
    }
}
