using System;
using System.Collections.Generic;
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
using ProductEvalation.Services;
using ProductEvalation.DataBase;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ProductEvalation.User
{
    /// <summary>
    /// FeedBackcs.xaml etkileşim mantığı
    /// </summary>
    public partial class FeedBackcs : Window
    {
       Services.User UserPro = new Services.User();
        FeedBack feedBack = new FeedBack();
        FeedBackDetail feedBackDetail = new FeedBackDetail();
        FeedBack_FeedBackDetail _FeedBackDetail = new FeedBack_FeedBackDetail();
        services services = new services();
        int? userID;
        int? ProductID;
        string file;
        public FeedBackcs(int ID)
        {
            InitializeComponent();

            var userProducts = UserPro.getUserProducts(ID);
            dtGridFeedBack.ItemsSource = userProducts;
            lblID.Content = ""+ID+"";

        }
        private void DtGridFeedBack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
       
            if (dtGridFeedBack.SelectedItem!=null)
            {
                popUpPoint.IsOpen = false;
                int counter = 0;
                var row = (Sells)dtGridFeedBack.SelectedItem;
                userID = row.UserID;
                ProductID = row.ProductID;
                var image = services.Image(int.Parse(row.ProductID.ToString()));
                ImgFeedBack.Source = image;

                var listUserPoint = UserPro.UserPoint(ProductID,userID);

                if (row.DeliveryFlag==0)
                {
                    lblTicket.Content = "Bilet Durumu: Açık";
                    rchBoxReview.IsReadOnly = false;
                }
                else
                {
                    lblTicket.Content = "Bilet Durumu: Kapalı";
                    rchBoxReview.IsReadOnly = true;
                }

                if (listUserPoint==null)
                {
                    txtDısGörünüs.Text = "Dış Görünüş Puanını Buraya Giriniz";
                    txtDısGörünüs.IsReadOnly = false;
                    txtFiyPerf.Text = "Fiyat Performans Puanını Buraya Giriniz";
                    txtFiyPerf.IsReadOnly = false;
                    txtKullanımK.Text = "Kullanım Kolaylığı Puanını Buraya Giriniz";
                    txtKullanımK.IsReadOnly = false;
                    txtKalite.Text = "Kalite Puanını Buraya Giriniz";
                    txtKalite.IsReadOnly = false;
                    txtDay.Text = "Dayanıklılık Puanını Buraya Giriniz";
 
                }
                else
                {
                    foreach (var item in listUserPoint)
                    {

                        if (counter==0)
                        {
                            txtDısGörünüs.Text = "Ürün Puanınız=" +item + "";
                            txtDısGörünüs.IsReadOnly = true;
                        }
                        if (counter==1)
                        {
                            txtFiyPerf.Text = "Ürün Puanınız="+item+"";
                            txtFiyPerf.IsReadOnly = true;
                        }
                        if (counter==2)
                        {
                            txtKullanımK.Text = "Ürün Puanınız="+item+"";
                            txtKullanımK.IsReadOnly = true;
                        }
                        if (counter==3)
                        {
                            txtKalite.Text = "Ürün Puanınız= "+item+"";
                            txtKalite.IsReadOnly = true;
                        }
                        if (counter==4)
                        {
                            txtDay.Text = "Ürün Puanınız= "+item+"";
                            txtFiyPerf.IsReadOnly = true;
                        }

                        if (counter==5)
                        {
                            rchBoxReview.Document.Blocks.Clear();
                            rchBoxReview.Document.Blocks.Add(new Paragraph(new Run(item))); 
                        }

                        counter++;
                    }


                }
                popUpPoint.IsOpen=true;
                popUpPoint.Visibility = Visibility.Visible; 

            }
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
       {
            if (txtDısGörünüs.Text == "Dış Görünüş Puanını Buraya Giriniz")
            {
                txtDısGörünüs.Clear(); 
            }
        }

        private void BntClosePopup_Click(object sender, RoutedEventArgs e)
        {
            dtGridFeedBack.SelectedItem = null;
            popUpPoint.IsOpen = false;
        }

        private void TxtKullanımK_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (txtKullanımK.Text == "Kullanım Kolaylığı Puanını Buraya Giriniz")
            {
                txtKullanımK.Clear();
            }
        }

        private void TxtFiyPerf_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (txtFiyPerf.Text == "Fiyat Performans Puanını Buraya Giriniz")
            {
                txtFiyPerf.Clear();
            }
        }

        private void TxtKalite_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (txtKalite.Text == "Kalite Puanını Buraya Giriniz")
            {
                txtKalite.Clear();
            }
        }

        private void TxtDay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (txtDay.Text == "Dayanıklılık Puanını Buraya Giriniz")
            {
                txtDay.Clear();
            }
        }
            private void TxtDısGörünüs_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex tx = new Regex("[^0-9.-]+");
            e.Handled = tx.IsMatch(e.Text);
        }
        private void BtnPuan_Click(object sender, RoutedEventArgs e)
        {
            bool userFeedBackStatus = UserPro.NewFeedBackStatus(userID, ProductID);

            if (userFeedBackStatus!=false)
            {
                if (rchBoxReview.Document != null)
                {
                    feedBack.UserID = userID;
                    feedBack.ProductID = ProductID;
                    feedBackDetail.DısGörünüs = int.Parse(txtDısGörünüs.Text.ToString());
                    feedBackDetail.Kalite = int.Parse(txtKalite.Text.ToString());
                    feedBackDetail.KullanımKolaylıgı = int.Parse(txtKullanımK.Text.ToString());
                    feedBackDetail.Dayanıklılık = int.Parse(txtDay.Text.ToString());
                    feedBackDetail.FiyatPerf = int.Parse(txtFiyPerf.Text.ToString());
                    feedBackDetail.UserReview = new TextRange(rchBoxReview.Document.ContentStart, rchBoxReview.Document.ContentEnd).Text.Trim();
                    feedBackDetail.Flag = 0;
                    feedBackDetail.IMG = file;
                    

                    if (userFeedBackStatus == true)
                    {
                        MessageBox.Show("Kayıt Başarılı Bir Şekilde Tamamlandı!");
                    }

                }

                else
                {
                    MessageBox.Show("Bütün alanları doldurunuz");
                } 
            }
            else
            {
                MessageBox.Show("Zaten Açık bir kaydınız var");
            }
        }

        private void TxtDısGörünüs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtDısGörünüs.Text!= "Dış Görünüş Puanını Buraya Giriniz")
            {
                int control = -1;
                 int.TryParse(txtDısGörünüs.Text,out control);

                if (control < 0 || control > 5)
                {
                    txtDısGörünüs.Clear();
                }

            }
        }

        private void BtnProfil_Click(object sender, RoutedEventArgs e)
        {
            UI uI = new UI(int.Parse(lblID.Content.ToString()));
            uI.Show();
            this.Close();
        }

        private void DtGridFeedBack_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString()=="DeliveryFlag"||e.Column.Header.ToString()=="UserID"||e.Column.Header.ToString()=="Products"||e.Column.Header.ToString()=="Users")
            {
                e.Cancel = true;
            }
        }

        private void BtnUserProductImg_Click(object sender, RoutedEventArgs e)
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
                ImageUserProduct.Source = bitmapImage;
            }
        }
    }
}
