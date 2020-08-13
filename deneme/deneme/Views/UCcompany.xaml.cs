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
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace deneme.Views
{
    /// <summary>
    /// UCcompany.xaml etkileşim mantığı
    /// </summary>
    public partial class UCcompany : UserControl
    {
        Services.Services services = new Services.Services();
        Company_Info companyIcon = new Company_Info();
        int? id;
        string file;
        
        public UCcompany(int? ID)
        {
            InitializeComponent();
            id = ID;

            companyInfo();
        }
        public void companyInfo() {

            var company = services.getNameCompany(id);
            var companyInfo = services.getCompanyInfo(id);
            var CompanIMG = services.CompanIcon(id);

            lblCompanyID.Content = "Şirket ID= "+company.CompanyID+"";
            lblCompanyName.Content = "Şirket İsmi= "+company.CompanyName+"";
            lblUserName.Content = "Yetkili İsmi "+services.getUserName(company.UserID)+"";

            txtCompanyMail.Text = companyInfo.Mail;
            txtTelNu.Text = companyInfo.TelNu.ToString();
            rchAdress.Document.Blocks.Clear();
            rchAdress.Document.Blocks.Add(new Paragraph(new Run(companyInfo.Adress.ToString())));

            ImgCompany.Source = CompanIMG;

        }
        private void BtnCompanyIMG_Click(object sender, RoutedEventArgs e)
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
                ImgCompany.Source = bitmapImage;
                if (file!=null)
                {
                    if (MessageBox.Show("Logo kaydedilsin mi?","Uyarı!!",MessageBoxButton.YesNoCancel)==MessageBoxResult.Yes)
                    {
                        companyIcon.Icon = file;
                        services.UpdateCompanIcon(companyIcon,id);
                        MessageBox.Show("Logo başarıyla kaydedildi.");
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
