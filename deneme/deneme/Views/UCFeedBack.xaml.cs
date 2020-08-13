using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using deneme.Models;
using deneme.Services;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;
namespace deneme.Views
{
    /// <summary>
    /// UCMain.xaml etkileşim mantığı
    /// </summary>
    public partial class UCFeedBack : UserControl
    {
        string full;
        FeedBack_Description _Description = new FeedBack_Description();
        FeedBack_Info _Info = new FeedBack_Info();
        FeedBack_Point _Point = new FeedBack_Point();
        string file;
        int wordCounter = 150;
        Services.Services services = new Services.Services();
        int id;
        public UCFeedBack(int UserID)
        {
            InitializeComponent();
            comboBoxItems();

            id = UserID;
            cbCompanyName.SelectedItem = null;
            cbName.SelectedItem = null;
        }

        public void comboBoxItems()
        {
            var CompanyName = services.getCompanyName();
            cbCompanyName.ItemsSource = CompanyName;

            List<int> cbItems = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                cbItems.Add(i + 1);
            }

            cbDisGörünüs.ItemsSource = cbItems;
            cbDyaniklilik.ItemsSource = cbItems;
            cbKalite.ItemsSource = cbItems;
            cbKullanimKolaylıgı.ItemsSource = cbItems;
            cbFiyatPerf.ItemsSource = cbItems;
        }

        private void CbCompanyName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCompanyName.SelectedItem != null)
            {
                var ProductName = services.GetCompanyProduct(cbCompanyName.SelectedItem.ToString().Trim());
                cbName.ItemsSource = ProductName;
            }
        }

        private void CbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbName.SelectedItem != null)
            {
                var price = services.GetProductPrice(cbName.SelectedItem.ToString().Trim());
                txtSellPrice.Text = price.ToString();
            }
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(async () =>
            {
                if (cbCompanyName.SelectedItem != null && cbName.SelectedItem != null && dateBuy.Text != null
                        && cbDisGörünüs.SelectedItem != null && cbDyaniklilik.SelectedItem != null && cbFiyatPerf.SelectedItem != null
                        && cbKalite.SelectedItem != null && cbKullanimKolaylıgı.SelectedItem != null)
                {
                    _Info.ProductID = services.GetProductID(cbName.Text);
                    _Info.Price = decimal.Parse(txtSellPrice.Text);
                    _Info.UserID = id;
                    _Info.BuyDate = DateTime.Parse(dateBuy.Text);
                    _Info.FormationDay = DateTime.Now;

                    _Point.Dayaniklilik = int.Parse(cbDyaniklilik.Text);
                    _Point.DisGörünüs = int.Parse(cbDisGörünüs.Text);
                    _Point.FiyatPerformans = int.Parse(cbFiyatPerf.Text);
                    _Point.Kalite = int.Parse(cbKalite.Text);
                    _Point.KullanimKolayligi = int.Parse(cbKullanimKolaylıgı.Text);


                    _Description.Description = new TextRange(richBoxDiscp.Document.ContentStart, richBoxDiscp.Document.ContentEnd).Text.Trim();

                    _Info.IMG = file;
                   await services.NewFeedBack(_Info, _Point, _Description);


                    MessageBox.Show("Geri Bildiriminiz Kaydedildi");



                }
                else
                {
                    MessageBox.Show("Gerekli alanları doldurunuz");
                }
            });
        }

        private void BtnAddIMG_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            DialogResult result = dialog.ShowDialog();
            dialog.RestoreDirectory = true;
            if (result.ToString() == "OK")
            {
                file = dialog.FileName;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(file);
                bitmapImage.EndInit();
                IMGProduct.Source = bitmapImage;
            }
        }

        private void RichBoxDiscp_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (wordCounter != 0)
            {
                if (wordCounter > 0)
                {
                    wordCounter--;
                    lblSozcuk.Content = "Kalan Sözcük Sayısı: " + wordCounter + "";
                }
            }
            else
            {
                richBoxDiscp.IsReadOnly = true;
            }
     
        }
        private void RichBoxDiscp_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Back && wordCounter < 150)
            {
                if (full==null)
                {
                    richBoxDiscp.IsReadOnly = false;
                    wordCounter++;
                    lblSozcuk.Content = "Kalan Sözcük Sayısı: " + wordCounter + "";
                }
                else
                {
                    richBoxDiscp.IsReadOnly = false;
                    wordCounter = wordCounter + full.Length;
                    lblSozcuk.Content = "Kalan Sözcük Sayısı: " + wordCounter + "";
                }
            }
     
        }
        private void RichBoxDiscp_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        
            TextPointer textPointer = richBoxDiscp.CaretPosition;
            string before;
            string after;
   
            before = textPointer.GetTextInRun(LogicalDirection.Backward);
            after = textPointer.GetTextInRun(LogicalDirection.Forward);

            full = after + before;
         
         
        }

        private void RichBoxDiscp_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space && wordCounter > 0)
            {
                wordCounter--;
                lblSozcuk.Content = "Kalan Sözcük Sayısı: " + wordCounter + "";
            }
        }


    }
}
