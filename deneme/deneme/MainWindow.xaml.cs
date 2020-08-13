using deneme.Views;
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

namespace deneme
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        Services.Services services = new Services.Services();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            //int? Companyid2;
            int id = 0;
            bool userConnetion= false;
            id = services.Connetion(txtID.Text, txtPassword.Password.ToString(),out userConnetion);
            //Companyid2 = services.CompanyUser(txtID.Text, txtPassword.Password.ToString());
            if (id != 0 && userConnetion==true)
            {
                UI uI = new UI(id);
                uI.Show();
            }
            else if (id != 0 && userConnetion==false)
            {
                CompanyMainxaml companyMainxaml = new CompanyMainxaml(id);
                companyMainxaml.Show();
            }
            else
            {
                MessageBox.Show("Mail veya şifre yanlış!");
            }
            //Win win = new Win();
            //win.Show();
            
        }

        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
        }
    }
}
