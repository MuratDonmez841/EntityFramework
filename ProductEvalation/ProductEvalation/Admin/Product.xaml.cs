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
using Microsoft.Win32;
using ProductEvalation.Admin;
using ProductEvalation.Services;
using ProductEvalation.DataBase;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace ProductEvalation.Admin
{
    /// <summary>
    /// Product.xaml etkileşim mantığı
    /// </summary>
    public partial class Product : Window
    {
        services servis = new services();
        Products product = new Products();
        Category category = new Category();
        ProductCategory ProductCategory = new ProductCategory();
        Services.Admin admin = new Services.Admin();
        FeedBackDetail feed = new FeedBackDetail();
        Sells sells = new Sells();
        string file;
        decimal price = 0;
        public Product(int ID)
        {
            InitializeComponent();
            lblCompanyID.Content = "" + ID + "";
           
            var row = admin.Company(ID);
            lblCompanyName.Content = "Firma Adı= "+row.CompanyName+"";
            lblCompanyMail.Content = "Mail Adresi= "+row.Mail+"";

            var toplamsatis = admin.ToplamSatis(ID);
            lblCompanyToplamSatis.Content = "Toplam Satış="+toplamsatis.ToString()+"";

            txtCompany.Text = ""+row.CompanyName+"";
            txtCompany.IsReadOnly = true;

            getLists();
            
            var getList = servis.getView(ID);
            dtGridProduct.ItemsSource = getList;

            

        }

        public void getLists() {
           

            var getCategory = servis.GetCategories();
            dtGridCategory.ItemsSource = getCategory;

            var getSellist = servis.GetSells(int.Parse(lblCompanyID.Content.ToString()));
            dtGridSells.ItemsSource = getSellist;

            var getFeedBack = servis.GetViewFeedBak(int.Parse(lblCompanyID.Content.ToString()));
            dtGridTP.ItemsSource = getFeedBack;

            var getCategoryCombo = servis.ReturnCategory();
            cbCategory.ItemsSource = getCategoryCombo;

            var getProductCategory = servis.returnProductCategory();
            cbProductCategory.ItemsSource = getProductCategory;
        }

        private void BtnNewProduct_Click(object sender, RoutedEventArgs e)
        {
            product.PName = txtProductName.Text;
            product.Color = txtProductColor.Text;

            decimal.TryParse(txtProductPrice.Text,out price);

            if (price!=0)
            {
                product.Price = price; 
            }
            int id= admin.GetCompanyID(txtCompany.Text);
            product.CompanyID = id;
            int producutCategory = servis.CategoryID(cbCategory.Text,cbProductCategory.Text);
            product.ProductCategoryID = producutCategory;
            product.SellStartDate =DateTime.Parse(datePickerSellStart.Text);

            product.IMG = file;

            servis.NewProduct(product);
            var getList = servis.getView(int.Parse(lblCompanyID.Content.ToString()));
            dtGridProduct.ItemsSource = getList;
            getLists();
        }

        private void BtnIMG_Click(object sender, RoutedEventArgs e)
        {
           

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            DialogResult result = dialog.ShowDialog();
            dialog.RestoreDirectory = true;
            if (result.ToString()=="OK")
            {
                file = dialog.FileName;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(file);
                bitmapImage.EndInit();
                imgProduct.Source = bitmapImage;
            }
        }

        private void BtnCategoryAdd_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Kategoriyi kaydetmek istediğinize emin misiniz","Uyarı!",MessageBoxButton.YesNoCancel)==MessageBoxResult.Yes)
            {
                category.CategoryName = txtCategoryName.Text.Trim();
                ProductCategory.ProductCategoryName = txtPerCategory.Text.Trim();
                servis.NewCategory(category);
                servis.NewPerCategory(ProductCategory, txtCategoryName.Text);

                var getCategory = servis.GetCategories();
                dtGridCategory.ItemsSource = getCategory; 
            }
            else
            {
                    
            }
        }

        private void BtnDFlag_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Teslim Durumu Teslim Edildi Olarak Değiştirilecek","Uyarı!",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
                sells.DeliveryFlag = 1;
                sells.Sell_Id =int.Parse(lblSellNu.Content.ToString());
                servis.flag(sells);
                var getSellist = servis.GetSells(int.Parse(lblCompanyID.Content.ToString()));
                dtGridSells.ItemsSource = getSellist;

                lblFlag.Content = "Teslim Edildi";
            }
            else
            {

            }

        }

        private void DtGridSells_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtGridSells.SelectedItem!=null)
            {
                var row = (Sells)dtGridSells.SelectedItem;
                var UserName = servis.GetUserName(row.UserID);
                LblName.Content = UserName;
                var productName = servis.GetProductName(row.ProductID);
                lblProduct.Content = productName;
                lblSellNu.Content = row.Sell_Id.ToString();
                lblPrice.Content = row.Price.ToString();
                lblDateTime.Content = row.DeliveryTime.ToString();
                if (row.DeliveryFlag == 0)
                {
                    lblFlag.Content = "Teslim Edilmedi";
                }
                else
                {
                    lblFlag.Content = "Teslim Edildi";
                } 
            }

            
        }

        private void DtGridTP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtGridTP.SelectedItem!=null)
            {
                var row = (V_FeedBakDetail)dtGridTP.SelectedItem;
                lblFeedBackNU.Content = row.FeedBackDetail_ID.ToString();
                var userName = servis.GetUserName(row.UserID);
                LblNameP.Content = userName;
                var product = servis.GetProductName(row.ProductID);
                lblProductP.Content = product;
                lblDayanıklılık.Content = row.Dayanıklılık.ToString();
                lblDısGörünüs.Content = row.DısGörünüs.ToString();
                lblFiyatPerf.Content = row.FiyatPerf.ToString();
                lblKalite.Content = row.Kalite.ToString();
                lblKullanımK.Content = row.KullanımKolaylıgı.ToString();  
                var flagControl = admin.flagControl(row.FeedBackDetail_ID);
                var imgUser = admin.feedBakcImage(row.FeedBackDetail_ID);
                ImUsersProductPhoto.Source = imgUser;


                if (flagControl==true)
                {
                    lblDurum.Content = "Bildiri Kapatılmış";
                }
                else
                {
                    lblDurum.Content = "Bildiri Açık";
                }

                txtAcıklama.Document.Blocks.Clear();
                txtAcıklama.Document.Blocks.Add(new Paragraph(new Run(row.UserReview.Trim())));
                row.CompanyReview = new TextRange(txtSirketA.Document.ContentStart,txtSirketA.Document.ContentEnd).Text.Trim();
            }

        }

        private void BntFlag_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bildirimi kapatmak istediğinize emin misiniz","Uyarı!",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
                feed.FeedBackDetail_ID =int.Parse(lblFeedBackNU.Content.ToString());
                feed.Flag = 1;
                servis.feedFlag(feed);     
                getLists();
            }
            else
            {

            }
        }

        private void BtnCompanyDes_Click(object sender, RoutedEventArgs e)
        {
            if (lblFeedBackNU.Content.ToString()!=""&&lblFeedBackNU.Content.ToString()!=null)
            {
                feed.FeedBackDetail_ID = int.Parse(lblFeedBackNU.Content.ToString());
                feed.CompanyReview = new TextRange(txtSirketA.Document.ContentStart, txtSirketA.Document.ContentEnd).Text.Trim();

                servis.feedFlag(feed);
                getLists();
            }
        }

        private void BtnPback_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void DtGridProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtGridProduct.SelectedItem!=null)
            {
                //var row = (V_Product_V)dtGridProduct.SelectedItem;
                //txtProductName.Text = row.PName;
                //txtProductColor.Text = row.Color;
                //decimal price = 0;
                //price = row.Price;
                //decimal.TryParse(txtProductPrice.Text,out price);
                
            }

        }

    }
}
