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
using ProductEvalation.Admin;
using ProductEvalation.DataBase;
using ProductEvalation.Services;
using System.Text.RegularExpressions;
using System.Windows.Threading;
namespace ProductEvalation.Admin
{
    /// <summary>
    /// NewAdmin.xaml etkileşim mantığı
    /// </summary>
    public partial class NewAdmin : Window
    {
        Services.Admin services = new Services.Admin();
        Company admins = new Company();
        bool control = false;
        bool control2 = false;
        bool timeControl = false;
        public NewAdmin()
        {
            InitializeComponent();
        }

        private void BtnCon_Click(object sender, RoutedEventArgs e)
        {
            var mailMatch = Regex.Match(txtMail.Text,"@"+txtName.Text+".com.tr");

            if (mailMatch.Success)
            {
                var companyMailcontrol = services.MailControlCompany(txtMail.Text);
                if (companyMailcontrol==true)
                {
                    admins.CompanyName = txtName.Text;
                    admins.Mail = txtMail.Text;

                    control = true; 
                }
                else
                {
                    MessageBox.Show("Bu eposta adresi zaten kayıtlı");
                    
                }
             }
            if (txtPass1.Password.ToString() == txtPass2.Password.ToString())
            {
                admins.Apassword = txtPass1.Password.ToString();
                control2 = true;
            }
            else
            {
                MessageBox.Show("Şifreler Eşleşmedi");
            }
            if (control==true&&control2==true)
            {
                services.NewADmin(admins);
                MessageBox.Show("Kaydınız Tamamlandı");
            }
            else
            {
                MessageBox.Show("Kaydınız Yapılamadı Alanlardan Hiçbirini Boş Bırakmadığınıza Emin Olun");
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void TxtPass1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pass1 = txtPass1.Password.ToString();
            var pass2 = txtPass2.Password.ToString();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();

            if (pass1!=pass2)
            {
                timeControl = false;
                lblPass.Foreground = new SolidColorBrush(Colors.Crimson);
                TimeSpan timer;
                timer = TimeSpan.FromSeconds(10);

                dispatcherTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    if (timeControl == false)
                    {
                        var time = timer.TotalSeconds;
                        if (time % 2 == 0)
                        {
                            lblPass.Content = "Şifreler Eşleşmedi";
                        }
                        else
                        {
                            lblPass.Content = "";
                        }
                        if (timer == TimeSpan.Zero)
                        {
                            dispatcherTimer.Stop();
                        }
                        timer = timer.Add(TimeSpan.FromSeconds(-1));
                    }

                    if (timeControl==true)
                    {
                        dispatcherTimer.Stop();
                        timeControl = false;
                        lblPass.Content = "";
                    }
                   
                },Dispatcher);

                dispatcherTimer.Start();

            }

            else
            {
            timeControl = true;
            }

        }


    }
}
