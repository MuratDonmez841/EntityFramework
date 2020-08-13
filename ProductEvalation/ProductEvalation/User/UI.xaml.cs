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
using MessageBox = System.Windows.MessageBox;

namespace ProductEvalation.User
{
    /// <summary>
    /// UI.xaml etkileşim mantığı
    /// </summary>

    public partial class UI : Window
    {
        Services.User users = new Services.User();
        services services = new services();
        Users dataUser = new Users();
        string file;
        Sells sells = new Sells();
        public UI(int ID)
        {
            InitializeComponent();
            headersource();
            var UserInfo = users.getUserInfo(ID);
            txtName.Text = UserInfo.UserName;
            txtAdress.Document.Blocks.Clear();
            txtAdress.Document.Blocks.Add(new Paragraph(new Run(UserInfo.UserAdress.Trim())));
            txtDisc.Text = UserInfo.UserDisct;
            txtMail.Text = UserInfo.UserMail;
            txtPotKod.Text = UserInfo.UserPostaKod.ToString();
            txtProv.Text = UserInfo.UserProvi;
            txtTelNU.Text = UserInfo.UserTel.ToString();
            string filePath = UserInfo.profilPhoto;

            //var img = services.stringToImage();
            //ImgDeneme.Source = img.FirstOrDefault();

            if (UserInfo.profilPhoto==null || UserInfo.profilPhoto=="")
            {
               BitmapImage bitmapImage = new BitmapImage();
               bitmapImage.BeginInit();
               bitmapImage.UriSource = new Uri("C:\\b\\anon.jpg");
               bitmapImage.EndInit();
               ImgUser.Source = bitmapImage;
            }
            else
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(filePath);
                bitmapImage.EndInit();
                ImgUser.Source = bitmapImage;
            }


            lblUserId.Content = "" + ID + "";

            services.log(ID);

            tabControl.SelectedItem = null;

            //var item = users.aa(ID);
            //dtDEneme.ItemsSource = item;


        }
        public void headersource()
        {
            var header = services.ListProductCategory(tbItemBike.Header.ToString());
            dtBike.ItemsSource = header;
            //var resim = services.stringToImage();
            //dtBike.ItemsSource = resim;

            var gloves = services.ListProductCategory(tbItemGloves.Header.ToString());
            dtGloves.ItemsSource = gloves;


            var tshirt = services.ListProductCategory(tbItemTshirt.Header.ToString());
            dtTshirt.ItemsSource = tshirt;

        }

        private void BtnFeedBack_Click(object sender, RoutedEventArgs e)
        {
            var Id = int.Parse(lblUserId.Content.ToString());
            FeedBackcs feedBack = new FeedBackcs(Id);
            feedBack.Show();
            this.Close();
        }

        private void BtnUpdateUserInfo_Click(object sender, RoutedEventArgs e)
        {
            var match = Regex.Match(txtMail.Text, "@hotmail.com");
            var match2 = Regex.Match(txtMail.Text, "@gmail.com");


            if (txtPass1.Password.ToString() == null || txtPass1.Password.ToString() == "" && txtPass2.Password.ToString() == "" || txtPass2.Password.ToString() == null)
            {
                if (match.Success || match2.Success)
                {
                    dataUser.UserID = int.Parse(lblUserId.Content.ToString());
                    dataUser.UserName = txtName.Text;
                    dataUser.UserAdress = new TextRange(txtAdress.Document.ContentStart, txtAdress.Document.ContentEnd).Text.Trim();
                    dataUser.UserDisct = txtDisc.Text;
                    dataUser.UserMail = txtMail.Text;
                    dataUser.UserProvi = txtProv.Text;
                    dataUser.UserTel = int.Parse(txtTelNU.Text.ToString());
                    dataUser.UserPostaKod = int.Parse(txtPotKod.Text.ToString());
                    dataUser.profilPhoto = file;
                    users.UpdateUser(dataUser, true);

                    MessageBox.Show("Bilgileriniz Güncellendi");
                }
                else
                {
                    MessageBox.Show("GErekli alanları doldurunuz");
                }
            }
            else if (txtPass1.Password.ToString() == txtPass2.Password.ToString() && txtPass1.Password.ToString() != null && txtPass1.Password.ToString() != "" && txtPass2.Password.ToString() != "" && txtPass2.Password.ToString() != null)
            {
                dataUser.UserID = int.Parse(lblUserId.Content.ToString());
                dataUser.UserName = txtName.Text;
                dataUser.UserAdress = new TextRange(txtAdress.Document.ContentStart, txtAdress.Document.ContentEnd).Text.Trim();
                dataUser.UserDisct = txtDisc.Text;
                dataUser.UserMail = txtMail.Text;
                dataUser.UserProvi = txtProv.Text;
                dataUser.UserTel = int.Parse(txtTelNU.Text.ToString());
                dataUser.UserPostaKod = int.Parse(txtPotKod.Text.ToString());
                dataUser.profilPhoto = file;

                dataUser.UserID = int.Parse(lblUserId.Content.ToString());
                dataUser.UserPassword = txtPass2.Password.ToString();
                users.UpdateUser(dataUser, false);

                MessageBox.Show("Bilgileriniz Güncellendi");
            }
            else
            {
                MessageBox.Show("Gerekli Alanları Doldurunuz");
            }

        }

        private void BtnTicket_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = new Ticket(int.Parse(lblUserId.Content.ToString()));
            ticket.Show();
            this.Close();
        }

        private void DtBike_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            if (dtBike.SelectedItem != null)
            {
                popupBike.IsOpen = false;
                var row = (V_Product_V)dtBike.SelectedItem;
                double? avgPoint = 0;
                var ID = row.ProductID;
                var image = services.Image(ID);
                ImgBike.Source = image;
                
                avgPoint = users.AVgPointForProduct(row.ProductID);

                if (avgPoint != 0)
                {
                    lblAvgPoint.Content = "Ürünün Ortalama Puanı : " + avgPoint.ToString() + "";
                }
                else
                {
                    lblAvgPoint.Content = "Ürün hakkında puanlama yapılmamış";
                }
                popupBike.IsOpen = true;

            }

        }

        private void BtnColosepopup_Click(object sender, RoutedEventArgs e)
        {
            popupBike.IsOpen = false;
            popup.IsOpen = false;
            popupTshirt.IsOpen = false;
            dtBike.SelectedItem = null;
            dtGloves.SelectedItem = null;
            dtTshirt.SelectedItem = null;
        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            V_Product_V row;
            TabItem tabItem = new TabItem();
            tabItem.TabIndex = tabControl.TabIndex;
            if (tabItem.TabIndex == tbItemBike.TabIndex)
            {
                row = (V_Product_V)dtBike.SelectedItem;
            }
            else if (tabItem.TabIndex == tbItemGloves.TabIndex)
            {
                row = (V_Product_V)dtGloves.SelectedItem;
            }
            else
            {
                row = (V_Product_V)dtTshirt.SelectedItem;
            }
            if (dtBike.SelectedItem != null || dtTshirt.SelectedItem != null || dtGloves.SelectedItem != null)
            {
                if (MessageBox.Show("Ürünü satın almak istediğinize emin misiniz", "Uyarı", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                {
                    sells.UserID = int.Parse(lblUserId.Content.ToString());
                    sells.ProductID = row.ProductID;

                    sells.Price = row.Price;

                    sells.DeliveryTime = DateTime.Now;

                    sells.DeliveryFlag = 0;

                    users.UserBuy(sells);

                    MessageBox.Show("İşlem tamamlandı");
                }
                else
                {


                }


            }
            else
            {
                MessageBox.Show("Satın almak istediğiniz ürünü seçiniz");


            }
        }

        private void DtGloves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtGloves.SelectedItem != null)
            {
                popup.IsOpen = false;
                var gloves = (V_Product_V)dtGloves.SelectedItem;
                double? avg = 0;
                avg = users.AVgPointForProduct(gloves.ProductID);

                var image = services.Image(gloves.ProductID);
                ImgGloves.Source = image;

                if (avg != 0)
                {
                    lblAvgPoi.Content = "Ürünün Ortalama Puanı: " + avg.ToString() + "";
                }
                else
                {
                    lblAvgPoi.Content = "Ürün Hakkında Puanlama yapılmamış";
                }
                popup.IsOpen = true;
            }
        }

        private void DtTshirt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dtTshirt.SelectedItem != null)
            {
                popupTshirt.IsOpen = false;
                var tshirt = (V_Product_V)dtTshirt.SelectedItem;
                double? avg = 0;
                avg = users.AVgPointForProduct(tshirt.ProductID);
                var image = services.Image(tshirt.ProductID);
                ImgTshirt.Source =image;
                if (avg != 0)
                {
                    lblAvgPointTh.Content = "Ürünün ortalama puanı: " + avg.ToString() + "";

                }
                else
                {
                    lblAvgPointTh.Content = "Ürünün ortalama puanı bulunamadı";
                }
                popupTshirt.IsOpen = true;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtBike.SelectedItem = null;
            dtGloves.SelectedItem = null;
            dtTshirt.SelectedItem = null;
            if (tabControl.SelectedItem != null)
            {
                if (tabControl.SelectedIndex == 0)
                {
                    tabControl.TabIndex = tbItemBike.TabIndex;
                }
                if (tabControl.SelectedIndex == 1)
                {
                    tabControl.TabIndex = tbItemGloves.TabIndex;
                }
                if (tabControl.SelectedIndex == 2)
                {
                    tabControl.TabIndex = tbItemTshirt.TabIndex;
                }

            }

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void IMGClick_Click(object sender, RoutedEventArgs e)
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
                ImgUser.Source = bitmapImage;
            }

        }

        private void DtBike_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {



            if (e.Column.Header.ToString()=="IMG")
            {
                e.Cancel=true;
            }
            //if (e.Column.Header.ToString() == "IMG")
            //{
            //    DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();

            //    Image image = new Image();

            //    var resim = services.stringToImage();

            //    image.Source = resim.FirstOrDefault();

            //    DataTemplate dataTemplate = new DataTemplate();

            //    DependencyProperty dependencyProperty = DependencyProperty.Register("resim", typeof(BitmapImage), typeof(Readability));


            //    templateColumn.CellTemplate.Template = dataTemplate.Template;

            //    templateColumn.Header = "Resim";
            //    e.Column = templateColumn;
            //e.Column.SetValue(dependencyProperty, image.Source);




            //BitmapImage bitmapImage = new BitmapImage();
            //DependencyProperty dp = new DependencyProperty();
            //e.Column.SetValue(dp,bitmapImage);

            //if (e.PropertyName=="IMG")
            //{
            //    DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn();
            //    Image image = new Image();
            //    var resim = services.stringToImage();
            //    image.Source = resim.FirstOrDefault();
            //    System.Windows.Data.Binding binding = new System.Windows.Data.Binding(e.PropertyName);

            //    binding.ConverterParameter = new ImageSourceConverter();

            //    dataGridTemplateColumn.ClipboardContentBinding = binding;
            //    dataGridTemplateColumn.Header = "Resim";

            //    e.Column = dataGridTemplateColumn;



            //    DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn();

            //    Image image = new Image();
            //    var resim = services.stringToImage();
            //    image.Source = resim.FirstOrDefault();
            //    dataGridTemplateColumn.Header = "Resim";

            //    e.Column = dataGridTemplateColumn;
            //}

            }

        private void DtGloves_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString()=="IMG")
            {
                e.Cancel = true;
            }
        }

        private void DtTshirt_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString()=="IMG")
            {
                e.Cancel = true;
            }
        }
    }
    }

