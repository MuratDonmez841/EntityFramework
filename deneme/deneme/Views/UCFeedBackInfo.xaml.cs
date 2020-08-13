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
using System.Windows.Navigation;
using System.Windows.Shapes;
using deneme.Services;
using deneme.Models;
namespace deneme.Views
{
    /// <summary>
    /// UCFeedBackInfo.xaml etkileşim mantığı
    /// </summary>
    public partial class UCFeedBackInfo : UserControl
    {
        Services.Services services = new Services.Services();
        FeedBack_Description _Description = new FeedBack_Description();
        int Feedbackid;
        int? id;
        string filePath;
        bool control1;
        public UCFeedBackInfo(int FeedBackID,int? ID,bool control)
        {
            
            InitializeComponent();
            Feedbackid = FeedBackID;
            id = ID;
            control1 = control;
            if (control==true)
            {
                richCompany.IsReadOnly = true;
                richCompany.Background =new SolidColorBrush(Colors.LightGray);
                btnCompanyDescriptionAdd.Visibility = Visibility.Hidden;
                var companyDescription = services.getCompayDescriptions(Feedbackid);
                richCompany.Document.Blocks.Clear();
                richCompany.Document.Blocks.Add(new Paragraph(new Run(companyDescription.ToString().Trim())));
            }
            else
            {
                richCompany.IsReadOnly = false;
                richCompany.Background = new SolidColorBrush(Colors.White);
                btnCompanyDescriptionAdd.Visibility = Visibility.Visible;
            }
            getInfo();

        }
        public void getInfo() {

            var _info = services.ınfo(Feedbackid);
            var desc = services.description(Feedbackid);
            var point = services._Point(Feedbackid);

            txtSellPrice.Text = _info.Price.ToString();
            cbCompanyName.Text = services.getSoloCompanyName(_info.ProductID);
            cbName.Text = services.getSoloProductName(_info.ProductID);
            dateBuy.Text = _info.BuyDate.ToString();


            cbKullanimKolaylıgı.Text = point.KullanimKolayligi.ToString();
            cbDyaniklilik.Text = point.Dayaniklilik.ToString();
            cbDisGörünüs.Text = point.DisGörünüs.ToString();
            cbFiyatPerf.Text = point.FiyatPerformans.ToString();
            cbKalite.Text = point.Kalite.ToString();

            richBoxDiscp.Document.Blocks.Clear();
            richBoxDiscp.Document.Blocks.Add(new Paragraph(new Run(desc.Description.Trim())));
            filePath = _info.IMG;
            if (filePath ==null)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri("C:\\b\\resimyok.png");
                bitmapImage.EndInit();
                IMGProduct.Source = bitmapImage;
            }
            else
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(filePath);
                bitmapImage.EndInit();
                IMGProduct.Source = bitmapImage;
            }

        }
        private void BtnGeri_Click(object sender, RoutedEventArgs e)
        {
            if (control1 == false)
            {
                gridCH.Children.Clear();
                UCFeedBacksCompany uCFeedBacksCompany = new UCFeedBacksCompany(id);
                gridCH.Children.Add(uCFeedBacksCompany); 
            }
            else
            {
                gridCH.Children.Clear();
                UCUserOldFeedBacks uCUserOldFeedBacks = new UCUserOldFeedBacks(id);
                gridCH.Children.Add(uCUserOldFeedBacks);
            }
        }

        private void BtnCompanyDescriptionAdd_Click(object sender, RoutedEventArgs e)
        {
            if (richCompany.Document.Blocks.ToString()!="")
            {
                string companyDescription= new TextRange(richCompany.Document.ContentStart, richCompany.Document.ContentEnd).Text.Trim();

                bool  success=services.AddCompanyDescription(Feedbackid,companyDescription);
                if (success==true)
                {
                    MessageBox.Show("Açıklama başarılı bir şekilde kaydedildi!");
                }
                else
                {
                    MessageBox.Show("Açıklama kaydı başarısız!");
                }
            }
            else
            {

            }
        }

        private void RichCompany_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (control1!=true)
            {
                richCompany.Document.Blocks.Clear(); 
            }
            else
            {

            }
        } 
    }
}
