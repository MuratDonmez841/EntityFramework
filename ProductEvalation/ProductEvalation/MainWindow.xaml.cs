using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProductEvalation.Services;
using ProductEvalation.Admin;
using ProductEvalation.User;
namespace ProductEvalation
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {

        Services.User services = new Services.User();
        Services.Admin admin = new Services.Admin();
        services serviceso = new services();
        public MainWindow()
        {
            InitializeComponent();

            // serviceso.vs();
           
        }

        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {
            NewUser newUser = new NewUser();
            newUser.Show();
            this.Close();
        }

        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            var mail = txtID.Text;
            var pass = txtPassword.Password.ToString();
            var re=services.ConUser(mail, pass);
            var adminRe = admin.ConADmin(mail,pass);
            if (re!=0)
            {
                UI ui = new UI(re);
                ui.Show();
                txtID.Clear();
                txtPassword.Clear();
            }
            else if (adminRe!=0)
            {
                Product product = new Product(adminRe);
                product.Show();
                txtID.Clear();
                txtPassword.Clear();
            }
            else
            {
                MessageBox.Show("Mail adresi veya şifre yanlış","Uyarı!",MessageBoxButton.OK);
            }
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            NewAdmin newAdmin = new NewAdmin();
            newAdmin.Show();
            this.Close();
        }
    }
}
