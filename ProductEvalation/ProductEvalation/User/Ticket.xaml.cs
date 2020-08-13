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
namespace ProductEvalation.User
{
    /// <summary>
    /// Ticket.xaml etkileşim mantığı
    /// </summary>
    public partial class Ticket : Window
    {
       Services.User ticket = new Services.User();
        services services = new services();
        public Ticket(int ID)
        {
            InitializeComponent();

            var GridTicket = ticket.UserTicket(ID);
            dtGridTicket.ItemsSource = GridTicket;
            lblID.Content = ""+ID+"";
        }

        private void DtGridTicket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = (V_FeedBakDetail)dtGridTicket.SelectedItem;
            if (dtGridTicket.SelectedItem!=null)
            {
                TicketPopUp.IsOpen = false;
                TicketPopUp.IsOpen = true;
                var userName = ticket.getUserInfo(int.Parse(row.UserID.ToString()));
                lblUserName.Content = "Kullanıcı Adı "+userName.UserName+"";
                lblMail.Content = "Mail Adresi: "+userName.UserMail+"";
                var ProductName = ticket.UserTicketProduct(int.Parse(row.ProductID.ToString()),false);
                lblProductName.Content = "Ürün İsmi: "+ProductName.ToString()+"";
                var CompanyName = ticket.UserTicketProduct(int.Parse(row.ProductID.ToString()),true);
                lblFirma.Content = "Firma Adı: "+CompanyName.ToString()+"";
                lblPoint.Content = "Ürün Ortalama Puanınız: "+((row.Dayanıklılık+row.DısGörünüs+row.Kalite+row.KullanımKolaylıgı+row.FiyatPerf)/5).ToString()+"";

                var image = services.Image(int.Parse(row.ProductID.ToString()));

                ImgTicket.Source = image; 
           

                if (ticket.FlagStatus(row.FeedBackDetail_ID)==false)
                {
                    lblTicketStatus.Content = "Bildirim Durumu : Açık";
                }
                else
                {
                    lblTicketStatus.Content = "Bildirim Durumu : Kapalı";
                }
                lblSikayet.Content = ""+row.UserReview.ToUpper()+"";

                if (row.CompanyReview!=null && row.CompanyReview!="")
                {
                    lblFirmaAcıklamasi.Content = "" + row.CompanyReview.ToUpper() + ""; 
                }
                else
                {
                    lblFirmaAcıklamasi.Content = "Firma Henüz Geri Dönüş Yapmamıştır";

                }
            }

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            UI uI = new UI(int.Parse(lblID.Content.ToString()));
            uI.Show();
            this.Close();

        }

        private void DtGridTicket_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString()=="UserID")
            {
                e.Cancel = true;
            }
        }

        private void BtnTicketPopupClose_Click(object sender, RoutedEventArgs e)
        {
            TicketPopUp.IsOpen = false;
            dtGridTicket.SelectedItem = null;
        }
    }
}
